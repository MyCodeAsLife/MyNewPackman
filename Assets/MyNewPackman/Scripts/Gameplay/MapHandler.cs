using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.MyPackman.Presenter
{
    public class MapHandler : IMapHandler       // Разделить на ModelMapHandler и PresenterMapHandler и связать через шину событий
    {
        private ILevelConfig _currentLevel;       // DI - ? через interface
        private Tilemap _wallsTileMap;          // DI - ?
        private Tilemap _pelletsTilemap;        // DI - ?
        private Tile[] _walls;                  // DI - ?

        public MapHandler(Tilemap wallsTileMap, Tilemap pelletsTilemap, Tile[] walls, ILevelConfig level)
        {
            _wallsTileMap = wallsTileMap;
            _pelletsTilemap = pelletsTilemap;
            _walls = walls;
            _currentLevel = level;
        }

        // Передовать класс(по типу патерна комманда) в котором будет указан нужный tilemap, нужный массив тайлов и номер тайла
        public void ChangeTile(Vector3 position, int objectNumber)                       // Остановился здесь+++++++++++++++++++++++
        {
            var handlePosition = ConvertToCellPosition(position);

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

        private Vector3Int ConvertToCellPosition(Vector3 position)
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

        public bool IsIntersaction(int x, int y)       // Вынести в mapHandler
        {
            int numberOfPaths = 0;
            int vertical = 0;
            int horizontal = 0;
            int maxLengthY = _currentLevel.Map.GetLength(0);
            int maxLengthX = _currentLevel.Map.GetLength(1);

            int upX = x - 1;
            int downX = x + 1;
            int leftY = y - 1;
            int rightY = y + 1;

            if (upX >= 0 && (_currentLevel.Map[y, upX] == GameConstants.PelletTile ||
                             _currentLevel.Map[y, upX] == GameConstants.EmptyTile))
            {
                numberOfPaths++;
                vertical++;
            }

            if (downX < maxLengthX && (_currentLevel.Map[y, downX] == GameConstants.PelletTile ||
                                       _currentLevel.Map[y, downX] == GameConstants.EmptyTile))
            {
                numberOfPaths++;
                vertical--;
            }

            if (leftY >= 0 && (_currentLevel.Map[leftY, x] == GameConstants.PelletTile ||
                               _currentLevel.Map[leftY, x] == GameConstants.EmptyTile))
            {
                numberOfPaths++;
                horizontal++;
            }

            if (rightY < maxLengthY && (_currentLevel.Map[rightY, x] == GameConstants.PelletTile ||
                                        _currentLevel.Map[rightY, x] == GameConstants.EmptyTile))
            {
                numberOfPaths++;
                horizontal--;
            }

            return numberOfPaths > 2 || horizontal != 0 || vertical != 0 ? true : false;                                    //Magic
        }
    }
}
