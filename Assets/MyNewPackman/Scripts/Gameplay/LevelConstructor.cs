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
    private readonly Tile[] _pellets;
    private readonly ILevelData _map;                         // Получать через DI ?

    public LevelConstructor(DIContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
        _wallsTileMap = sceneContainer.Resolve<Tilemap>(GameConstants.Obstacle);
        _pelletsTileMap = sceneContainer.Resolve<Tilemap>(GameConstants.Pellet);
        _walls = LoadResources(GameConstants.WallTilesFolderPath, GameConstants.WallTilesCount);
        _pellets = LoadResources(GameConstants.PelletTilesFolderPath, GameConstants.PelletTilesCount);
        _map = sceneContainer.Resolve<ILevelData>();                                                          // Получать от MainMenu? при загрузке сцены
        _sceneContainer.RegisterInstance(new MapHandler(_wallsTileMap, _walls, _map));                      // Создание классов вынести в DI?
    }

    public void ConstructLevel()
    {
        _wallsTileMap.ClearAllTiles();

        for (int y = 0; y < _map.Map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.Map.GetLength(1); x++)
            {
                if (_map.Map[y, x] > -1)
                    _wallsTileMap.SetTile(new Vector3Int(x, -y), _walls[_map.Map[y, x]]);
                else if (_map.Map[y, x] == -1)
                    SpawnPacman(x, y);
                else if (_map.Map[y, x] == -4)
                    _pelletsTileMap.SetTile(new Vector3Int(x, -y), _pellets[0]);
            }
        }
    }

    private Tile[] LoadResources(string folderPath, int count)
    {
        Tile[] walls = new Tile[count];

        for (int tileName = 0; tileName < count; tileName++)
            walls[tileName] = Resources.Load<Tile>($"{folderPath}{tileName}");

        return walls;
    }

    private void SpawnPacman(float x, float y)
    {
        float newX = x * GameConstants.GridCellSize + GameConstants.GridCellSize / 2;
        float newY = -(y * GameConstants.GridCellSize - GameConstants.GridCellSize / 2);
        var newPosition = new Vector3(newX, newY);

        var player = _sceneContainer.Resolve<Pacman>();
        player.transform.position = newPosition;
        player.transform.rotation = Quaternion.identity;
        player.gameObject.SetActive(true);
        player.Initialize(_sceneContainer.Resolve<MapHandler>());
    }
}
