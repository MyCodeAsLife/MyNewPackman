using R3;
using System;
using System.Linq;

// Регистрация в DI контейнер всех сервисов необходимых для сцены Gameplay
public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        // Получение настроек
        var gameSettings = container.Resolve<ISettingsProvider>().GameSettings;
        //var buildings = gameStateProvider.GameState.Maps;

        // Регистрируем сигнал запрашивающий выход в MainMenu
        container.RegisterInstance(GameConstants.ExitSceneRequestTag, new Subject<Unit>());

        // Создание и регистрация процессора\обработчика команд
        var cmd = new CommandProcessor(gameStateProvider);
        container.RegisterInstance<ICommandProcessor>(cmd);
        // Регистрация обработчиков команд
        //cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));                  // Команда создания\размещения строения
        cmd.RegisterHandler(new CmdCreateMapHandler(gameStateProvider.GameState, gameSettings));   // Комманда создания карты
        cmd.RegisterHandler(new CmdResourcesAddHandler(gameStateProvider.GameState));                   // Создание\добавление ресурса
        cmd.RegisterHandler(new CmdResourcesSpendHandler(gameStateProvider.GameState));                 // Трата ресурса

        // На данный момент мы знаем, что мы пытаемся загрузить карту. Но не знаем, есть ли ее состояние вообще.
        // Создание карты - это модель, так что работать с сней нужно через команды, поэтому нужен обработчик команд
        // на случай, если состояние карты еще не существует. Надо этот момент переделать потом, чтобы состояние
        // карты создовалось ДО загрузки сцены и тут небыло подобных проверок, но пока так.
        var loadingMapId = gameplayEnterParams.MapId;
        var loadingMap = gameStateProvider.GameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);

        if (loadingMap == null)
        {
            // Если карта не нашлась, создаем её
            var command = new CmdCreateMap(loadingMapId); // Созданная карта положится в gameStateProvider.GameState.Maps
            bool success = cmd.Process(command);

            if (success == false)
            {
                throw new Exception($"Couldn't create map state with id: {loadingMapId}");
            }

            loadingMap = gameStateProvider.GameState.Maps.First(m => m.Id == loadingMapId); // Вынимаем созданную карту
        }

        //// Регистрация фабрики создания сервиса строений(создание\перемещение\удаление)
        //container.RegisterFactory(_ => new BuildingsService(
        //        loadingMap.Buildings,
        //        gameSettings.BuildingsSettings,
        //        cmd))
        //    .AsSingle();
        // Регистрация фабрики создания сервиса по работе с ресурсами(создание, добавление, трата, подписка на изменение и т.д.)
        container.RegisterFactory(_ => new ResourcesService(gameStateProvider.GameState.Resources, cmd)).AsSingle();
    }
}
