//using System.Linq;
//using UnityEngine;

//public class CmdPlaceBuildingHandler : ICommandHandler<CmdPlaceBuilding>
//{
//    private readonly GameStateProxy _gameState;

//    public CmdPlaceBuildingHandler(GameStateProxy gameStateProxy) // Правильнее передавать через интерфейс
//    {
//        _gameState = gameStateProxy;
//    }

//    public bool Handle(CmdPlaceBuilding command)
//    {
//        var currentMap = _gameState.Maps.FirstOrDefault(m => m.Id == _gameState.CurrentMapId.Value);
//        if (currentMap == null)
//        {
//            Debug.Log($"Couldn't find MapState for id: {_gameState.CurrentMapId.Value}");
//            return false;
//        }

//        var entityId = _gameState.CreateEntityId();
//        var newBuildingEntity = new BuildingEntityData
//        {
//            Id = entityId,
//            TypeId = command.BuildingTypeId,
//            Position = command.Position,
//        };

//        var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
//        currentMap.Buildings.Add(newBuildingEntityProxy);

//        return true;
//    }
//}
