using Assets.MyPackman.Presenter;
using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pacman : MonoBehaviour     // ��������� ������
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
