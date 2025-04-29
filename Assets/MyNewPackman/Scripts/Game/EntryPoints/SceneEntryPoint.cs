using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneEntryPoint : MonoBehaviour
{
    private DIContainer _sceneContainer;

    // Если что сделать публичным и инициализировать извне
    public void Run(DIContainer projectContainer = null)
    {
        _sceneContainer = new DIContainer(projectContainer);

        // + Зарегестрировать игрового персонажа
        // Зарегестрировать контроллер (обработчик нажатия клавиш)
        // Зарегестрировать противников
        // + Зарегестрировать уровень
        // Что еще зарегестрировать?
        _sceneContainer.RegisterSingleton(_ => new CharacterFactory());
        _sceneContainer.RegisterSingleton<Pacman>(c => c.Resolve<CharacterFactory>().CreatePacman(Vector3.zero));
        _sceneContainer.RegisterTransient<Ghost>(c => c.Resolve<CharacterFactory>().CreateGhost(Vector3.zero));     // Можно разбить на синглтоны ghost 1, 2, 3 и т.д.

        _sceneContainer.RegisterInstance<ILevelConfig>(new NormalLevelConfig());           // Переделать под LevelData

        new LevelData(new NormalLevelConfig());

        InitializeCamera();
        CreateScene();
    }

    private void CreateScene()
    {
        var sceneFrame = new GameObject("Level");                   // Magic.  Имя левела получать как часть настроек из MainMenu
        sceneFrame.AddComponent<Grid>();

        CreateWallFrame(sceneFrame.transform);
        CreatePelletFrame(sceneFrame.transform);
        CreateNodeFrame(sceneFrame.transform);

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

    private void CreateNodeFrame(Transform parent)
    {
        var nodes = new GameObject(GameConstants.Node);
        nodes.transform.parent = parent;
        nodes.layer = LayerMask.NameToLayer(GameConstants.Node);
        var nodesTilemap = nodes.AddComponent<Tilemap>();
        _sceneContainer.RegisterInstance(GameConstants.Node, nodesTilemap);
        nodes.AddComponent<TilemapCollider2D>();
    }

    private void InitializeCamera()
    {
        const float OffsetFromScreenAspectRatio = 16f / 9f;

        var map = _sceneContainer.Resolve<ILevelConfig>().Map;
        float y = map.GetLength(0) * GameConstants.GridCellSize * GameConstants.Half;
        float x = map.GetLength(1) * GameConstants.GridCellSize * GameConstants.Half;

        Camera.main.transform.position
            = new Vector3(x, -y + GameConstants.GameplayInformationalPamelHeight, Camera.main.transform.position.z);
        float size = y;

        if (x > y)
            size = x / OffsetFromScreenAspectRatio - GameConstants.GameplayInformationalPamelHeight;

        Camera.main.orthographicSize
            = size + (GameConstants.GameplayInformationalPamelHeight * OffsetFromScreenAspectRatio);
    }
}
