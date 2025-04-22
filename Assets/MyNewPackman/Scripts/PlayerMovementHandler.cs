using Assets.MyPackman.Settings;
using System;
using UnityEngine;

namespace Assets.MyPackman.Presenter
{
    public class PlayerMovementHandler : IPlayerMovementHandler
    {
        private Rigidbody2D _rigidbody;
        private IMapHandler _mapHandler;                                    // DI - ?
        private Func<Vector2> _getDirection;

        private event Action Moved;                                  // Вынести в шину событий?

        public PlayerMovementHandler(Rigidbody2D rigidbody, IMapHandler handler, MonoBehaviour parent)
        {
            _rigidbody = rigidbody;
            _mapHandler = handler;
        }

        public void Tick()
        {
            Moved?.Invoke();
        }

        public void Move()
        {
            Vector2 currentDirection = _getDirection();

            currentDirection.x = Mathf.Round(currentDirection.x);
            currentDirection.y = Mathf.Round(currentDirection.y);


            float posX = _rigidbody.position.x + (ConstantsGame.PlayerSpeed * Time.fixedDeltaTime * currentDirection.x);
            float posY = _rigidbody.position.y + (ConstantsGame.PlayerSpeed * Time.fixedDeltaTime * currentDirection.y);
            var newPosition = new Vector3(posX, posY, 0);
            _rigidbody.MovePosition(newPosition);
        }

        public void StartMoving()
        {
            Moved += Move;
        }

        public void StopMoving()
        {
            Moved -= Move;
        }

        public void Initialyze(Func<Vector2> getDirection)
        {
            _getDirection = getDirection;
        }

        public void HandleCollision(Vector3 position)
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

            var newPosition = new Vector3Int(X, Y);
            Debug.Log(newPosition);
            _mapHandler.ChangeTile(newPosition, ConstantsGame.EmptyTile);
        }
    }
}
