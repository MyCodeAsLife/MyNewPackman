using UnityEngine;

public class EntityData
{
    public int UniqId { get; set; }             // Уникальный идентификатор сущности
    public string ConfigId { get; set; }        // Идентификатор для поиска настроек сущности
    public EntityType Type { get; set; }        // Тип сущности для быстрого понимания, что это за сущность
    public Vector2Int Position { get; set; }    // Позития в координатах x,y, которая конвертируется в x,z, на плоскости
}