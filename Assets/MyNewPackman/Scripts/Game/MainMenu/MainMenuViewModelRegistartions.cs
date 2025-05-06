// Регистрация в DI контейнер всех ViewModel необходимых для сцены MainMenu
public static class MainMenuViewModelRegistartions
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(_ => new UIMainMenuRootViewModel()).AsSingle();
    }
}
