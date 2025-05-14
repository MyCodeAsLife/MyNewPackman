using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationSettings", menuName = "GameSettings/New Application Settings")]
public class ApplicationSettings : ScriptableObject
{
    public int MusicVolume;
    public int SFXVolume;
    public string Difficulty;
}
