using System;
using UnityEngine;

namespace Assets.MyPackman.Presenter
{
    public interface IPlayerMovementHandler
    {
        public void Tick();
        public void Move();
        public void StartMoving();
        public void StopMoving();
        public void Initialyze(Func<Vector2> getDirection);
        public void HandleCollision(Vector3 position);
    }
}
