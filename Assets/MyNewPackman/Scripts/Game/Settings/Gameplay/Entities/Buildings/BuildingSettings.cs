using UnityEngine;
// Настройки конкретного типа строения
[CreateAssetMenu(fileName = "BuildingSettings", menuName = "GameSettings/New Building Settings")]
public class BuildingSettings : EntitySettings<BuildingLevelSettings>
{
}
