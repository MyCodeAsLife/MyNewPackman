public static class EntitiesFactory       // Статика плохо!!! Переделать
{
    public static Entity CreateEntity(EntityData entityData)    // Ресурсоемкие процессы, но делаются редко
    {
        switch (entityData.Type)
        {
            case EntityType.Building:
                return new BuildingEntity(entityData as BuildingEntityData);

            case EntityType.Resource:
                return new ResourceEntity(entityData as ResourceEntityData);

            default:
                throw new System.Exception($"Unsuported entity type: {entityData.Type}");
        }
    }
}
