// Регистрация в DI контейнер всех ViewModel необходимых для сцены Gameplay
public static class GameplayViewModelRegistartions
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIGameplayRootViewModel(c.Resolve<SomeGameplayService>())).AsSingle();
        container.RegisterFactory(c => new WorldGameplayRootViewModel()).AsSingle();
    }
}
