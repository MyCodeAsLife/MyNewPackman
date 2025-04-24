using Assets.MyPackman.Settings;
using System;
using UnityEngine;

namespace Assets.MyPackman.Presenter
{
    public class PlayerMovementHandler : IPlayerMovementHandler
    {
        private Rigidbody2D _rigidbody;
        private Func<Vector2> _getDirection;

        private event Action Moved;                                  // Вынести в шину событий?

        public PlayerMovementHandler(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
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


            float posX = _rigidbody.position.x + (GameConstants.PlayerSpeed * Time.fixedDeltaTime * currentDirection.x);
            float posY = _rigidbody.position.y + (GameConstants.PlayerSpeed * Time.fixedDeltaTime * currentDirection.y);
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
    }
}
