using R3;

// Интерфейс для провайдеров загрузки\сохранения (в файл, в облако, в базу данных и т.д.)
public interface IGameStateProvider
{
    public GameStateProxy GameState { get; }
    public GameSettingsStateProxy SettingsState { get; }

    // Возвращают Observable чтобы загрузку\сохранение можно было подождать.
    public Observable<GameStateProxy> LoadGameState();
    public Observable<GameSettingsStateProxy> LoadSettingsState();
    public Observable<bool> SaveGameState();
    public Observable<bool> SaveSettingsState();
    public Observable<bool> ResetGameState();
    public Observable<bool> ResetSettingsState();
}
