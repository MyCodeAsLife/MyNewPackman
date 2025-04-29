using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private UIGameplayRootBinder _uiGameplayRootBinder;
    private DIContainer _sceneContainer;

    public event Action GoToMainMenuSceneRequested;

    public void Run(DIContainer projectContainer = null)
    {
        _sceneContainer = new DIContainer(projectContainer);

        CreateUISceneBinder();

        //// Заглушка
        //var dummy = gameObject.AddComponent<SceneEntryPoint>();
        //dummy.Run(_sceneContainer);

        // Черновой код, подписка на запрос перехода в MainMenu
        _uiGameplayRootBinder.GoToMainMenuButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };
    }

    // Можно выделить в шаблон (в MainMenuEntryPoint похожая функция)
    private void CreateUISceneBinder()        // Создаем UIGameplayRootBinder
    {
        var uiScenePrefab = Resources.Load<UIGameplayRootBinder>(GameConstants.UIGameplayFullPath);
        _uiGameplayRootBinder = Instantiate(uiScenePrefab);
        var uiRoot = _sceneContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(_uiGameplayRootBinder.gameObject);
    }
}