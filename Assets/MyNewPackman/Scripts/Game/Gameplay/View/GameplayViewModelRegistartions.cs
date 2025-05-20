// Регистрация в DI контейнер все ViewModel-инициализаторов, необходимых для сцены Gameplay
public static class GameplayViewModelRegistartions
{
    public static void Register(DIContainer container)
    {
        container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
        container.RegisterFactory(c => new WorldGameplayRootViewModel(
                c.Resolve<BuildingsService>(),
                c.Resolve<ResourcesService>()))
            .AsSingle();
    }
}
