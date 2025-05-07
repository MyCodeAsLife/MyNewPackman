// ����������� � DI ��������� ���� �������� ����������� ��� ����� Gameplay
public static class GameplayRegistrations
{
    public static void Register(DIContainer container, GameplayEnterParams gameplayEnterParams)
    {
        container.RegisterFactory(
            c => new SomeGameplayService(
                c.Resolve<IGameStateProvader>().GameState, 
                c.Resolve<SomeCommonService>())
            ).AsSingle();
    }
}
