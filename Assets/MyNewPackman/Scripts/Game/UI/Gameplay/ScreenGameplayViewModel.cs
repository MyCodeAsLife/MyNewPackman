using R3;

public class ScreenGameplayViewModel : WindowViewModel
{
    private readonly GameplayUIManager _uiManager;
    private readonly Subject<Unit> _exitSceneRequest;

    public ScreenGameplayViewModel(GameplayUIManager uiManager, Subject<Unit> exitSceneRequest)
    {
        _uiManager = uiManager;
        _exitSceneRequest = exitSceneRequest;
    }

    public override string Id => "ScreenGameplay";

    public void RequestOpenPopupA()
    {
        _uiManager.OpenPopupA();
    }

    public void RequestOpenPopupB()
    {
        _uiManager.OpenPopupB();
    }

    public void RequestGoToMainMenu()
    {
        _exitSceneRequest.OnNext(Unit.Default);
    }
}