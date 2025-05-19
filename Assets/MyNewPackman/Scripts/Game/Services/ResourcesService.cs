using ObservableCollections;
using R3;
using System.Collections.Generic;
using System.Linq;

public class ResourcesService
{
    public readonly ObservableList<ResourceViewModel> Resources = new();    // Нужно сделать ReadOnlyObservableList

    private readonly Dictionary<ResourceType, ResourceViewModel> _resourcesMap = new();

    public ResourcesService(ObservableList<Resource> resources)
    {
        // Связываем модели и вьюмодели
        resources.ForEach(recource => { Resources.Add(new ResourceViewModel(recource)); });

        // Связываем на добавление ресурса
        resources.ObserveAdd().Subscribe(collectionAddEvent =>
        {
            var newResource = new ResourceViewModel(collectionAddEvent.Value);
            Resources.Add(newResource);
            _resourcesMap[newResource.ResourceType] = newResource;
        });

        // Связываем на удаление ресурса
        resources.ObserveRemove().Subscribe(collectionRemoveEvent =>
        {
            if (_resourcesMap.TryGetValue(collectionRemoveEvent.Value.ResourceType, out ResourceViewModel resourceViewModel))
            {
                Resources.Remove(resourceViewModel);
                _resourcesMap.Remove(resourceViewModel.ResourceType);
            }
        });
    }
}