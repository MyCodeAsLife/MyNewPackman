using ObservableCollections;
using R3;
using System.Linq;

// Паттерн Proxy над GameState
public class GameStateProxy // Переименовать в GameState
{
    private readonly GameStateData _gameStateData;

    public ReactiveProperty<int> CurrentMapId = new();

    public GameStateProxy(GameStateData gameStateData)
    {
        _gameStateData = gameStateData;

        InitMaps();
        InitResources();
    }

    public ObservableList<Map> Maps { get; } = new();
    public ObservableList<Resource> Resources { get; } = new();

    public int CreateEntityId() => _gameStateData.CreateEntityId();

    private void InitMaps()
    {
        _gameStateData.Maps.ForEach(mapStateData => Maps.Add(new Map(mapStateData)));

        // При добавлении элемента в Maps текущего класса, добавится элемент в Maps класса GameStateData
        Maps.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedMap = collectionAddEvent.Value;
            _gameStateData.Maps.Add(addedMap.Origin);
        });

        // При удалении элемента из Maps текущего класса, также удалится элемент из Maps класса GameStateData
        Maps.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedMap = collectionRemovedEvent.Value;
            var removedMapStateData = _gameStateData.Maps.FirstOrDefault(mapStateData =>
                                                            mapStateData.Id == removedMap.Id);
            _gameStateData.Maps.Remove(removedMapStateData);
        });

        CurrentMapId.Subscribe(newValue => { _gameStateData.CurrentMapId = newValue; });
    }

    private void InitResources()
    {
        _gameStateData.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));

        Resources.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedResource = collectionAddEvent.Value;
            _gameStateData.Resources.Add(addedResource.Origin);
        });

        Resources.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedResource = collectionRemovedEvent.Value;
            var removedResourceData = _gameStateData.Resources.FirstOrDefault(resourceData =>
                                                resourceData.ResourceType == removedResource.ResourceType);
            _gameStateData.Resources.Remove(removedResourceData);
        });
    }
}
