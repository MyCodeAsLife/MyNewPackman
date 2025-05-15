using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingSettings _buildingSettings;    // ��������� ��������� �������
    private readonly BuildingsService _buildingsService;

    // ����� ���? ���� ��� ��������� ����� ������ �� _buildingSettings
    private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

    public BuildingViewModel(
        BuildingEntityProxy buildingEntity,
        BuildingSettings buildingSettings,
        BuildingsService buildingsService)
    {
        _buildingEntity = buildingEntity;       // ��������� ���������� ����� ��������� "������ ��� ������"
        _buildingSettings = buildingSettings;   // � ����� ��� ��������� ������ ��������, ��������� ������ ����� ����� ������
        _buildingsService = buildingsService;   // ��������� ��� �������� ������ �� ����������, ��� �������������� � ���������

        foreach (var buildingLevelSettings in _buildingSettings.LevelsSettings)
        {
            _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        }
    }

    public ReadOnlyReactiveProperty<Vector3Int> Position => _buildingEntity.Position;
    public ReadOnlyReactiveProperty<int> Level => _buildingEntity.Level;
    public int BuildingEntityId => _buildingEntity.Id;
    public string TypeId => _buildingEntity.TypeId;

    // � ������� ��� ��������� �� ��������� ������, UI �������� ������ ������ ���������� ������ ����� �������� ������������
    // ��� ��������� ������ ��� ���������
    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return _levelSettingsMap[level];
    }
}