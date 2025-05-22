using R3;

public class GameplayUIManager : UIManager
{
    private readonly Subject<Unit> _exitSceneRequest;

    public GameplayUIManager(DIContainer container) : base(container)
    {
        _exitSceneRequest = container.Resolve<Subject<Unit>>(GameConstants.ExitSceneRequestTag);
    }

    public ScreenGameplayViewModel OpenScreenGameplay()
    {
        var viewModel = new ScreenGameplayViewModel(this, _exitSceneRequest);
        var rootUI = Container.Resolve<UIGameplayRootViewModel>();  // Вызов тут, потому как в конструкторе еще неизвестно, создан или нет UIGameplayRootViewModel

        rootUI.OpenScreen(viewModel);

        return viewModel;
    }

    public PopupAViewModel OpenPopupA()     // Похож на OpenPopupB
    {
        var popupA = new PopupAViewModel();
        var rootUI = Container.Resolve<UIGameplayRootViewModel>();  // Вызов тут, потому как в конструкторе еще неизвестно, создан или нет UIGameplayRootViewModel

        rootUI.OpenPopup(popupA);

        return popupA;
    }

    public PopupBViewModel OpenPopupB()     // Похож на OpenPopupA
    {
        var popupB = new PopupBViewModel();
        var rootUI = Container.Resolve<UIGameplayRootViewModel>();  // Вызов тут, потому как в конструкторе еще неизвестно, создан или нет UIGameplayRootViewModel

        rootUI.OpenPopup(popupB);

        return popupB;
    }
}