using R3;
using UnityEngine;

// Паттерн Proxy над BuildingEntity
public class BuildingEntityProxy
{
    public BuildingEntityProxy(BuildingEntity buildingEntity)
    {
        Id = buildingEntity.Id;
        TypeId = buildingEntity.TypeId;
        Level = new ReactiveProperty<int>(buildingEntity.Level);
        Position = new ReactiveProperty<Vector3Int>(buildingEntity.Position);

        Level.Skip(1).Subscribe(value => buildingEntity.Level = value);         // Подписка на изменение оригинала, при изменении Proxy
        Position.Skip(1).Subscribe(value => buildingEntity.Position = value);   // Подписка на изменение оригинала, при изменении Proxy
    }

    public int Id { get; }              // Нельзя изменить
    public string TypeId { get; }       // Нельзя изменить
    public ReactiveProperty<int> Level { get; }             // Можно изменить через Level.Value
    public ReactiveProperty<Vector3Int> Position { get; }   // Можно изменить через Position.Value
}
