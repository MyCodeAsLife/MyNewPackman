using R3;
using UnityEngine;

public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingsService _buildingsService;

    public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingsService buildingsService)
    {
        _buildingEntity = buildingEntity;       // Правильно передовать через интерфейс "только для чтения"
        _buildingsService = buildingsService;   // Необходим для отправки команд на изменениее, при взаимодействии с моделькой
    }

    public ReadOnlyReactiveProperty<Vector3Int> Position => _buildingEntity.Position;
    public int BuildingEntityId => _buildingEntity.Id;
}