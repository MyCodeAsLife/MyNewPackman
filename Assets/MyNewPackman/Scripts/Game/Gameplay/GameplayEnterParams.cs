public class GameplayEnterParams : SceneEnterParams
{
    public GameplayEnterParams(string saveFileName, int levelNumber) : base(GameConstants.Gameplay)
    {
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }

    public string SaveFileName { get; } // Для загрузки "уровня"
    public int LevelNumber { get; }     // Номер уровня для создания из "префаба"
}
