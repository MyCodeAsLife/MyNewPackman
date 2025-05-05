using R3;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    private UIGameplayRootBinder _uiScene;
    private DIContainer _sceneContainer;

    public Observable<SceneExitParams> Run(SceneEnterParams sceneEnterParams, DIContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
        GameplayEnterParams gameplayEnterParams = sceneEnterParams.As<GameplayEnterParams>();

        CreateUISceneBinder();

        //// Заглушка
        //var dummy = gameObject.AddComponent<SceneEntryPoint>();
        //dummy.Run(_sceneContainer);

        Debug.Log($"Run Gameplay scene. Save file: {gameplayEnterParams?.SaveFileName}");           //++++++++++++++++++++++

        var exitParams = CreateExitParams();
        var exitSceneSignalSubj = CreateExitSignal();
        var exitToMainMenuSceneSignal = ConfigurateExitSignal(exitSceneSignalSubj, exitParams);
        return exitToMainMenuSceneSignal; // Возвращаем преобразованный сигнал
    }

    private Subject<Unit> CreateExitSignal()
    {
        // Создание сигнала и привязка его к UI сцены (на кнопку выхода в MainMenu)
        var exitSceneSignalSubj = new Subject<Unit>();
        _uiScene.Bind(exitSceneSignalSubj);
        return exitSceneSignalSubj;
    }

    private Observable<SceneExitParams> ConfigurateExitSignal(Subject<Unit> exitSceneSignalSubj,
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

    // Можно выделить в шаблон (в MainMenuEntryPoint похожая функция)
    private void CreateUISceneBinder()        // Создаем UIGameplayRootBinder
    {
        var uiScenePrefab = Resources.Load<UIGameplayRootBinder>(GameConstants.UIGameplayFullPath);
        _uiScene = Instantiate(uiScenePrefab);
        var uiRoot = _sceneContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(_uiScene.gameObject);
    }
}