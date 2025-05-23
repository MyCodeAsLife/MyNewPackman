﻿using ObservableCollections;
using R3;
using System;
using System.Linq;
using System.Collections.Generic;

// Обрабатывает запросы на создание и закрытие окон
public class UIRootViewModel : IDisposable
{
    public ReadOnlyReactiveProperty<WindowViewModel> OpenedScreen => _openedScreen; // Отдаем данные только для чтения
    public IObservableCollection<WindowViewModel> OpenedPopups => _openedPopups;    // Отдаем данные только для чтения

    private readonly ReactiveProperty<WindowViewModel> _openedScreen = new();
    private readonly ObservableList<WindowViewModel> _openedPopups = new();
    private readonly Dictionary<WindowViewModel, IDisposable> _popupSubscriptions = new();

    public void Dispose()
    {
        CloseAllPopups();
        _openedScreen.Value?.Dispose();
    }

    public void OpenScreen(WindowViewModel screenViewModel)
    {
        _openedScreen.Value?.Dispose(); // Закрываем открытое окно
        _openedScreen.Value = screenViewModel;
    }

    public void OpenPopup(WindowViewModel popupViewModel)
    {
        if (_openedPopups.Contains(popupViewModel))
            return;

        var subscription = popupViewModel.CloseRequested.Subscribe(ClosePopup);    // Запрос popupa на закрытие, подписываем на метод который его закроет
        _popupSubscriptions[popupViewModel] = subscription;  // Сохраняем подписку
        _openedPopups.Add(popupViewModel);
    }

    public void ClosePopup(WindowViewModel popupViewModel)
    {
        if (_openedPopups.Contains(popupViewModel))
        {
            popupViewModel.Dispose();
            _openedPopups.Remove(popupViewModel);
            _popupSubscriptions[popupViewModel]?.Dispose(); // Отписываемся от "запроса на закрытие" через сохраненую подписку
            _popupSubscriptions.Remove(popupViewModel);
        }
    }

    public void ClosePopup(string popupId)
    {
        var openedPopup = _openedPopups.FirstOrDefault(p => p.Id == popupId);

        if (openedPopup != null)
            ClosePopup(openedPopup);
    }

    public void CloseAllPopups()
    {
        foreach (var popup in _openedPopups)
            ClosePopup(popup);
    }
}