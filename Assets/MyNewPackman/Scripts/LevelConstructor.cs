using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.MyPackman.Presenter
{
    public class LevelConstructor
    {
        private Tilemap _wallsTileMap;                      // Получать через DI ?
        private Tilemap _pelletsTileMap;                      // Получать через DI ?
        private Tile[] _walls;                     // Получать через DI ?
        private Tile[] _pellets;
        private Packman _player;                      // Получать через DI ?
        private MapModel _map;                        // Получать через DI ?

        public LevelConstructor(Tilemap wallsTileMap, Tilemap pelletsTileMap, Tile[] walls, Tile[] pellets, MapModel map)
        {
            _player = Resources.Load<Packman>("Prefabs/Pacman");

            _wallsTileMap = wallsTileMap;
            _pelletsTileMap = pelletsTileMap;
            _walls = walls;
            _pellets = pellets;
            _map = map;
            _wallsTileMap.ClearAllTiles();

            ConstructLevel();
        }

        private void ConstructLevel()
        {
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

        private void SpawnPacman(float x, float y)
        {
            float newX = x * ConstantsGame.GridCellSize + ConstantsGame.GridCellSize / 2;
            float newY = -(y * ConstantsGame.GridCellSize - ConstantsGame.GridCellSize / 2);
            var newPosition = new Vector3(newX, newY);

            var player = Object.Instantiate(_player, newPosition, Quaternion.identity);
            player.gameObject.SetActive(true);
        }
    }
}
