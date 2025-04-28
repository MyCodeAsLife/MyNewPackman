using Assets.MyPackman.Settings;
using System.Collections;
using UnityEngine;

public class AppEntryPoint
{
    private static AppEntryPoint _instance;
    private UIRootView _uiRootView;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoStartApplication()
    {
        _instance = new AppEntryPoint();
        // �������� �������� ����� ��������(��������) ��� ������ ���������� ����������
        _instance.StartApplication();
    }

    private AppEntryPoint()
    {
        var loadingScreenPrefab = Resources.Load<UIRootView>(GameConstants.UIRootViewFullPath);
        _uiRootView = Object.Instantiate(loadingScreenPrefab);
        _uiRootView.ShowLoadingScreen();
        // �������� ��������(� ��� ����� �����������)
        Coroutines.StartRoutine(Loading());
    }

    private void StartApplication()
    {

    }

    private IEnumerator Loading()
    {
        // ����������� ���������� ���� �� ���������� ��������
        yield return new WaitForSeconds(2f);
        _uiRootView.HideLoadingScreen();
    }
}
