using R3;
using System;
using System.Linq;

// ����������� � DI ��������� ���� �������� ����������� ��� ����� Gameplay
public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        // ��������� ��������
        var gameSettings = container.Resolve<ISettingsProvider>().GameSettings;
        //var buildings = gameStateProvider.GameState.Maps;

        // ������������ ������ ������������� ����� � MainMenu
        container.RegisterInstance(GameConstants.ExitSceneRequestTag, new Subject<Unit>());

        // �������� � ����������� ����������\����������� ������
        var cmd = new CommandProcessor(gameStateProvider);
        container.RegisterInstance<ICommandProcessor>(cmd);
        // ����������� ������������ ������
        //cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));                  // ������� ��������\���������� ��������
        cmd.RegisterHandler(new CmdCreateMapHandler(gameStateProvider.GameState, gameSettings));   // �������� �������� �����
        cmd.RegisterHandler(new CmdResourcesAddHandler(gameStateProvider.GameState));                   // ��������\���������� �������
        cmd.RegisterHandler(new CmdResourcesSpendHandler(gameStateProvider.GameState));                 // ����� �������

        // �� ������ ������ �� �����, ��� �� �������� ��������� �����. �� �� �����, ���� �� �� ��������� ������.
        // �������� ����� - ��� ������, ��� ��� �������� � ���� ����� ����� �������, ������� ����� ���������� ������
        // �� ������, ���� ��������� ����� ��� �� ����������. ���� ���� ������ ���������� �����, ����� ���������
        // ����� ����������� �� �������� ����� � ��� ������ �������� ��������, �� ���� ���.
        var loadingMapId = gameplayEnterParams.MapId;
        var loadingMap = gameStateProvider.GameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);

        if (loadingMap == null)
        {
            // ���� ����� �� �������, ������� �
            var command = new CmdCreateMap(loadingMapId); // ��������� ����� ��������� � gameStateProvider.GameState.Maps
            bool success = cmd.Process(command);

            if (success == false)
            {
                throw new Exception($"Couldn't create map state with id: {loadingMapId}");
            }

            loadingMap = gameStateProvider.GameState.Maps.First(m => m.Id == loadingMapId); // �������� ��������� �����
        }

        //// ����������� ������� �������� ������� ��������(��������\�����������\��������)
        //container.RegisterFactory(_ => new BuildingsService(
        //        loadingMap.Buildings,
        //        gameSettings.BuildingsSettings,
        //        cmd))
        //    .AsSingle();
        // ����������� ������� �������� ������� �� ������ � ���������(��������, ����������, �����, �������� �� ��������� � �.�.)
        container.RegisterFactory(_ => new ResourcesService(gameStateProvider.GameState.Resources, cmd)).AsSingle();
    }
}
