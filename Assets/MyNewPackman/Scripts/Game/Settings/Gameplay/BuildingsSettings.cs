using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsSettings", menuName = "GameSettings/New Buildings Settings")]
public class BuildingsSettings : ScriptableObject
{
    public List<BuildingSettings> AllBuildings; // Список настроек всех типов строений
}
