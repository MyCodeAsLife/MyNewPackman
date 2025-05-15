using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings/New Game Settings")]
public class GameSettings : ScriptableObject
{
    public BuildingsSettings BuildingsSettings;
    public MapsSettings MapsSettings;
}
