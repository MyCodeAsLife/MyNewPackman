using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelConstructor
{
    private readonly DIContainer _sceneContainer;
    private readonly Tilemap _wallsTileMap;                 // Получать через DI ?
    private readonly Tilemap _pelletsTileMap;               // Получать через DI ?
    private readonly Tile[] _walls;                         // Получать через DI ?
    private readonly ILevelData _level;                     // Получать через DI ?
    private readonly RuleTile[] _rulePellets;

    public LevelConstructor(DIContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
        _wallsTileMap = sceneContainer.Resolve<Tilemap>(GameConstants.Obstacle);
        _pelletsTileMap = sceneContainer.Resolve<Tilemap>(GameConstants.Pellet);
        _walls = LoadTiles(GameConstants.WallTilesFolderPath, GameConstants.WallTilesCount);
        _rulePellets = LoadRuleTiles(GameConstants.PelletRuleTilesFolderPath, GameConstants.PelletTilesCount);
        _level = sceneContainer.Resolve<ILevelData>();                                                      // Получать от MainMenu? при загрузке сцены
        _sceneContainer.RegisterInstance(new MapHandler(_wallsTileMap, _pelletsTileMap, _walls, _level));   // Создание классов вынести в DI?
    }

    public void ConstructLevel()        // Передовать команды в MapHendler, чтобы только он менял Tilemap?
    {
        _wallsTileMap.ClearAllTiles();

        for (int y = 0; y < _level.Map.GetLength(0); y++)                                               // Magic
        {
            for (int x = 0; x < _level.Map.GetLength(1); x++)                                           // Magic
            {
                if (_level.Map[y, x] > -1)                                                              // Magic
                    _wallsTileMap.SetTile(new Vector3Int(x, -y), _walls[_level.Map[y, x]]);
                else if (_level.Map[y, x] == -1)                                                        // Magic
                    SpawnPacman(x, y);
                else if (_level.Map[y, x] == -4)                                                        // Magic
                {
                    if (IsIntersaction(x, y))
                    {
                        int chance = Random.Range(0, 100);

                        if (chance < 10)                                                                // Magic
                            _pelletsTileMap.SetTile(new Vector3Int(x, -y), _rulePellets[2]);            // Magic
                        else
                            _pelletsTileMap.SetTile(new Vector3Int(x, -y), _rulePellets[1]);            // Magic
                    }
                    else
                    {
                        _pelletsTileMap.SetTile(new Vector3Int(x, -y), _rulePellets[0]);                // Magic
                    }
                }
            }
        }
    }

    private Tile[] LoadTiles(string folderPath, int count)
    {
        Tile[] tiles = new Tile[count];

        for (int tileName = 0; tileName < count; tileName++)
            tiles[tileName] = Resources.Load<Tile>($"{folderPath}{tileName}");

        return tiles;
    }

    private RuleTile[] LoadRuleTiles(string folderPath, int count)
    {
        RuleTile[] ruleTiles = new RuleTile[count];

        for (int tileName = 0; tileName < count; tileName++)
            ruleTiles[tileName] = Resources.Load<RuleTile>($"{folderPath}{tileName}");

        return ruleTiles;
    }

    private void SpawnPacman(float x, float y)
    {
        float newX = x * GameConstants.GridCellSize + GameConstants.GridCellSize * GameConstants.Half;
        float newY = -(y * GameConstants.GridCellSize - GameConstants.GridCellSize * GameConstants.Half);
        var newPosition = new Vector3(newX, newY);

        var player = _sceneContainer.Resolve<Pacman>();
        player.transform.position = newPosition;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);
        player.Initialize(_sceneContainer.Resolve<MapHandler>());

        _sceneContainer.Resolve<MapHandler>().ChangeTile(new Vector3(x, y), GameConstants.EmptyTile);
    }

    private bool IsIntersaction(int x, int y)
    {
        int numberOfPaths = 0;
        int maxLengthY = _level.Map.GetLength(0);
        int maxLengthX = _level.Map.GetLength(1);

        int upX = x - 1;
        int downX = x + 1;
        int leftY = y - 1;
        int rightY = y + 1;

        if (upX >= 0 && (_level.Map[y, upX] == GameConstants.PelletTile || _level.Map[y, upX] == GameConstants.EmptyTile))
            numberOfPaths++;

        if (downX < maxLengthX && (_level.Map[y, downX] == GameConstants.PelletTile || _level.Map[y, downX] == GameConstants.EmptyTile))
            numberOfPaths++;

        if (leftY >= 0 && (_level.Map[leftY, x] == GameConstants.PelletTile || _level.Map[leftY, x] == GameConstants.EmptyTile))
            numberOfPaths++;

        if (rightY < maxLengthY && (_level.Map[rightY, x] == GameConstants.PelletTile || _level.Map[rightY, x] == GameConstants.EmptyTile))
            numberOfPaths++;

        return numberOfPaths > 2 ? true : false;                                    //Magic
    }
}
