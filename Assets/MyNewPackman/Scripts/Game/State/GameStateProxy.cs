using ObservableCollections;
using R3;
using System.Linq;

// ������� Proxy ��� GameState
public class GameStateProxy
{
    private readonly GameState _gameState;

    public ReactiveProperty<int> CurrentMapId = new();

    public GameStateProxy(GameState gameState)
    {
        _gameState = gameState;
        _gameState.Maps.ForEach(mapState => Maps.Add(new Map(mapState)));

        // ��� ���������� �������� � Buildings �������� ������, ��������� ������� � Buildings ������ GameState
        Maps.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var addedMap = collectionAddEvent.Value;
            _gameState.Maps.Add(addedMap.Origin);
        });

        // ��� �������� �������� �� Buildings �������� ������, ����� �������� ������� �� Buildings ������ GameState
        Maps.ObserveRemove().Subscribe(collectionRemovedEvent =>
        {
            var removedMap = collectionRemovedEvent.Value;
            var removedMapState = _gameState.Maps.FirstOrDefault(mapState =>
                                                mapState.Id == removedMap.Id);
            _gameState.Maps.Remove(removedMapState);
        });

        CurrentMapId.Subscribe(newValue => { gameState.CurrentMapId = newValue; });
    }

    public ObservableList<Map> Maps { get; } = new();

    public int CreateEntityId() => _gameState.CreateEntityId();
}
