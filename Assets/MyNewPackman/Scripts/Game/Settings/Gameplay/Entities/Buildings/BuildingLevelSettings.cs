using UnityEngine;
// Настройки конкретных уровней строений
[CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "GameSettings/New Building Level Settings")]
public class BuildingLevelSettings : EntityLevelSettings
{
    [field: SerializeField] public double BaseIncome { get; private set; }   // Базовый доход
}