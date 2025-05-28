using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CmdCreateMapHandler : ICommandHandler<CmdCreateMap>
{
    private readonly GameStateProxy _gameState;
    private readonly GameSettings _gameSettings;

    public CmdCreateMapHandler(GameStateProxy gameState, GameSettings gameSettings)
    {
        _gameState = gameState;
        _gameSettings = gameSettings;
    }

    public bool Handle(CmdCreateMap command)
    {
        var isMapAlreadyExisted = _gameState.Maps.Any(m => m.Id == command.MapId);  // Созданна ли уже данная карта

        if (isMapAlreadyExisted)
        {
            Debug.Log($"Map width Id = {command.MapId} already exists");
            return false;
        }

        var newMapSettings = _gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);  // Получаем настройки карты
        var newMapInitialStateSettings = newMapSettings.InitialStateSettings;   // Получаем стартовые настройки окружения данной карты

        var initialEntities = new List<EntityData>();

        foreach (var entitySettings in newMapInitialStateSettings.Entities)
        {
            var initialEntityData = EntitiesDataFactory.CreateEntity(entitySettings);
            initialEntityData.UniqId = _gameState.CreateEntityId();
            initialEntities.Add(initialEntityData);
        }

        var newMapState = new MapData
        {
            Id = command.MapId,
            Entities = initialEntities,
        };

        var newMap = new Map(newMapState);
        _gameState.Maps.Add(newMap);

        return true;
    }
}