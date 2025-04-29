using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    private UIMainMenuRootBinder _uiMainMenuRootBinder;
    private DIContainer _sceneContainer;

    public event Action GoToGameplaySceneRequested;

    public void Run(DIContainer projectContainer = null)
    {
        _sceneContainer = new DIContainer(projectContainer);

        CreateUISceneBinder();

        //// Заглушка
        //var dummy = gameObject.AddComponent<SceneEntryPoint>();
        //dummy.Run(_sceneContainer);

        // Черновой код, подписка на запрос перехода в Gameplay
        _uiMainMenuRootBinder.GoToGameplayButtonClicked += () =>
        {
            GoToGameplaySceneRequested?.Invoke();
        };
    }

    // Можно выделить в шаблон (в GameplayEntryPoint похожая функция)
    private void CreateUISceneBinder()        // Создаем UIGameplayRootBinder
    {
        var uiScenePrefab = Resources.Load<UIMainMenuRootBinder>(GameConstants.UIMainMenuFullPath);
        _uiMainMenuRootBinder = Instantiate(uiScenePrefab);
        var uiRoot = _sceneContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(_uiMainMenuRootBinder.gameObject);
    }
}
