using R3;
using System.Collections.Generic;
using UnityEngine;

// Разделить провайдер сохранения\загрузки геймплея\игры и настроект приложения
public class PlayerPrefsGameStateProvider : IGameStateProvider
{
    private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);           // Вынести в контсанты
    private const string SETTINGS_STATE_KEY = nameof(SETTINGS_STATE_KEY);   // Вынести в контсанты

    public GameStateProxy GameState { get; private set; }
    public GameSettingsStateProxy SettingsState { get; private set; }

    private GameStateData _gameStateOrigin { get; set; }
    private GameSettingsStateData _settingsStateOrigin { get; set; }


    public Observable<GameStateProxy> LoadGameState()   // Похожа на LoadSettingsState
    {
        if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
        {
            GameState = CreateGameStateFromSettings();  // Создаем дефолтное состояние
            Debug.Log("GameState created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));    //++++++++++++++++++++++++++++++++

            SaveGameState();    // Сохраняем дефолтное состояние
        }
        else
        {
            // Загружаем
            var json = PlayerPrefs.GetString(GAME_STATE_KEY);
            _gameStateOrigin = JsonUtility.FromJson<GameStateData>(json);
            GameState = new GameStateProxy(_gameStateOrigin);

            Debug.Log("GameState loaded: " + json);                                  //++++++++++++++++++++++++++++++++
        }

        return Observable.Return(GameState);
    }

    public Observable<GameSettingsStateProxy> LoadSettingsState()   // Похожа на LoadGameState
    {
        if (!PlayerPrefs.HasKey(SETTINGS_STATE_KEY))
        {
            SettingsState = CreateGameSettingsStateFromSettings();  // Создаем дефолтное состояние
            //Debug.Log("GameSettingsState created from settings: " + JsonUtility.ToJson(_settingsStateOrigin, true));    //++++++++++++++++++++++++++++++++

            SaveSettingsState();    // Сохраняем дефолтное состояние
        }
        else
        {
            // Загружаем
            var json = PlayerPrefs.GetString(SETTINGS_STATE_KEY);
            _settingsStateOrigin = JsonUtility.FromJson<GameSettingsStateData>(json);
            SettingsState = new GameSettingsStateProxy(_settingsStateOrigin);

            //Debug.Log("GameSettingsState loaded: " + json);                                  //++++++++++++++++++++++++++++++++
        }

        return Observable.Return(SettingsState);
    }

    public Observable<bool> SaveGameState() // Похожа на SaveSettingsState
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GAME_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> SaveSettingsState() // Похожа на SaveGameState
    {
        var json = JsonUtility.ToJson(_settingsStateOrigin, true);
        PlayerPrefs.SetString(SETTINGS_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> ResetGameState()    // Похожа на ResetSettingsState
    {
        GameState = CreateGameStateFromSettings();
        SaveGameState();

        return Observable.Return(true);
    }

    public Observable<bool> ResetSettingsState()    // Похожа на ResetGameState
    {
        SettingsState = CreateGameSettingsStateFromSettings();
        SaveSettingsState();

        return Observable.Return(true);
    }

    private GameStateProxy CreateGameStateFromSettings()    // Похожа на CreateGameSettingsStateFromSettings
    {
        // Делаем фейк
        _gameStateOrigin = new GameStateData
        {
            Maps = new List<MapData>(),
            Resources = new List<ResourceData>()
            {
                new() {ResourceType = ResourceType.SoftCurrency, Amount = 0},
                new() {ResourceType = ResourceType.HardCurrency, Amount = 0},
            }
        };

        return new GameStateProxy(_gameStateOrigin);
    }

    private GameSettingsStateProxy CreateGameSettingsStateFromSettings()    // Похожа на CreateGameStateFromSettings
    {
        // Делаем фейк
        _settingsStateOrigin = new GameSettingsStateData
        {
            MusicVolume = 22,
            SFXVolume = 33,
        };

        return new GameSettingsStateProxy(_settingsStateOrigin);
    }
}
