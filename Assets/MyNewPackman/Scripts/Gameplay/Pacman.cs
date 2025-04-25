using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pacman : MonoBehaviour
{
    private int _pelletLayer;

    private PlayerInputActions _inputActions;                       // ��� ������������� ������� � ����� PlayerInputController
    private IPlayerMovementHandler _playerMoveHandler;              // DI - ? ����� interface
    private IMapHandler _mapHandler;

    private void Awake()
    {
        _pelletLayer = LayerMask.NameToLayer(GameConstants.Pellet);
    }

    private void OnEnable()
    {
        //_mapHandler = FindFirstObjectByType<LevelPresenter>().MapHandler;   // ������� � ����� Initialize

        _playerMoveHandler = new PlayerMovementHandler(transform.GetComponent<Rigidbody2D>());        // �������� ������� ������� � DI?
        _playerMoveHandler.Initialyze(() => _inputActions.Keyboard.Movement.ReadValue<Vector2>());

        _inputActions = new PlayerInputActions();                       // ��� ������������� ������� � ����� PlayerInputController
        _inputActions.Enable();                                         // ��� ������������� ������� � ����� PlayerInputController
        _inputActions.Keyboard.Movement.started += OnMoveStarted;       // ������������ ����� DI?
        _inputActions.Keyboard.Movement.canceled += OnMoveCanceled;     // ������������ ����� DI?
    }

    private void OnDisable()
    {
        _inputActions.Disable();                                    // ��� ������������� ������� � ����� PlayerInputController
        _inputActions.Keyboard.Movement.performed -= OnMoveStarted; // ����������� ����� DI?
    }

    private void Update()
    {
        // ���� ������� ����� Update ����� R3, �� ������ ����� ����������, � ����� ����� ����������� �����
        _playerMoveHandler.Tick();  // ��� �������� "�����"
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������������ ������������ �� ����
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
