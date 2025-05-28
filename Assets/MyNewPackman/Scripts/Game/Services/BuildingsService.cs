using ObservableCollections;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

// ���������� ViewModel's � ���������, � ���-�� ����� ICommandProcessor
public class BuildingsService   // �������� ������� � ���������� ������
{
    private readonly ICommandProcessor _cmd;
    private readonly ObservableList<BuildingViewModel> _allBuildings = new();
    private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();  // �������� ��������� ViewModel
    private readonly Dictionary<string, BuildingSettings> _buildingsSettingsMap = new();   // �������� ������ �������� ��� ���� ����� ��������

    public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;  // ��� ������ ������ ����������� ������

    public BuildingsService(
        IObservableCollection<Entity> entities,
        EntitiesSettings buildingsSettings,
        ICommandProcessor cmd)
    {
        _cmd = cmd;

        // ��������� ������ �������� ��� ���� ����� ��������
        foreach (var buildingSettings in buildingsSettings.Buildings)
        {
            _buildingsSettingsMap[buildingSettings.ConfigId] = buildingSettings;
        }

        // ����������� �� ���������� � ��� ������� ��������� ������� ViewModel
        foreach (var entity in entities)
        {
            if (entity is BuildingEntity buildingEntity)
                CreateBuildingViewModel(buildingEntity);
        }

        // ����������� �������� ����� ViewModel ��� ����������������� ���������
        entities.ObserveAdd().Subscribe(e =>
        {
            if (e.Value is BuildingEntity buildingEntity)
                CreateBuildingViewModel(buildingEntity);
        });

        // ����������� �������� ������� ViewModel ��� �������� �� ���������
        entities.ObserveRemove().Subscribe(e =>
        {
            if (e.Value is BuildingEntity buildingEntity)
                RemoveBuildingViewModel(buildingEntity);
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

    private void CreateBuildingViewModel(BuildingEntity buildingEntity)
    {
        var buildingSettings = _buildingsSettingsMap[buildingEntity.ConfigId];
        var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);
        _allBuildings.Add(buildingViewModel);
        _buildingsMap[buildingEntity.UniqueId] = buildingViewModel;
    }

    private void RemoveBuildingViewModel(BuildingEntity buildingEntity)
    {
        if (_buildingsMap.TryGetValue(buildingEntity.UniqueId, out var buildingViewModel))
        {
            _buildingsMap.Remove(buildingEntity.UniqueId);
            _allBuildings.Remove(buildingViewModel);
        }
    }
}