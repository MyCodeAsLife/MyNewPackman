using Assets.MyPackman.Model;
using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pacman : MonoBehaviour
{
    private int _pelletLayer; 

    private PlayerInputActions _inputActions;                       // Все сопутствующее вынести в класс PlayerInputController
    private IPlayerMovementHandler _playerMoveHandler;                                                          // DI - ? через interface
    private IMapHandler _mapHandler;

    private void Awake()
    {
        _pelletLayer = LayerMask.NameToLayer("Pellet");
    }

    private void OnEnable()
    {
        _mapHandler = FindFirstObjectByType<LevelPresenter>().MapHandler;
        _playerMoveHandler = new PlayerMovementHandler(transform.GetComponent<Rigidbody2D>()/*, _mapHandler, this*/);        // Создание классов вынести в DI?
        _playerMoveHandler.Initialyze(() => _inputActions.Keyboard.Movement.ReadValue<Vector2>());
        _inputActions = new PlayerInputActions();                                                                                   // Создание классов вынести в DI?
        _inputActions.Enable();
        _inputActions.Keyboard.Movement.started += OnMoveStarted;
        _inputActions.Keyboard.Movement.canceled += OnMoveCanceled;
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
        Debug.Log(collision.gameObject.layer);                                                     //++++++++++++++++++++++

        HandleCollision(transform.position);
    }

    private void HandleCollision(Vector3 position)
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
        Debug.Log(newPosition);                                                     //++++++++++++++++++++++
        _mapHandler.ChangeTile(newPosition, ConstantsGame.EmptyTile);
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StartMoving();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StopMoving();
    }
}
