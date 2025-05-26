using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    public readonly string TypeId;
    public readonly int BuildingEntityId;
    //private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingSettings _buildingSettings;    // ��������� ��������� �������
    //private readonly BuildingsService _buildingsService;
    // ����� ���? ���� ��� ��������� ����� ������ �� _buildingSettings
    private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

    //public BuildingViewModel(
    //    BuildingEntityProxy buildingEntity,
    //    BuildingSettings buildingSettings,
    //    BuildingsService buildingsService)
    //{
    //    TypeId = buildingEntity.TypeId;
    //    BuildingEntityId = buildingEntity.Id;
    //    Level = buildingEntity.Level;
    //    _buildingEntity = buildingEntity;       // ��������� ���������� ����� ��������� "������ ��� ������"
    //    _buildingSettings = buildingSettings;   // � ����� ��� ��������� ������ ��������, ��������� ������ ����� ����� ������
    //    _buildingsService = buildingsService;   // ��������� ��� �������� ������ �� ����������, ��� �������������� � ���������

        //    foreach (var buildingLevelSettings in _buildingSettings.LevelsSettings)
        //    {
        //        _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        //    }
        //}

    public ReadOnlyReactiveProperty<Vector3Int> Position { get; }
    public ReadOnlyReactiveProperty<int> Level { get; }

    // � ������� ��� ��������� �� ��������� ������, UI �������� ������ ������ ���������� ������ ����� �������� ������������
    // ��� ��������� ������ ��� ���������
    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return _levelSettingsMap[level];
    }
}