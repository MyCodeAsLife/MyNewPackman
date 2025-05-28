using System;
using UnityEngine;

public static class EntitiesDataFactory // Статика это плохо! Переделать!!!
{
    public static EntityData CreateEntity(EntityInitialStateSettingsData initialSettings)
    {
        switch (initialSettings.EntityType)
        {
            //case EntityType.Unknown:
            //    break;
            case EntityType.Building:
                var buildingEntityData = CreateEntity<BuildingEntityData>(initialSettings);
                UpdateBuildingEntity(buildingEntityData);
                return buildingEntityData;
            //case EntityType.Resource:
            //    break;
            default:
                throw new Exception($"Not implement entity creation: {initialSettings.EntityType}");
        }
    }

    private static T CreateEntity<T>(EntityInitialStateSettingsData initialSettings) where T : EntityData, new()
    {
        return CreateEntity<T>(
            initialSettings.EntityType,
            initialSettings.ConfigId,
            initialSettings.Level,
            initialSettings.InitialPosition);
    }

    private static T CreateEntity<T>(EntityType type, string configId, int level, Vector2Int position)
        where T : EntityData, new()
    {
        var entity = new T()
        {
            Type = type,
            ConfigId = configId,
            Level = level,
            Position = position,
        };

        return entity;
    }

    private static void UpdateBuildingEntity(BuildingEntityData buildingEntityData)
    {
        buildingEntityData.LastClickedTimeMS = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        buildingEntityData.IsAutoCollectionEnabled = false;
    }
}