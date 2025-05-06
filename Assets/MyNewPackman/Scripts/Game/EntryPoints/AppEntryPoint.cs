using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppEntryPoint
{
    private static AppEntryPoint _instance;

    private readonly DIContainer _projectContainer = new();

    private UIRootView _uiRoot;
    private DIContainer _cashedSceneContainer;  // Для проведения финальных мероприятий над объектами контейнера перед уничтожением сцены

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoStartApplication()
    {
        _instance = new AppEntryPoint();        // Загрузка настроек приложения
        _instance.RunApplication();             // Загрузка первой сцены
    }

    // Загрузка настроек приложения(в том числе сохраненных)
    private AppEntryPoint()
    {
        // Создание и регистрация корневого UI
        var loadingScreenPrefab = Resources.Load<UIRootView>(GameConstants.UIRootViewFullPath);
        _uiRoot = Object.Instantiate(loadingScreenPrefab);
        _projectContainer.RegisterInstance(_uiRoot);

        // Регистрация сервисов уровня проекта
        _projectContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
    }

    // Запрос на загрузку первой сцены, при запуске приложения
    private void RunApplication()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == GameConstants.Gameplay)
        {
            var defaultScene = new GameplayEnterParams("ddd.sv", 1);
            Coroutines.StartRoutine(LoadAndStartGameplay(defaultScene));
            return;
        }

        if (sceneName == GameConstants.MainMenu)
        {
            Coroutines.StartRoutine(LoadAndStartMainMenu());
            return;
        }

        if (sceneName != GameConstants.Boot)
        {
            return;
        }
#endif

        Coroutines.StartRoutine(LoadAndStartMainMenu());
    }

    // Загрузка сцены MainMenu
    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null)
    {
        // Блокировать управление пока не закончится загрузка
        _uiRoot.ShowLoadingScreen();            // Включение экрана(скриншот) загрузки
        _cashedSceneContainer?.Dispose();        // Очистка контейнера сцены.

        yield return LoadScene(GameConstants.Boot);         // Сперва загружаем сцену "заглушку" чтобы выгрузить предыдущую сцену
        yield return LoadScene(GameConstants.MainMenu);     // Затем загружаем целевую сцену

        yield return new WaitForSeconds(1f);                // Имитация продолжительной загрузки

        var mainMenuContainer = _cashedSceneContainer = new DIContainer(_projectContainer);    // Создание чистого контейнера для сцены
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();   // Ищем точку входа на сцене
        sceneEntryPoint.Run(mainMenuEnterParams, mainMenuContainer).Subscribe(mainMenuExitParams =>
        {
            Coroutines.StartRoutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams));
        });                // Передаем туда настройки и контейнер

        _uiRoot.HideLoadingScreen();            // Отключение экрана(скриншот) загрузки
    }

    // Загрузка сцены Gameplay
    private IEnumerator LoadAndStartGameplay(SceneEnterParams sceneEnterParams)
    {
        // Блокировать управление пока не закончится загрузка
        _uiRoot.ShowLoadingScreen();            // Включение экрана(скриншот) загрузки
        _cashedSceneContainer?.Dispose();        // Очистка контейнера сцены.

        yield return LoadScene(GameConstants.Boot);         // Сперва загружаем сцену "заглушку" чтобы выгрузить предыдущую сцену
        yield return LoadScene(GameConstants.Gameplay);     // Затем загружаем целевую сцену

        yield return new WaitForSeconds(1f);                // Имитация продолжительной загрузки

        var gameplayContainer = _cashedSceneContainer = new DIContainer(_projectContainer); // Создание чистого контейнера для сцены
        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();   // Ищем точку входа на сцене
        sceneEntryPoint.Run(sceneEnterParams, gameplayContainer).Subscribe(gameplayExitParams =>
        {
            // Делаем чтото данными возвращенными из сцены gameplayExitParams
            Coroutines.StartRoutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
        });                                     // Передаем туда настройки и контейнер

        _uiRoot.HideLoadingScreen();            // Отключение экрана(скриншот) загрузки
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
