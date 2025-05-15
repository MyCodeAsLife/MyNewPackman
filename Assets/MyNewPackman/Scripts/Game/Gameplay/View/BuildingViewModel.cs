using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    private readonly BuildingEntityProxy _buildingEntity;
    private readonly BuildingSettings _buildingSettings;    // Начальные настройки объекта
    private readonly BuildingsService _buildingsService;

    // Зачем это? если эти настройки можно бырать из _buildingSettings
    private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

    public BuildingViewModel(
        BuildingEntityProxy buildingEntity,
        BuildingSettings buildingSettings,
        BuildingsService buildingsService)
    {
        _buildingEntity = buildingEntity;       // Правильно передовать через интерфейс "только для чтения"
        _buildingSettings = buildingSettings;   // А также при изменении уровня строения, настройки уровня можно брать отсюда
        _buildingsService = buildingsService;   // Необходим для отправки команд на изменениее, при взаимодействии с моделькой

        foreach (var buildingLevelSettings in _buildingSettings.LevelsSettings)
        {
            _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        }
    }

    public ReadOnlyReactiveProperty<Vector3Int> Position => _buildingEntity.Position;
    public ReadOnlyReactiveProperty<int> Level => _buildingEntity.Level;
    public int BuildingEntityId => _buildingEntity.Id;
    public string TypeId => _buildingEntity.TypeId;

    // К примеру при наведении на улучшение здание, UI запросит отсюда данные следующего уровня чтобы показать пользователю
    // как изменится здание при улучшении
    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return _levelSettingsMap[level];
    }
}