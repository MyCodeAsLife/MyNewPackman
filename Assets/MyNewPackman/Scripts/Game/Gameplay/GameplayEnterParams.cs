public class GameplayEnterParams : SceneEnterParams
{
    public GameplayEnterParams(string saveFileName, int mapId) : base(GameConstants.Gameplay)
    {
        SaveFileName = saveFileName;
        MapId = mapId;
    }

    public string SaveFileName { get; } // Для загрузки "уровня"
    public int MapId { get; }     // Номер уровня для создания из "префаба"
}
