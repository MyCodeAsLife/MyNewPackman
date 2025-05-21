using System.Collections.Generic;
using UnityEngine;

// Создает, хранит и закрывает View
public class WindowsContainer : MonoBehaviour
{
    [SerializeField] private Transform _screensContainer;
    [SerializeField] private Transform _popupsContainer;

    private readonly Dictionary<WindowViewModel, IWindowBinder> _openedPopupBinders = new();
    private IWindowBinder _openedScreenBinder;

    public void OpenPopup(WindowViewModel viewModel)
    {
        IWindowBinder binder = CreateView(viewModel, _popupsContainer);
        _openedPopupBinders.Add(viewModel, binder);
    }

    public void ClosePopup(WindowViewModel popupViewModel)
    {
        var binder = _openedPopupBinders[popupViewModel];
        binder?.Close();
        _openedPopupBinders.Remove(popupViewModel);
    }

    public void OpenScreen(WindowViewModel viewModel)
    {
        if (viewModel == null)
            return;

        IWindowBinder binder = CreateView(viewModel, _screensContainer);
        _openedScreenBinder = binder;
    }

    private string GetPrefabPath(WindowViewModel viewModel)
    {
        return $"Prefabs/UI/{viewModel.Id}";
    }

    private IWindowBinder CreateView(WindowViewModel viewModel, Transform container)
    {
        var prefabPath = GetPrefabPath(viewModel);
        var prefab = Resources.Load<GameObject>(prefabPath);
        var createdPopup = Instantiate(prefab, container);
        var binder = createdPopup.GetComponent<IWindowBinder>();
        binder.Bind(viewModel);

        return binder;
    }
}