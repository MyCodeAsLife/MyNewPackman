using R3;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private DIContainer _sceneContainer;
    private UIGameplayRootBinder _uiScene;
    private WorldGameplayRootBinder _worldGameplayRootBinder;

    public Observable<SceneExitParams> Run(SceneEnterParams sceneEnterParams, DIContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
        GameplayEnterParams gameplayEnterParams = sceneEnterParams.As<GameplayEnterParams>();

        // Регистрация всего необходимого для данной сцены
        GameplayRegistrations.Register(_sceneContainer, gameplayEnterParams);   // Регистрируем все сервисы необходимые для сцены
        var gameplayViewModelContainer = new DIContainer(_sceneContainer);      // Создаем отдельный контейнер для ViewModel's
        GameplayViewModelRegistartions.Register(gameplayViewModelContainer);    // Регистрируем все ViewModel's необходимые для сцены

        CreateUIRootBinder();
        CreateViewRootBinder(gameplayViewModelContainer.Resolve<WorldGameplayRootViewModel>());

        // For test
        //TestCommandProcessor();
        gameplayViewModelContainer.Resolve<UIGameplayRootViewModel>();

        //// Заглушка
        //var dummy = gameObject.AddComponent<SceneEntryPoint>();
        //dummy.Run(_sceneContainer);

        var exitParams = CreateExitParams();
        var exitSceneSignalSubj = CreateExitSignal();
        var exitToMainMenuSceneSignal = ConfigurateExitSignal(exitSceneSignalSubj, exitParams);
        return exitToMainMenuSceneSignal; // Возвращаем преобразованный сигнал
    }

    //private void TestCommandProcessor()
    //{
    //    var gameStateProvider = _sceneContainer.Resolve<IGameStateProvider>();
    //    gameStateProvider.GameState.Maps.ObserveAdd().Subscribe(e =>
    //    {
    //        var building = e.Value;
    //        Debug.Log("Building placed. Type id: " +
    //            building.TypeId +
    //            ", ID: " + building.Id +
    //            ", Position: " +
    //            building.Position.Value
    //            );
    //    });     //+++++++++++++++++++++

    //    var buildingsService = _sceneContainer.Resolve<BuildingsService>();
    //    buildingsService.PlaceBuilding("dummy", new Vector3Int(1, 0, 0));
    //    buildingsService.PlaceBuilding("dummy", new Vector3Int(1, 2, 0));
    //    buildingsService.PlaceBuilding("dummy", new Vector3Int(-1, 1, 0));
    //}

    private void CreateViewRootBinder(WorldGameplayRootViewModel worldGameplayRootViewModel)
    {
        _worldGameplayRootBinder = transform.AddComponent<WorldGameplayRootBinder>();
        _worldGameplayRootBinder.Bind(worldGameplayRootViewModel);
    }

    // Можно выделить в шаблон (в MainMenuEntryPoint похожая функция)
    private void CreateUIRootBinder()        // Создаем UIGameplayRootBinder
    {
        var uiScenePrefab = Resources.Load<UIGameplayRootBinder>(GameConstants.UIGameplayFullPath);
        _uiScene = Instantiate(uiScenePrefab);
        var uiRoot = _sceneContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(_uiScene.gameObject);
    }

    private Subject<R3.Unit> CreateExitSignal()
    {
        // Создание сигнала и привязка его к UI сцены (на кнопку выхода в MainMenu)
        var exitSceneSignalSubj = new Subject<R3.Unit>();
        _uiScene.Bind(exitSceneSignalSubj);
        return exitSceneSignalSubj;
    }

    private Observable<SceneExitParams> ConfigurateExitSignal(Subject<R3.Unit> exitSceneSignalSubj,
                                                                 SceneExitParams exitParams)
    {
        // Преобразовываем сигнал выхода со сцены, чтобы он возвращал значение GameplayExitParams
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToMainMenuSceneSignal;
    }

    private SceneExitParams CreateExitParams()
    {
        // Создаем\конфигурируем параметры выхода с текущей сцены
        var mainMenuEnterParams = new MainMenuEnterParams("Some params");
        var exitParams = new SceneExitParams(mainMenuEnterParams);
        return exitParams;
    }
}