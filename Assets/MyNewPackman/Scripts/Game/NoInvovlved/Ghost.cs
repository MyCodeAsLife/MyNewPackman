using R3;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public ReadOnlyReactiveProperty<int> Health => _health;
    public Observable<int> Healt1 => _health;
    public Observable<int> HealthChanged => _healthChanged;

    private ReactiveProperty<int> _health;

    private readonly Subject<int> _healthChanged = new();
    public void Call()
    {
        _health = new ReactiveProperty<int>(200);
        _health.Value = 0;
        _health.Subscribe(newValue => { Debug.Log($"Health: {newValue}"); });
        _health.OnNext(55);

        _healthChanged.OnNext(60);
    }
}
