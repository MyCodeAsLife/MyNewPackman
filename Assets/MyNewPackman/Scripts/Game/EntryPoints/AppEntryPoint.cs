using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppEntryPoint
{
    private static AppEntryPoint _instance;

    private readonly DIContainer _projectContainer = new();

    private UIRootView _uiRoot;
    private DIContainer _cashedSceneContainer;  // ��� ���������� ��������� ����������� ��� ��������� ���������� ����� ������������ �����

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoStartApplication()
    {
        _instance = new AppEntryPoint();        // �������� �������� ����������
        _instance.RunApplication();             // �������� ������ �����
    }

    // �������� �������� ����������(� ��� ����� �����������)
    private AppEntryPoint()
    {
        // �������� �������\���������� �������� ������� �������� �����
        var settingsProvider = new SettingsProvider();
        // ����������� �������\���������� �������� ������� �������� �����
        _projectContainer.RegisterInstance<ISettingsProvider>(settingsProvider);
        // �������� �������\���������� ����������\�������� ������ �������\�����
        var gameStateProvider = new PlayerPrefsGameStateProvider();

        // ����������� �������� ������ �������
        _projectContainer.RegisterFactory(_ => new SomeCommonService()).AsSingle();
        _projectContainer.RegisterInstance<IGameStateProvider>(gameStateProvider);

        _projectContainer.Resolve<IGameStateProvider>().LoadSettingsState();  // �������� �������� ����������

        // �������� � ����������� ��������� UI
        var loadingScreenPrefab = Resources.Load<UIRootView>(GameConstants.UIRootViewFullPath);
        _uiRoot = Object.Instantiate(loadingScreenPrefab);
        _projectContainer.RegisterInstance(_uiRoot);
    }

    // ������ �� �������� ������ �����, ��� ������� ����������
    private async void RunApplication()
    {
        await _projectContainer.Resolve<ISettingsProvider>().LoadGameSettingsAsync();   // ����� ������� � ���� �������� ������?

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

    // �������� ����� MainMenu
    private IEnumerator LoadAndStartMainMenu(MainMenuEnterParams mainMenuEnterParams = null)
    {
        // ����������� ���������� ���� �� ���������� ��������
        _uiRoot.ShowLoadingScreen();            // ��������� ������(��������) ��������
        _cashedSceneContainer?.Dispose();        // ������� ���������� �����.

        yield return LoadScene(GameConstants.Boot);         // ������ ��������� ����� "��������" ����� ��������� ���������� �����
        yield return LoadScene(GameConstants.MainMenu);     // ����� ��������� ������� �����

        yield return new WaitForSeconds(1f);                // �������� ��������������� ��������

        var mainMenuContainer = _cashedSceneContainer = new DIContainer(_projectContainer);    // �������� ������� ���������� ��� �����
        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();   // ���� ����� ����� �� �����
        sceneEntryPoint.Run(mainMenuEnterParams, mainMenuContainer).Subscribe(mainMenuExitParams =>
        {
            Coroutines.StartRoutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams));
        });                // �������� ���� ��������� � ���������

        _uiRoot.HideLoadingScreen();            // ���������� ������(��������) ��������
    }

    // �������� ����� Gameplay
    private IEnumerator LoadAndStartGameplay(SceneEnterParams sceneEnterParams)
    {
        // ����������� ���������� ���� �� ���������� ��������
        _uiRoot.ShowLoadingScreen();            // ��������� ������(��������) ��������
        _cashedSceneContainer?.Dispose();        // ������� ���������� �����.

        yield return LoadScene(GameConstants.Boot);         // ������ ��������� ����� "��������" ����� ��������� ���������� �����
        yield return LoadScene(GameConstants.Gameplay);     // ����� ��������� ������� �����

        yield return new WaitForSeconds(1f);                // �������� ��������������� ��������

        // �������� ������/��������/���������� ������
        bool isGameStateLoaded = false;
        _projectContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
        yield return new WaitUntil(() => isGameStateLoaded);

        var gameplayContainer = _cashedSceneContainer = new DIContainer(_projectContainer); // �������� ������� ���������� ��� �����
        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();   // ���� ����� ����� �� �����
        sceneEntryPoint.Run(sceneEnterParams, gameplayContainer).Subscribe(gameplayExitParams =>
        {
            // ������ ����� � ������� ������������� �� ����� gameplayExitParams
            Coroutines.StartRoutine(LoadAndStartMainMenu(gameplayExitParams.MainMenuEnterParams));
        });                                     // �������� ���� ��������� � ���������

        _uiRoot.HideLoadingScreen();            // ���������� ������(��������) ��������
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
