using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.MyPackman.Presenter
{
    public class MapHandler : IMapHandler       // Разделить на ModelMapHandler и PresenterMapHandler и связать через шину событий
    {
        private ILevelData _currentLevel;       // DI - ? через interface
        private Tilemap _wallsTileMap;          // DI - ?
        private Tile[] _walls;                  // DI - ?

        public MapHandler(Tilemap wallsTileMap, Tile[] walls, ILevelData map)
        {
            _wallsTileMap = wallsTileMap;
            _walls = walls;
            _currentLevel = map;
        }

        public int Tile(Vector3Int position) => _currentLevel.Map[position.y, position.x];

        // Передовать класс(по типу патерна комманда) в котором будет указан нужный tilemap, нужный массив тайлов и номер тайла
        public void ChangeTile(Vector3Int position, int objectNumber)                       // Через шину событий?
        {
            _currentLevel.Map[position.y, position.x] = objectNumber;                                // Изменяет Модель
            var tile = objectNumber > 0 ? _walls[objectNumber] : null;
            _wallsTileMap.SetTile(new Vector3Int(position.x, -position.y), tile);           // Изменяет Presenter
        }

        public bool TryFindPositionByObjectNumber(int number, ref Vector3Int position)
        {
            Vector3Int tileNumber = Vector3Int.zero;

            for (int y = 0; y < _currentLevel.Map.GetLength(0); y++)
            {
                for (int x = 0; x < _currentLevel.Map.GetLength(1); x++)
                {
                    if (_currentLevel.Map[y, x] == number)
                    {
                        position = new Vector3Int(x, y);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
