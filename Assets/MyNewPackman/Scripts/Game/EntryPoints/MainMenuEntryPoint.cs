using R3;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    private UIMainMenuRootBinder _uiScene;
    private DIContainer _sceneContainer;

    public Observable<MainMenuExitParams> Run(MainMenuEnterParams mainMenuEnterParams, DIContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;

        CreateUISceneBinder();

        //// Заглушка
        //var dummy = gameObject.AddComponent<SceneEntryPoint>();
        //dummy.Run(_sceneContainer);

        Debug.Log($"Run MainMenu scene. Result: {mainMenuEnterParams?.EnterParams}");           //++++++++++++++++++++++

        var exitParams = CreateExitParams();
        var exitSceneSignalSubj = CreateExitSignal();
        var exitToGameplaySceneSignal = ConfigurateExitSignal(exitSceneSignalSubj, exitParams);
        return exitToGameplaySceneSignal; // Возвращаем преобразованный сигнал
    }

    private Subject<Unit> CreateExitSignal()
    {
        // Создание сигнала и привязка его к UI сцены (на кнопку выхода в MainMenu)
        var exitSceneSignalSubj = new Subject<Unit>();
        _uiScene.Bind(exitSceneSignalSubj);
        return exitSceneSignalSubj;
    }

    private Observable<MainMenuExitParams> ConfigurateExitSignal(Subject<Unit> exitSceneSignalSubj,
                                                                 MainMenuExitParams exitParams)
    {
        // Преобразовываем сигнал выхода со сцены, чтобы он возвращал значение GameplayExitParams
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
        return exitToMainMenuSceneSignal;
    }

    private MainMenuExitParams CreateExitParams()
    {
        // Создаем\конфигурируем параметры выхода с текущей сцены
        var gameplayEnterParams = new GameplayEnterParams("large.save", 228);     // Magic
        var exitParams = new MainMenuExitParams(gameplayEnterParams);
        return exitParams;
    }

    // Можно выделить в шаблон (в GameplayEntryPoint похожая функция)
    private void CreateUISceneBinder()        // Создаем UIGameplayRootBinder
    {
        var uiScenePrefab = Resources.Load<UIMainMenuRootBinder>(GameConstants.UIMainMenuFullPath);
        _uiScene = Instantiate(uiScenePrefab);
        var uiRoot = _sceneContainer.Resolve<UIRootView>();
        uiRoot.AttachSceneUI(_uiScene.gameObject);
    }
}
