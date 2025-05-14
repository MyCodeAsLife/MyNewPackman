using UnityEngine;
// Настройки конкретных уровней строений
[CreateAssetMenu(fileName = "BuildingLevelSettings", menuName = "GameSettings/New Building Level Settings")]
public class BuildingLevelSettings : ScriptableObject
{
    public int Level;           // Уровень строения
    public double BaseIncome;   // Базовый доход
}