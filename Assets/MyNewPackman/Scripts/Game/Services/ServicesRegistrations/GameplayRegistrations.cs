// ����������� � DI ��������� ���� �������� ����������� ��� ����� Gameplay
public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        var gameStateProvider = container.Resolve<IGameStateProvider>();
        var buildings = gameStateProvider.GameState.Buildings;
        // �������� � ����������� ����������\����������� ������
        var cmd = new CommandProcessor(gameStateProvider);
        container.RegisterInstance<ICommandProcessor>(cmd);
        // ����������� ����������� ������� ��������\���������� ��������
        cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameStateProvider.GameState));
        // ��������� �������� ��������
        var buildingsSettings = container.Resolve<ISettingsProvider>().GameSettings.BuildingsSettings;
        // ����������� ������� �������� ������� ��������(��������\�����������\��������)
        container.RegisterFactory(_ => new BuildingsService(buildings, buildingsSettings, cmd)).AsSingle();
    }
}
