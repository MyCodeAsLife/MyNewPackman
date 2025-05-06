using System;
using UnityEngine;

[Serializable]  // Для возможности сохранения в JSON-формате
public class BuildingEntity
{
    public int Id;
    public int Level;
    public string TypeId;
    public Vector3Int position;
}
