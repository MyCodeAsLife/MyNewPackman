using Assets.MyPackman.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneEntryPoint : MonoBehaviour
{
    private DIContainer _sceneContainer;

    private void Start()
    {
        Initialize();
    }

    // Если что сделать публичным и инициализировать извне
    private void Initialize(DIContainer parentContainer = null)
    {
        _sceneContainer = new DIContainer(parentContainer);
        _sceneContainer.RegisterInstance<ILevelData>(new NormalLevelData());           // MapModel - Получать как часть настроек из MainMenu

        // Зарегестрировать игрового персонажа
        // Зарегестрировать контроллер (обработчик нажатия клавиш)
        // Зарегестрировать противников
        // Что еще зарегестрировать?
        _sceneContainer.RegisterSingleton(_ => new PacmanFactory());
        _sceneContainer.RegisterSingleton<Pacman>(c => c.Resolve<PacmanFactory>().CreatePacman(Vector3.zero));

        InitializeCamera();
        CreateScene();
    }

    private void CreateScene()
    {
        var sceneFrame = new GameObject("Level");                   // Magic.  Имя левела получать как часть настроек из MainMenu
        sceneFrame.AddComponent<Grid>();

        CreateWallFrame(sceneFrame.transform);
        CreatePelletFrame(sceneFrame.transform);

        var levelConstarctor = new LevelConstructor(_sceneContainer);
        levelConstarctor.ConstructLevel();
    }

    private void CreateWallFrame(Transform parent)
    {
        var walls = new GameObject(GameConstants.Obstacle);
        walls.transform.parent = parent;
        walls.layer = LayerMask.NameToLayer(GameConstants.Obstacle);
        var wallsTilemap = walls.AddComponent<Tilemap>();
        _sceneContainer.RegisterInstance(GameConstants.Obstacle, wallsTilemap);
        walls.AddComponent<TilemapRenderer>();
        var wallsCollider = walls.AddComponent<TilemapCollider2D>();
        wallsCollider.sharedMaterial = Resources.Load<PhysicsMaterial2D>(GameConstants.NoFrictionFullPath);
    }

    private void CreatePelletFrame(Transform parent)
    {
        var pellets = new GameObject(GameConstants.Pellet);
        pellets.transform.parent = parent;
        pellets.layer = LayerMask.NameToLayer(GameConstants.Pellet);
        var pelletsTilemap = pellets.AddComponent<Tilemap>();
        _sceneContainer.RegisterInstance(GameConstants.Pellet, pelletsTilemap);
        pellets.AddComponent<TilemapCollider2D>();
        pellets.AddComponent<TilemapRenderer>();
    }

    private void InitializeCamera()
    {
        var map = _sceneContainer.Resolve<ILevelData>().Map;
        float y = map.GetLength(0) * GameConstants.GridCellSize * /*GameConstants.Half*/0.5f;
        float x = map.GetLength(1) * GameConstants.GridCellSize * GameConstants.Half;

        float panelHeight = map.GetLength(0) * 0.1f;   // 10%

        Camera.main.transform.position
            = new Vector3(x, -y + GameConstants.GameplayInformationalPamelHeight, Camera.main.transform.position.z);
        float size = y;

        if (x > y)          // РАБОТАЕТ!!!!!!!!!!!!!!!
            size = x / (16f / 9f) - GameConstants.GameplayInformationalPamelHeight;

        //Camera.main.orthographicSize = size + GameConstants.GridCellSize;

        Camera.main.orthographicSize = size + GameConstants.GameplayInformationalPamelHeight;

        Debug.Log($"x = {map.GetLength(0)}, y = {map.GetLength(1)}, pahelHeight = {panelHeight}"); //+++++++++++++++++++++
    }
}
