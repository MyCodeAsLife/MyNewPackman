using R3;
using System.Collections.Generic;
using UnityEngine;

// Класс в котором создаются View и объеденяются с ViewModel
// Хранит в себе список префабов
public class WorldGameplayRootBinder : MonoBehaviour
{
    private readonly Dictionary<int, BuildingBinder> _viewBuildingsMap = new();

    // Это на случай когда во время выгрузки сцены, данный объект удалится раньше ViewModel-ей
    // И они при своем удалении будут пытатся обрашатся к данному объекту на удаление View
    private readonly CompositeDisposable _disposables = new();

    private WorldGameplayRootViewModel _viewModel;

    // For Tests
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _viewModel.HandleTestInput();
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    public void Bind(WorldGameplayRootViewModel rootViewModel)
    {
        _viewModel = rootViewModel; // For Tests

        //foreach (var viewModel in rootViewModel.AllBuildings)
        //{
        //    CreateBuilding(viewModel);
        //}

        //// Подписываем создание View, на появление новых ViewModel
        //_disposables.Add(rootViewModel.AllBuildings.ObserveAdd().Subscribe(e =>
        //{
        //    CreateBuilding(e.Value);
        //}));
        //// Подписываем удаление View, на удаление ViewModel
        //_disposables.Add(rootViewModel.AllBuildings.ObserveRemove().Subscribe(e =>
        //{
        //    DestroyBuilding(e.Value);
        //}));
    }

    private void CreateBuilding(BuildingViewModel buildingViewModel)
    {
        int buildingLevel = buildingViewModel.Level.CurrentValue;
        string buildingTypeId = buildingViewModel.TypeId;
        string prefabBuildingPath = $"Prefabs/ForTests/Building_{buildingTypeId}_{buildingLevel}";
        var prefabBuilding = Resources.Load<BuildingBinder>(prefabBuildingPath);
        var createdBuilding = Instantiate(prefabBuilding);     // Создаем View объекта

        createdBuilding.Bind(buildingViewModel);                // Объеденяем его с ViewModel

        // По хорошему, создаваемые View нужно кэшировать, чтобы проще было их удалять
        // Или переложить ответственность на их удаление на сам объект (тоесть подписать функцию удаление на евент удаления)
        _viewBuildingsMap[buildingViewModel.BuildingEntityId] = createdBuilding;
    }

    private void DestroyBuilding(BuildingViewModel buildingViewModel)
    {
        if (_viewBuildingsMap.TryGetValue(buildingViewModel.BuildingEntityId, out var createdBuilding))
        {
            Destroy(createdBuilding.gameObject);
            _viewBuildingsMap.Remove(buildingViewModel.BuildingEntityId);
        }
    }
}
