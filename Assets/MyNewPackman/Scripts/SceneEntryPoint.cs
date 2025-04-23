using Assets.MyPackman.Model;
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

        // Зарегестрировать игрового персонажа
        // Зарегестрировать контроллер (обработчик нажатия клавиш)
        // Зарегестрировать противников
        // Что еще зарегестрировать?
        _sceneContainer.RegisterSingleton(c => new PacmanFactory());

        CreateScene();
    }

    private void CreateScene()
    {
        var grid = new GameObject("Level");
        grid.AddComponent<Grid>();

        var walls = new GameObject("Walls");
        walls.transform.parent = grid.transform;
        walls.layer = LayerMask.NameToLayer("Obstacle");
        var wallsTilemap = walls.AddComponent<Tilemap>();
        walls.AddComponent<TilemapRenderer>();
        var wallsCollider = walls.AddComponent<TilemapCollider2D>();
        wallsCollider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("NoFriction");

        var pellets = new GameObject("Pellets");
        pellets.transform.parent = grid.transform;
        pellets.layer = LayerMask.NameToLayer("Pellet");
        var pelletsTilemap = pellets.AddComponent<Tilemap>();
        pellets.AddComponent<TilemapCollider2D>();
        pellets.AddComponent<TilemapRenderer>();

        var wallsTiles = LoadResources("WallTiles/", 38);
        var pelletsTiles = LoadResources("PelletTiles/", 3);

        var levelPresenter = gameObject.AddComponent<LevelPresenter>();
        levelPresenter.Initialize(wallsTilemap, pelletsTilemap, wallsTiles, pelletsTiles);
        levelPresenter.Run();
    }

    private Tile[] LoadResources(string path, int count)
    {
        Tile[] walls = new Tile[count];

        for (int i = 0; i < count; i++)
            walls[i] = Resources.Load<Tile>($"{path}{i}");

        return walls;
    }
}
