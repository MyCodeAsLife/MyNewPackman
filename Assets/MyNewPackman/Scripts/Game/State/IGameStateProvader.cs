using R3;

// ��������� ��� ����������� ��������\���������� (� ����, � ������, � ���� ������ � �.�.)
public interface IGameStateProvader
{
    public GameStateProxy GameState { get; }

    // ���������� Observable ����� ��������\���������� ����� ���� ���������.
    public Observable<GameStateProxy> LoadGameState();
    public Observable<bool> SaveGameState();
    public Observable<bool> ResetGameState();
}
