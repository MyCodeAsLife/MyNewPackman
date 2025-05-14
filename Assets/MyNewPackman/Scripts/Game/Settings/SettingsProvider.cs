using System.Threading.Tasks;
using UnityEngine;

public class SettingsProvider : ISettingsProvider
{
    private GameSettings _gameSettings;

    public SettingsProvider()   // Загрузка настроек приложения при создании класса
    {
        ApplicationSettings = Resources.Load<ApplicationSettings>("Settings/ApplicationSettings");  // Magic
    }

    public ApplicationSettings ApplicationSettings { get; }
    public GameSettings GameSettings => _gameSettings;

    public Task<GameSettings> LoadGameSettingsAsync() // Асинхронная загрузка настроек игрового уровня
    {
        if (_gameSettings == null)
            _gameSettings = Resources.Load<GameSettings>("Settings/GameSettings");

        return Task.FromResult(_gameSettings);
    }
}
