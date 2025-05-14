// Регистрация в DI контейнер всех сервисов необходимых для сцены Gameplay
public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        var buildings = gameStateProvider.GameState.Buildings;
        // Создание и регистрация процессора\обработчика команд
        var cmd = new CommandProcessor(gameStateProvider);
        container.RegisterInstance<ICommandProcessor>(cmd);
        // Регистрация обработчика команды создания\размещения строения
        cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));
        // Получение настроек строений
        var buildingsSettings = container.Resolve<ISettingsProvider>().GameSettings.BuildingsSettings;
        // Регистрация фабрики создания сервиса строений(создание\перемещение\удаление)
        container.RegisterFactory(_ => new BuildingsService(buildings, buildingsSettings, cmd)).AsSingle();
    }
}
