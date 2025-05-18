using System.Threading.Tasks;

public interface ISettingsProvider
{
    GameSettings GameSettings { get; }
    ApplicationSettings ApplicationSettings { get; }

    public Task<GameSettings> LoadBasicGameSettingsAsync(); // Асинхронная загрузка настроек
}