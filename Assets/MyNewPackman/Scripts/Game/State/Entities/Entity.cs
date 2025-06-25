using R3;
using UnityEngine;

public abstract class Entity
{
    public readonly ReactiveProperty<Vector2Int> Position;

    public Entity(EntityData data)
    {
        Origin = data;

        Position = new ReactiveProperty<Vector2Int>(data.Position);             // Реактивное свойство
        Position.Subscribe(newPosition => { data.Position = newPosition; });    // При изменении реактивного свойства, также изменится data
    }

    public EntityData Origin { get; }
    public int UniqueId => Origin.UniqId;
    public string ConfigId => Origin.ConfigId;
    public EntityType Type => Origin.Type;
}