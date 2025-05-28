using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings/New Game Settings")]
public class GameSettings : ScriptableObject
{
    public EntitiesSettings EntitiesSettings;
    public MapsSettings MapsSettings;
}
