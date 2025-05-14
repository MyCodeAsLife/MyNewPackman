using System.Collections.Generic;
using UnityEngine;
// Настройки конкретного типа строения
[CreateAssetMenu(fileName = "BuildingSettings", menuName = "GameSettings/New Building Settings")]
public class BuildingSettings : ScriptableObject
{
    public string TypeId;                   // Тип строения
    public string TitleLID;                 // Наименование строения
    public string DescriptionLID;           // Описание строения
    public List<BuildingLevelSettings> LevelsSettings;  // Уровния строения
}
