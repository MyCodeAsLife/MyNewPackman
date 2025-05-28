using System.Collections.Generic;
using UnityEngine;

public abstract class EntitySettings<T> : ScriptableObject where T : EntityLevelSettings
{
    [field: SerializeField] public EntityType EntityType { get; private set; }  // Тип строения
    [field: SerializeField] public string ConfigId { get; private set; }        
    [field: SerializeField] public string TitleLid { get; private set; }        // Наименование строения
    [field: SerializeField] public string DescriptionLid { get; private set; }  // Описание строения
    [field: SerializeField] public string PrefabPath { get; private set; }
    [field: SerializeField] public string PrefabName { get; private set; }
    [field: SerializeField] public List<T> Levels { get; private set; }         // Уровни строения
}

// Сущность по умолчанию, наследованная от EntityLevelSettings
[CreateAssetMenu(fileName = "EntitySettings", menuName = "GameSettings/New Entity Settings")]
public class EntitySettings : EntitySettings<EntityLevelSettings>
{
}