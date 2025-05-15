using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapsSettings", menuName = "GameSettings/New Maps Settings")]
public class MapsSettings : ScriptableObject
{
    public List<MapSettings> Maps;
}