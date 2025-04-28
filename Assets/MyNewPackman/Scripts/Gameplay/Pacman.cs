using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pacman : MonoBehaviour     // Разделить логику
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
            case GameConstants.TriggerPelletSmall:
                Debug.Log(GameConstants.TriggerPelletSmall);
                break;

            case GameConstants.TriggerPelletMedium:
                Debug.Log(GameConstants.TriggerPelletMedium);
                break;

            case GameConstants.TriggerPelletLarge:
                Debug.Log(GameConstants.TriggerPelletLarge);
                break;
        }

        HandleCollision(transform.position);
        //collision.gameObject.SetActive(false);
    }

    public void Initialize(IMapHandler mapHandler)
    {
        _mapHandler = mapHandler;
    }

    private void HandleCollision(Vector3 position)
    {
        _mapHandler.ChangeTile(position, GameConstants.EmptyTile);
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
