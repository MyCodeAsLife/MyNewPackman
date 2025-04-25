using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pacman : MonoBehaviour
{
    private int _pelletLayer;

    private PlayerInputActions _inputActions;                       // Все сопутствующее вынести в класс PlayerInputController
    private IPlayerMovementHandler _playerMoveHandler;              // DI - ? через interface
    private IMapHandler _mapHandler;

    private void Awake()
    {
        _pelletLayer = LayerMask.NameToLayer(GameConstants.Pellet);
    }

    private void OnEnable()
    {
        //_mapHandler = FindFirstObjectByType<LevelPresenter>().MapHandler;   // Вынести в метод Initialize

        _playerMoveHandler = new PlayerMovementHandler(transform.GetComponent<Rigidbody2D>());        // Создание классов вынести в DI?
        _playerMoveHandler.Initialyze(() => _inputActions.Keyboard.Movement.ReadValue<Vector2>());

        _inputActions = new PlayerInputActions();                       // Все сопутствующее вынести в класс PlayerInputController
        _inputActions.Enable();                                         // Все сопутствующее вынести в класс PlayerInputController
        _inputActions.Keyboard.Movement.started += OnMoveStarted;       // Подписыватся через DI?
        _inputActions.Keyboard.Movement.canceled += OnMoveCanceled;     // Подписыватся через DI?
    }

    private void OnDisable()
    {
        _inputActions.Disable();                                    // Все сопутствующее вынести в класс PlayerInputController
        _inputActions.Keyboard.Movement.performed -= OnMoveStarted; // Отписыватся через DI?
    }

    private void Update()
    {
        // Если сделать общий Update через R3, то данный вызов упраздница, и проще будет реализовать паузу
        _playerMoveHandler.Tick();  // Для создания "Паузы"
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Обрабатывать столкновения по слою
        //Debug.Log(collision.gameObject.layer);    //++++++++++++++++++++++

        switch (collision.gameObject.name)
        {
            case GameConstants.PelletSmall:
                Debug.Log(GameConstants.PelletSmall);
                break;

            case GameConstants.PelletMedium:
                Debug.Log(GameConstants.PelletMedium);
                break;

            case GameConstants.PelletLarge:
                Debug.Log(GameConstants.PelletLarge);
                break;
        }

        HandleCollision(transform.position);
    }

    public void Initialize(IMapHandler mapHandler)
    {
        _mapHandler = mapHandler;
    }

    private void HandleCollision(Vector3 position)
    {
        //Vector3Int newPosition = HandleCoordinates(position);
        _mapHandler.ChangeTile(position, GameConstants.EmptyTile);
    }

    //private Vector3Int HandleCoordinates(Vector3 position)
    //{
    //    var pos = position;

    //    var newX = Mathf.Abs((int)pos.x - pos.x);
    //    var newY = Mathf.Abs((int)pos.y - pos.y);

    //    int X = (int)pos.x;
    //    int Y = Mathf.Abs((int)pos.y - 1);

    //    if (newX < 0.4f)
    //        X -= 1;
    //    else if (newX > 0.6f)
    //        X += 1;

    //    if (newY < 0.4f)
    //        Y -= 1;
    //    else if (newY > 0.6f)
    //        Y += 1;

    //    return new Vector3Int(X, Y);
    //}

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StartMoving();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _playerMoveHandler.StopMoving();
    }
}
