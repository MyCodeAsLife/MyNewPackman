using R3;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsGameStateProvider : IGameStateProvader
{
    private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);

    public GameStateProxy GameState { get; private set; }

    private GameState _gameStateOrigin { get; set; }

    public Observable<GameStateProxy> LoadGameState()
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
            _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
            GameState = new GameStateProxy(_gameStateOrigin);

            Debug.Log("GameState loaded: " + json);                                  //++++++++++++++++++++++++++++++++
        }

        return Observable.Return(GameState);
    }

    public Observable<bool> SaveGameState()
    {
        var json = JsonUtility.ToJson(_gameStateOrigin, true);
        PlayerPrefs.SetString(GAME_STATE_KEY, json);

        return Observable.Return(true);
    }

    public Observable<bool> ResetGameState()
    {
        GameState = CreateGameStateFromSettings();
        SaveGameState();

        return Observable.Return(true);
    }

    private GameStateProxy CreateGameStateFromSettings()
    {
        // Делаем фейк
        _gameStateOrigin = new GameState
        {
            Buildings = new List<BuildingEntity>
            {
                new(){TypeId = "Default one"},
                new(){TypeId = "Default two"},
            }
        };

        return new GameStateProxy(_gameStateOrigin);
    }
}
