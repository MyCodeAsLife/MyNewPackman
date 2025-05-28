using System.Collections.Generic;
using UnityEngine;

// В теории предназначен для списков настроек для всех сущностей
// Расширяется добавлением списков для новых сущностей
[CreateAssetMenu(fileName = "EntitiesSettings", menuName = "GameSettings/New Entities Settings")]
public class EntitiesSettings : ScriptableObject
{
    [field: SerializeField] public List<BuildingSettings> Buildings { get; private set; } // Список настроек всех типов строений
}