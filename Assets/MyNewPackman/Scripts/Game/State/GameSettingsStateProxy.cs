using R3;

public class GameSettingsStateProxy
{
    public GameSettingsStateProxy(GameSettingsState gameSettingsState)
    {
        MusicVolume = new ReactiveProperty<int>(gameSettingsState.MusicVolume);
        SFXVolume = new ReactiveProperty<int>(gameSettingsState.SFXVolume);

        MusicVolume.Skip(1).Subscribe(value => gameSettingsState.MusicVolume = value);  // Подписка на изменение оригинала, при изменении Proxy
        SFXVolume.Skip(1).Subscribe(value => gameSettingsState.SFXVolume = value);      // Подписка на изменение оригинала, при изменении Proxy
    }

    public ReactiveProperty<int> MusicVolume { get; }
    public ReactiveProperty<int> SFXVolume { get; }
}
