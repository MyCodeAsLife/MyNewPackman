using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    public readonly string ConfigId;
    public readonly int BuildingEntityId;
    private readonly BuildingEntity _buildingEntity;
    private readonly BuildingSettings _buildingSettings;    // ��������� ��������� �������
    private readonly BuildingsService _buildingsService;
    // ����� ���? ���� ��� ��������� ����� ������ �� _buildingSettings
    private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

    public BuildingViewModel(
        BuildingEntity buildingEntity,
        BuildingSettings buildingSettings,
        BuildingsService buildingsService)
    {
        ConfigId = buildingEntity.ConfigId;
        BuildingEntityId = buildingEntity.UniqueId;
        Level = buildingEntity.Level;
        _buildingEntity = buildingEntity;       // ��������� ���������� ����� ��������� "������ ��� ������"
        _buildingSettings = buildingSettings;   // � ����� ��� ��������� ������ ��������, ��������� ������ ����� ����� ������
        _buildingsService = buildingsService;   // ��������� ��� �������� ������ �� ����������, ��� �������������� � ���������

        foreach (var buildingLevelSettings in _buildingSettings.Levels)
        {
            _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        }

        Position = buildingEntity.Position;
    }

    public ReadOnlyReactiveProperty<Vector2Int> Position { get; }
    public ReadOnlyReactiveProperty<int> Level { get; }

    // � ������� ��� ��������� �� ��������� ������, UI �������� ������ ������ ���������� ������ ����� �������� ������������
    // ��� ��������� ������ ��� ���������
    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return _levelSettingsMap[level];
    }
}