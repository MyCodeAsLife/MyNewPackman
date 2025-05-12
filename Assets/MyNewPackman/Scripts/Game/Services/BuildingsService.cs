using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

// Объединяет ViewModel's и состояния, и так-же через ICommandProcessor
public class BuildingsService   // Посылает команды в обработчик команд
{
    private readonly ICommandProcessor _cmd;
    private readonly ObservableList<BuildingViewModel> _allBuildings = new(); // Для выдачи наружу реактивного списка
    private readonly Dictionary<int, BuildingViewModel> _buildingMap = new(); // Кэшируем созданные ViewModel

    public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;  // Для выдачи наружу реактивного списка

    public BuildingsService(IObservableCollection<BuildingEntityProxy> buildings, ICommandProcessor cmd)
    {
        _cmd = cmd;

        // Пробегаемся по состояниям и для каждого состояние создаем ViewModel
        foreach (var building in buildings)
        {
            CreateBuildingViewModel(building);
        }

        // Подписываем создание новых ViewModel для новодобовляющихся состояний
        buildings.ObserveAdd().Subscribe(e =>
        {
            CreateBuildingViewModel(e.Value);
        });

        // Подписываем удаление текущих ViewModel при удалении их состояний
        buildings.ObserveRemove().Subscribe(e =>
        {
            RemoveBuildingViewModel(e.Value);
        });
    }

    public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
    {
        var command = new CmdPlaceBuilding(buildingTypeId, position);
        var result = _cmd.Process(command);

        return result;
    }

    public bool MoveBuilding(int buildingEntityId, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    public bool DeleteBuilding(int buildingEntityId)
    {
        throw new NotImplementedException();
    }

    private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
    {
        var buildingViewModel = new BuildingViewModel(buildingEntity, this);
        _allBuildings.Add(buildingViewModel);
        _buildingMap[buildingEntity.Id] = buildingViewModel;
    }

    private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
    {
        if (_buildingMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
        {
            _buildingMap.Remove(buildingEntity.Id);
            _allBuildings.Remove(buildingViewModel);
        }
    }
}
