using System;
using UnityEngine;

[Serializable]
public class EntityInitialStateSettingsData
{
    [field: SerializeField] public EntityType EntityType { get; private set; }
    [field: SerializeField] public string ConfigId { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public Vector2Int InitialPosition { get; private set; }
}