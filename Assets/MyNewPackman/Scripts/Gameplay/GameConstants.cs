namespace Assets.MyPackman.Settings
{
    public static class GameConstants
    {
        //General
        public const float Half = 0.5f;

        // Layers and Tilemaps
        public const string Obstacle = nameof(Obstacle);
        public const string Pellet = nameof(Pellet);

        // Resources path
        public const string NoFrictionFullPath = "NoFriction";
        public const string WallTilesFolderPath = "WallTiles/";
        public const string PelletRuleTilesFolderPath = "PelletRuleTiles/";

        // Number of tiles along the specified path
        public const int WallTilesCount = 38;
        public const int PelletTilesCount = 3;

        // Map settings
        public const float GridCellSize = 1f;
        public const int GridCellPixelSize = 24;

        //Pellets name
        public const string PelletSmall = "PelletSmall(Clone)";
        public const string PelletMedium = "PelletMedium(Clone)";
        public const string PelletLarge = "PelletLarge(Clone)";

        // Packman settings
        //public const int NoDirection = -1;
        //public const int LeftDirection = 0;
        //public const int RightDirection = 1;
        //public const int UpDirection = 2;
        //public const int DownDirection = 3;
        public const float PlayerSpeed = 4f;

        //Level Constructor - переделать в enum?
        public const int EmptyTile = 0;
        public const int PelletTile = -4;
        public const int PacmanSpawn = -1;

        // Errors
        public const string PositionOnMapNotFound = "Position on map, not Found.";

        // UI and Camera
        public const float GameplayInformationalPamelHeight = 3f;
    }
}
