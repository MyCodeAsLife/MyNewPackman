public class GameplayEnterParams : SceneEnterParams
{
    public GameplayEnterParams(string saveFileName, int mapId) : base(GameConstants.Gameplay)
    {
        SaveFileName = saveFileName;
        MapId = mapId;
    }

    public string SaveFileName { get; } // ��� �������� "������" �� ����������
    public int MapId { get; }     // ����� ������ ��� �������� �� "�������"
}
