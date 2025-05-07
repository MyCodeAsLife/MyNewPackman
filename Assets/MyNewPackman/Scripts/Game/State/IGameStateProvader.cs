using R3;

// Интерфейс для провайдеров загрузки\сохранения (в файл, в облако, в базу данных и т.д.)
public interface IGameStateProvader
{
    public GameStateProxy GameState { get; }

    // Возвращают Observable чтобы загрузку\сохранение можно было подождать.
    public Observable<GameStateProxy> LoadGameState();
    public Observable<bool> SaveGameState();
    public Observable<bool> ResetGameState();
}
