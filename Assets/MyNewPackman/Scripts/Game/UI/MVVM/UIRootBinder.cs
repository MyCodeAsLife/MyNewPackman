using ObservableCollections;
using R3;
using UnityEngine;

// Отвечает за все подписки Вьюх на Вьюмодели
public class UIRootBinder : MonoBehaviour
{
    [SerializeField] private WindowsContainer _windowsContainer;

    private readonly CompositeDisposable _subscriptions = new();    // Для отписок

    public void Bind(UIRootViewModel viewModel)
    {
        // Подписываемся на изменение viewModel.OpenedScreen, тоесть когда пришел на запрос открытия нового окна
        _subscriptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
        {
            _windowsContainer.OpenScreen(newScreenViewModel);
        }));

        // Создаем View для уже существующих/открытых Popups
        foreach (var popup in viewModel.OpenedPopups)
        {
            _subscriptions.Add(popup);
        }

        // Пописываемся на открытие новых Popups
        _subscriptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            _windowsContainer.OpenPopup(collectionAddEvent.Value);
        }));

        // Подписываемся на закрытие уже открытых Popups
        _subscriptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(collectionRemoveEvent =>
        {
            _windowsContainer.ClosePopup(collectionRemoveEvent.Value);
        }));

        OnBind(viewModel);
    }

    protected virtual void OnBind(UIRootViewModel viewModel) { }

    private void OnDestroy()
    {
        foreach (var popup in _subscriptions)
            popup.Dispose();
    }
}