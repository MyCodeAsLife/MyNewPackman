using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppEntryPoint                  // Этот класс выгрузится после создания\загрузки сцены?
{
    private static AppEntryPoint _instance;

    private UIRootView _uiRootView;
    private DIContainer _projectContainer = new();  // Если этот класс выгрузится, то это поле бессмысленно

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoStartApplication()
    {
        _instance = new AppEntryPoint();        // Загрузка настроек приложения
        _instance.RunApplication();             // Загрузка первой сцены
    }

    // Загрузка настроек приложения(в том числе сохраненных)
    private AppEntryPoint()
    {
        var loadingScreenPrefab = Resources.Load<UIRootView>(GameConstants.UIRootViewFullPath);
        _uiRootView = Object.Instantiate(loadingScreenPrefab);
        _projectContainer.RegisterInstance(_uiRootView);
    }

    // Запрос на загрузку первой сцены, при запуске приложения
    private void RunApplication()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == GameConstants.Gameplay)
        {
            Coroutines.StartRoutine(LoadAndStartGameplay());
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
    private IEnumerator LoadAndStartMainMenu()
    {
        // Блокировать управление пока не закончится загрузка
        _uiRootView.ShowLoadingScreen();            // Включение экрана(скриншот) загрузки
        yield return LoadScene(GameConstants.Boot);         // Сперва загружаем сцену "заглушку" чтобы выгрузить предыдущую сцену
        yield return LoadScene(GameConstants.MainMenu);     // Затем загружаем целевую сцену

        yield return new WaitForSeconds(2f);                // Имитация продолжительной загрузки

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();   // Ищем точку входа на сцене
        sceneEntryPoint.Run(_projectContainer);                                     // Передаем туда настройки и контейнер

        // Черновой код. Подписка на запрос перехода в MainMenu.        НУЖНО Отписатся???
        sceneEntryPoint.GoToGameplaySceneRequested += () =>
        {
            Coroutines.StartRoutine(LoadAndStartGameplay());
        };

        _uiRootView.HideLoadingScreen();            // Отключение экрана(скриншот) загрузки
    }

    // Загрузка сцены Gameplay
    private IEnumerator LoadAndStartGameplay()
    {
        // Блокировать управление пока не закончится загрузка
        _uiRootView.ShowLoadingScreen();            // Включение экрана(скриншот) загрузки
        yield return LoadScene(GameConstants.Boot);         // Сперва загружаем сцену "заглушку" чтобы выгрузить предыдущую сцену
        yield return LoadScene(GameConstants.Gameplay);     // Затем загружаем целевую сцену

        yield return new WaitForSeconds(2f);                // Имитация продолжительной загрузки

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();   // Ищем точку входа на сцене
        sceneEntryPoint.Run(_projectContainer);                                     // Передаем туда настройки и контейнер

        // Черновой код. Подписка на запрос перехода в MainMenu.        НУЖНО Отписатся???
        sceneEntryPoint.GoToMainMenuSceneRequested += () =>
        {
            Coroutines.StartRoutine(LoadAndStartMainMenu());
        };

        _uiRootView.HideLoadingScreen();            // Отключение экрана(скриншот) загрузки
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
