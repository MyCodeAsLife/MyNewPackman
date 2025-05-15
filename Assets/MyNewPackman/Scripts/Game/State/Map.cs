using ObservableCollections;
using R3;
using System.Linq;

public class Map
{
    public Map(MapState mapState)
    {
        Origin = mapState;
        mapState.Buildings.ForEach(buildingEntity => Buildings.Add(new BuildingEntityProxy(buildingEntity)));

        // При добавлении элемента в Buildings текущего класса, добавится элемент в Buildings класса GameState
        Buildings.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedBuildingEntityProxy = collectionAddEvent.Value;
            mapState.Buildings.Add(addedBuildingEntityProxy.BuildingEntity);
        });

        // При удалении элемента из Buildings текущего класса, также удалится элемент из Buildings класса GameState
        Buildings.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedBuildingEntityProxy = collectionRemovedEvent.Value;
            var removedBuildingEntity = mapState.Buildings.FirstOrDefault(buildingEntity =>
                                                buildingEntity.Id == removedBuildingEntityProxy.Id);
            mapState.Buildings.Remove(removedBuildingEntity);
        });
    }

    public int Id => Origin.Id;
    public MapState Origin { get; }
    public ObservableList<BuildingEntityProxy> Buildings { get; } = new();
}