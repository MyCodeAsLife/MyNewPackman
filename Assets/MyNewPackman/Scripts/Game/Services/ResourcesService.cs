using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;

public class ResourcesService
{
    public readonly ObservableList<ResourceViewModel> Resources = new();    // Нужно сделать ReadOnlyObservableList

    private readonly Dictionary<ResourceType, ResourceViewModel> _resourcesMap = new();
    private readonly ICommandProcessor _cmd;

    public ResourcesService(ObservableList<Resource> resources, ICommandProcessor cmd)
    {
        _cmd = cmd;
        // Связываем модели и вьюмодели
        resources.ForEach(CreateResource);
        // Связываем на добавление ресурса
        resources.ObserveAdd().Subscribe(collectionAddEvent => CreateResource(collectionAddEvent.Value));
        // Связываем на удаление ресурса
        resources.ObserveRemove().Subscribe(collectionRemoveEvent => RemoveResource(collectionRemoveEvent.Value));
    }

    public bool TryAddResources(ResourceType resourceType, int amount)
    {
        var command = new CmdResourcesAdd(resourceType, amount);
        return _cmd.Process(command);
    }

    public bool TrySpendResources(ResourceType resourceType, int amount)
    {
        var command = new CmdResourcesSpend(resourceType, amount);
        return _cmd.Process(command);
    }

    public bool IsEnoughResources(ResourceType resourceType, int amount)
    {
        if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            return resourceViewModel.Amount.CurrentValue >= amount;

        return false;
    }

    public Observable<int> ObserveResource(ResourceType resourceType) // Для облегчения подписания на изменения ресурса
    {
        if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            return resourceViewModel.Amount;

        throw new Exception($"Resource of type {resourceType} doesn't exist");
    }

    private void CreateResource(Resource resource)
    {
        var newResourceViewModel = new ResourceViewModel(resource);
        _resourcesMap[newResourceViewModel.ResourceType] = newResourceViewModel;
        Resources.Add(newResourceViewModel);
    }

    private void RemoveResource(Resource resource)
    {
        if (_resourcesMap.TryGetValue(resource.ResourceType, out ResourceViewModel resourceViewModel))
        {
            _resourcesMap.Remove(resourceViewModel.ResourceType);
            Resources.Remove(resourceViewModel);
        }
    }
}