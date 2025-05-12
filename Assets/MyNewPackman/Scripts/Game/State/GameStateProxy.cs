using ObservableCollections;
using R3;
using System.Linq;

// ������� Proxy ��� GameState
public class GameStateProxy
{
    private readonly GameState _gameState;

    public GameStateProxy(GameState gameState)
    {
        _gameState = gameState;
        _gameState.Buildings.ForEach(buildingEntity => Buildings.Add(new BuildingEntityProxy(buildingEntity)));

        // ��� ���������� �������� � Buildings �������� ������, ��������� ������� � Buildings ������ GameState
        Buildings.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedBuildingEntityProxy = collectionAddEvent.Value;
            _gameState.Buildings.Add(addedBuildingEntityProxy.BuildingEntity);
        });

        // ��� �������� �������� �� Buildings �������� ������, ����� �������� ������� �� Buildings ������ GameState
        Buildings.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedBuildingEntityProxy = collectionRemovedEvent.Value;
            var removedBuildingEntity = _gameState.Buildings.FirstOrDefault(buildingEntity =>
                                                buildingEntity.Id == removedBuildingEntityProxy.Id);
            _gameState.Buildings.Remove(removedBuildingEntity);
        });
    }

    public ObservableList<BuildingEntityProxy> Buildings { get; } = new();

    public int GetEntityId() => _gameState.GlobalEntityId++;
}
