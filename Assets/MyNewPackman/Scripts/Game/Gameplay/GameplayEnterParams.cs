public class GameplayEnterParams : SceneEnterParams
{
    public GameplayEnterParams(string saveFileName, int levelNumber) : base(GameConstants.Gameplay)
    {
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }

    public string SaveFileName { get; } // ��� �������� "������"
    public int LevelNumber { get; }     // ����� ������ ��� �������� �� "�������"
}
