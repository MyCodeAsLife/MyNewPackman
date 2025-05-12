public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuilding>
{
    private readonly GameStateProxy _gameState;

    public CmdPlaceBuildingHandler(GameStateProxy gameStateProxy) // Правильнее передавать через интерфейс
    {
        _gameState = gameStateProxy;
    }

    public bool Handle(CmdPlaceBuilding command)
    {
        var entityId = _gameState.GetEntityId();
        var newBuildingEntity = new BuildingEntity
        {
            Id = entityId,
            TypeId = command.BuildingTypeId,
            Position = command.Position,
        };

        var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
        _gameState.Buildings.Add(newBuildingEntityProxy);

        return true;
    }
}
