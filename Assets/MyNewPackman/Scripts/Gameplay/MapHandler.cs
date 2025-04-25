using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.MyPackman.Presenter
{
    public class MapHandler : IMapHandler       // Разделить на ModelMapHandler и PresenterMapHandler и связать через шину событий
    {
        private ILevelData _currentLevel;       // DI - ? через interface
        private Tilemap _wallsTileMap;          // DI - ?
        private Tilemap _pelletsTilemap;        // DI - ?
        private Tile[] _walls;                  // DI - ?

        public MapHandler(Tilemap wallsTileMap, Tilemap pelletsTilemap, Tile[] walls, ILevelData level)
        {
            _wallsTileMap = wallsTileMap;
            _pelletsTilemap = pelletsTilemap;
            _walls = walls;
            _currentLevel = level;
        }

        //public int GetTile(Vector3Int position) => _currentLevel.Map[position.y, position.x];

        // Передовать класс(по типу патерна комманда) в котором будет указан нужный tilemap, нужный массив тайлов и номер тайла
        public void ChangeTile(Vector3 position, int objectNumber)                       // Остановился здесь+++++++++++++++++++++++
        {
            Debug.Log(objectNumber);                                                                //++++++++++++++++++++++++++
            var handlePosition = HandleCoordinates(position);

            _currentLevel.Map[handlePosition.y, handlePosition.x] = objectNumber;                       // Изменяет Модель

            if (objectNumber > 0)
            {
                _wallsTileMap.SetTile(new Vector3Int(handlePosition.x, -handlePosition.y), null);        // Изменяет Presenter
            }
            else
            {
                _pelletsTilemap.SetTile(new Vector3Int(handlePosition.x, -handlePosition.y), null);
            }
        }

        private Vector3Int HandleCoordinates(Vector3 position)
        {
            var pos = position;

            var newX = Mathf.Abs((int)pos.x - pos.x);
            var newY = Mathf.Abs((int)pos.y - pos.y);

            int X = (int)pos.x;
            int Y = Mathf.Abs((int)pos.y - 1);

            if (newX < 0.4f)
                X -= 1;
            else if (newX > 0.6f)
                X += 1;

            if (newY < 0.4f)
                Y -= 1;
            else if (newY > 0.6f)
                Y += 1;

            return new Vector3Int(X, Y);
        }

        //public bool TryFindPositionByObjectNumber(int number, ref Vector3Int position)
        //{
        //    Vector3Int tileNumber = Vector3Int.zero;

        //    for (int y = 0; y < _currentLevel.Map.GetLength(0); y++)
        //    {
        //        for (int x = 0; x < _currentLevel.Map.GetLength(1); x++)
        //        {
        //            if (_currentLevel.Map[y, x] == number)
        //            {
        //                position = new Vector3Int(x, y);
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}
    }
}
