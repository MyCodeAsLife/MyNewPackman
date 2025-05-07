using ObservableCollections;
using R3;
using System.Linq;

// ������� Proxy ��� GameState
public class GameStateProxy
{
    public GameStateProxy(GameState gameState)
    {
        gameState.Buildings.ForEach(buildingEntity => Buildings.Add(new BuildingEntityProxy(buildingEntity)));

        // ��� ���������� �������� � Buildings �������� ������, ��������� ������� � Buildings ������ GameState
        Buildings.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedBuildingEntityProxy = collectionAddEvent.Value;
            gameState.Buildings.Add(new BuildingEntity
            {
                Id = addedBuildingEntityProxy.Id,
                TypeId = addedBuildingEntityProxy.TypeId,
                Level = addedBuildingEntityProxy.Level.Value,
                Position = addedBuildingEntityProxy.Position.Value,
            });
        });

        // ��� �������� �������� �� Buildings �������� ������, ����� �������� ������� �� Buildings ������ GameState
        Buildings.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedBuildingEntityProxy = collectionRemovedEvent.Value;
            var removedBuildingEntity = gameState.Buildings.FirstOrDefault(buildingEntity => 
                                                buildingEntity.Id == removedBuildingEntityProxy.Id);
            gameState.Buildings.Remove(removedBuildingEntity);
        });
    }

    public ObservableList<BuildingEntityProxy> Buildings { get; } = new();
}
