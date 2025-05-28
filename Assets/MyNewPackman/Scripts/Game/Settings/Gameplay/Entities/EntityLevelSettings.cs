using UnityEngine;

[CreateAssetMenu(fileName = "EntityLevelSettings", menuName = "GameSettings/New Entity Level Settings")]
public class EntityLevelSettings : ScriptableObject
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string PrefabSkinPath { get; private set; }
}