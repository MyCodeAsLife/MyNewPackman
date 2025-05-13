using R3;
using UnityEngine;

public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingsService _buildingsService;

    public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingsService buildingsService)
    {
        _buildingEntity = buildingEntity;       // ��������� ���������� ����� ��������� "������ ��� ������"
        _buildingsService = buildingsService;   // ��������� ��� �������� ������ �� ����������, ��� �������������� � ���������
    }

    public ReadOnlyReactiveProperty<Vector3Int> Position => _buildingEntity.Position;
    public int BuildingEntityId => _buildingEntity.Id;
}