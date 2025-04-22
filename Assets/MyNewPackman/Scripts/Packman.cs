using Assets.MyPackman.Model;
using Assets.MyPackman.Presenter;
using UnityEngine;
using UnityEngine.InputSystem;

public class Packman : MonoBehaviour
{
    private PlayerInputActions _inputActions;                       // Все сопутствующее вынести в класс PlayerInputController
    private IPlayerMovementHandler _playerMoveHandler;                                                          // DI - ? через interface

    private void OnEnable()
    {
        var mapHandler = FindFirstObjectByType<LevelPresenter>().MapHandler;
        _playerMoveHandler = new PlayerMovementHandler(transform.GetComponent<Rigidbody2D>(), mapHandler, this);        // Создание классов вынести в DI?
        _playerMoveHandler.Initialyze(() => _inputActions.Keyboard.Movement.ReadValue<Vector2>());
        _inputActions = new PlayerInputActions();                                                                                   // Создание классов вынести в DI?
        _inputActions.Enable();
        _inputActions.Keyboard.Movement.started += OnMoveStarted;
        _inputActions.Keyboard.Movement.canceled += OnMoveCanceled;
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StartMoving();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StopMoving();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Keyboard.Movement.performed -= OnMoveStarted;
    }

    private void Update()
    {
        _playerMoveHandler.Tick();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Обрабатывать столкновения по слою
        Debug.Log(collision.gameObject.layer);

        _playerMoveHandler.HandleCollision(transform.position);
    }
}
