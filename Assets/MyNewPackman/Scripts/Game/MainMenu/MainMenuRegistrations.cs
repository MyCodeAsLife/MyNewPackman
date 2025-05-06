// Регистрация в DI контейнер всех сервисов необходимых для сцены MainMenu
public static class MainMenuRegistrations
{
    public static void Register(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
    {
        container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
    }
}
