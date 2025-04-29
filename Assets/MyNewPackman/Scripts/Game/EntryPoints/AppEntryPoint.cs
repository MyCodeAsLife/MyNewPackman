using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppEntryPoint                  // ���� ����� ���������� ����� ��������\�������� �����?
{
    private static AppEntryPoint _instance;

    private UIRootView _uiRootView;
    private DIContainer _projectContainer = new();  // ���� ���� ����� ����������, �� ��� ���� ������������

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoStartApplication()
    {
        _instance = new AppEntryPoint();        // �������� �������� ����������
        _instance.RunApplication();             // �������� ������ �����
    }

    // �������� �������� ����������(� ��� ����� �����������)
    private AppEntryPoint()
    {
        var loadingScreenPrefab = Resources.Load<UIRootView>(GameConstants.UIRootViewFullPath);
        _uiRootView = Object.Instantiate(loadingScreenPrefab);
        _projectContainer.RegisterInstance(_uiRootView);
    }

    // ������ �� �������� ������ �����, ��� ������� ����������
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

    // �������� ����� MainMenu
    private IEnumerator LoadAndStartMainMenu()
    {
        // ����������� ���������� ���� �� ���������� ��������
        _uiRootView.ShowLoadingScreen();            // ��������� ������(��������) ��������
        yield return LoadScene(GameConstants.Boot);         // ������ ��������� ����� "��������" ����� ��������� ���������� �����
        yield return LoadScene(GameConstants.MainMenu);     // ����� ��������� ������� �����

        yield return new WaitForSeconds(2f);                // �������� ��������������� ��������

        var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();   // ���� ����� ����� �� �����
        sceneEntryPoint.Run(_projectContainer);                                     // �������� ���� ��������� � ���������

        // �������� ���. �������� �� ������ �������� � MainMenu.        ����� ���������???
        sceneEntryPoint.GoToGameplaySceneRequested += () =>
        {
            Coroutines.StartRoutine(LoadAndStartGameplay());
        };

        _uiRootView.HideLoadingScreen();            // ���������� ������(��������) ��������
    }

    // �������� ����� Gameplay
    private IEnumerator LoadAndStartGameplay()
    {
        // ����������� ���������� ���� �� ���������� ��������
        _uiRootView.ShowLoadingScreen();            // ��������� ������(��������) ��������
        yield return LoadScene(GameConstants.Boot);         // ������ ��������� ����� "��������" ����� ��������� ���������� �����
        yield return LoadScene(GameConstants.Gameplay);     // ����� ��������� ������� �����

        yield return new WaitForSeconds(2f);                // �������� ��������������� ��������

        var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();   // ���� ����� ����� �� �����
        sceneEntryPoint.Run(_projectContainer);                                     // �������� ���� ��������� � ���������

        // �������� ���. �������� �� ������ �������� � MainMenu.        ����� ���������???
        sceneEntryPoint.GoToMainMenuSceneRequested += () =>
        {
            Coroutines.StartRoutine(LoadAndStartMainMenu());
        };

        _uiRootView.HideLoadingScreen();            // ���������� ������(��������) ��������
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
