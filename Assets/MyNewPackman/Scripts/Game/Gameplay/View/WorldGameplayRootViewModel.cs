using ObservableCollections;
using R3;
using System;
using UnityEngine;

// Содержит список всех ViewModel, является посредником между WorldGameplayRootBinder и реактивным списком всех ViewModel
public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<BuildingViewModel> AllBuildings;
    private readonly ResourcesService _resourcesService;

    public WorldGameplayRootViewModel(BuildingsService buildingsService, ResourcesService resourcesService)
    {
        AllBuildings = buildingsService.AllBuildings;   // Кэшируем доступ к реактивному списку ViewModel's

        // For Tests
        _resourcesService = resourcesService;
        resourcesService.ObserveResource(ResourceType.SoftCurrency)
            .Subscribe(newValue => Debug.Log($"SoftCurrency {newValue}."));
        resourcesService.ObserveResource(ResourceType.HardCurrency)
            .Subscribe(newValue => Debug.Log($"HardCurrency {newValue}."));
    }

    public void HandleTestInput()
    {
        var rResourceType = (ResourceType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ResourceType)).Length);
        var rValue = UnityEngine.Random.Range(1, 1000);
        var rOperation = UnityEngine.Random.Range(0, 2);

        if (rOperation == 0)
            _resourcesService.TryAddResources(rResourceType, rValue);
        else
            _resourcesService.TrySpendResources(rResourceType, rValue);
    }
}
