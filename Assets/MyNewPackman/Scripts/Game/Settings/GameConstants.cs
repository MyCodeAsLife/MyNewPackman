public static class GameConstants
{
    // General
    public const float Half = 0.5f;

    // Scene names
    public const string Boot = nameof(Boot);
    public const string MainMenu = nameof(MainMenu);
    public const string Gameplay = nameof(Gameplay);

    // Layers and Tilemaps
    public const string Obstacle = nameof(Obstacle);
    public const string Pellet = nameof(Pellet);
    public const string Node = nameof(Node);

    // Paths to prefabs
    public const string GhostFullPath = "Prefabs/Ghost";
    public const string PacmanFullPath = "Prefabs/Pacman";
    public const string NoFrictionFullPath = "NoFriction";
    public const string WallTilesFolderPath = "WallTiles/";
    public const string NodeRuleTileFolderPath = "NodeRuleTile/";
    public const string PelletRuleTilesFolderPath = "PelletRuleTiles/";
    public const string UIRootViewFullPath = "Prefabs/UI/RootViewUI";
    public const string UIMainMenuFullPath = "Prefabs/UI/MainMenuUI";
    public const string UIGameplayFullPath = "Prefabs/UI/GameplayUI";

    // Number of tiles along the specified path
    public const int NumberOfWallTiles = 38;
    public const int NumberOfPelletTiles = 3;
    public const int NumberOfNodeTiles = 1;

    // Map settings
    public const float GridCellSize = 1f;
    public const int GridCellPixelSize = 24;

    // Triggers object names
    public const string TriggerPelletSmall = "PelletSmall(Clone)";
    public const string TriggerPelletMedium = "PelletMedium(Clone)";
    public const string TriggerPelletLarge = "PelletLarge(Clone)";
    public const string TriggerNode = "Node(Clone)";

    // Score cost
    public const int CostPelletSmall = 5;
    public const int CostPelletMedium = 15;
    public const int CostPelletLarge = 50;

    // Packman settings
    public const float PlayerSpeed = 4f;

    // Level Constructor - переделать в enum?
    public const int EmptyTile = 0;
    public const int PelletTile = -4;
    public const int PacmanSpawn = -1;

    // Errors
    public const string PositionOnMapNotFound = "Position on map, not Found.";

    // UI and Camera
    public const float GameplayInformationalPamelHeight = 3f;
}
