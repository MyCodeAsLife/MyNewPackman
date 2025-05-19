using UnityEngine;

[CreateAssetMenu(fileName = "MapSettings", menuName = "GameSettings/New Map Settings")]
public class MapSettings : ScriptableObject
{
    public int MapId;
    public MapInitialStateSettingsData InitialStateSettings;
}