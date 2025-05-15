// ����������� � DI ��������� ���� �������� ����������� ��� ����� Gameplay
using System;
using System.Linq;

public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        // ��������� ��������
        var gameSettings = container.Resolve<ISettingsProvider>().GameSettings;
        //var buildings = gameStateProvider.GameState.Maps;
        // �������� � ����������� ����������\����������� ������
        var cmd = new CommandProcessor(gameStateProvider);
        container.RegisterInstance<ICommandProcessor>(cmd);
        // ����������� ������������ ������
        cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));                  // ������� ��������\���������� ��������
        cmd.RegisterHandler(new CmdCreateMapStateHandler(gameStateProvider.GameState, gameSettings));   // �������� �������� �����


        // �� ������ ������ �� �����, ��� �� �������� ��������� �����. �� �� �����, ���� �� �� ��������� ������.
        // �������� ����� - ��� ������, ��� ��� �������� � ���� ����� ����� �������, ������� ����� ���������� ������
        // �� ������, ���� ��������� ����� ��� �� ����������. ���� ���� ������ ���������� �����, ����� ���������
        // ����� ����������� �� �������� ����� � ��� ������ �������� ��������, �� ���� ���.
        var loadingMapId = gameplayEnterParams.MapId;
        var loadingMap = gameStateProvider.GameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);

        if (loadingMap == null)
        {
            // ���� ����� �� �������, ������� �
            var command = new CmdCreateMapState(loadingMapId); // ��������� ����� ��������� � gameStateProvider.GameState.Maps
            bool success = cmd.Process(command);

            if (success == false)
            {
                throw new Exception($"Couldn't create map state with id: {loadingMapId}");
            }

            loadingMap = gameStateProvider.GameState.Maps.First(m => m.Id == loadingMapId); // �������� ��������� �����
        }

        // ����������� ������� �������� ������� ��������(��������\�����������\��������)
        container.RegisterFactory(_ => new BuildingsService(loadingMap.Buildings, gameSettings.BuildingsSettings, cmd)).AsSingle();
    }
}
