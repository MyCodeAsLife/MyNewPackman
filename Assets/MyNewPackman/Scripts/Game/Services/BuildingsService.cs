//using ObservableCollections;
//using R3;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//// ���������� ViewModel's � ���������, � ���-�� ����� ICommandProcessor
//public class BuildingsService   // �������� ������� � ���������� ������
//{
//    private readonly ICommandProcessor _cmd;
//    private readonly ObservableList<BuildingViewModel> _allBuildings = new();
//    private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();  // �������� ��������� ViewModel
//    private readonly Dictionary<string, BuildingSettings> _buildingsSettingsMap = new();   // �������� ������ �������� ��� ���� ����� ��������

//    public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;  // ��� ������ ������ ����������� ������

//    public BuildingsService(
//        IObservableCollection<BuildingEntityProxy> buildings,
//        BuildingsSettings buildingsSettings,
//        ICommandProcessor cmd)
//    {
//        _cmd = cmd;

//        // ��������� ������ �������� ��� ���� ����� ��������
//        foreach (var buildingSettings in buildingsSettings.AllBuildings)
//        {
//            _buildingsSettingsMap[buildingSettings.TypeId] = buildingSettings;
//        }

//        // ����������� �� ���������� � ��� ������� ��������� ������� ViewModel
//        foreach (var building in buildings)
//        {
//            CreateBuildingViewModel(building);
//        }

//        // ����������� �������� ����� ViewModel ��� ����������������� ���������
//        buildings.ObserveAdd().Subscribe(e =>
//        {
//            CreateBuildingViewModel(e.Value);
//        });

//        // ����������� �������� ������� ViewModel ��� �������� �� ���������
//        buildings.ObserveRemove().Subscribe(e =>
//        {
//            RemoveBuildingViewModel(e.Value);
//        });
//    }

//    public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
//    {
//        var command = new CmdPlaceBuilding(buildingTypeId, position);
//        var result = _cmd.Process(command);

//        return result;
//    }

//    public bool MoveBuilding(int buildingEntityId, Vector3Int position)
//    {
//        throw new NotImplementedException();
//    }

//    public bool DeleteBuilding(int buildingEntityId)
//    {
//        throw new NotImplementedException();
//    }

//    private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
//    {
//        var buildingSettings = _buildingsSettingsMap[buildingEntity.TypeId];
//        var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);
//        _allBuildings.Add(buildingViewModel);
//        _buildingsMap[buildingEntity.Id] = buildingViewModel;
//    }

//    private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
//    {
//        if (_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
//        {
//            _buildingsMap.Remove(buildingEntity.Id);
//            _allBuildings.Remove(buildingViewModel);
//        }
//    }
//}