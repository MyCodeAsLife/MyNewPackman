using System;
using UnityEngine;

[Serializable]  // Для возможности сохранения в JSON-формате
public class BuildingEntity : Entity     // Некий игровой объект данные которого изменяются во времени
{
    public int Level;
    public string TypeId;
    public Vector3Int Position;
}
