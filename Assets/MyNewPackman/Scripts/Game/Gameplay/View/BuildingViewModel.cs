using R3;
using System.Collections.Generic;
using UnityEngine;

public class BuildingViewModel
{
    public readonly string ConfigId;
    public readonly int BuildingEntityId;
    private readonly BuildingEntity _buildingEntity;
    private readonly BuildingSettings _buildingSettings;    // Начальные настройки объекта
    private readonly BuildingsService _buildingsService;
    // Зачем это? если эти настройки можно бырать из _buildingSettings
    private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap = new();

    public BuildingViewModel(
        BuildingEntity buildingEntity,
        BuildingSettings buildingSettings,
        BuildingsService buildingsService)
    {
        ConfigId = buildingEntity.ConfigId;
        BuildingEntityId = buildingEntity.UniqueId;
        Level = buildingEntity.Level;
        _buildingEntity = buildingEntity;       // Правильно передовать через интерфейс "только для чтения"
        _buildingSettings = buildingSettings;   // А также при изменении уровня строения, настройки уровня можно брать отсюда
        _buildingsService = buildingsService;   // Необходим для отправки команд на изменениее, при взаимодействии с моделькой

        foreach (var buildingLevelSettings in _buildingSettings.Levels)
        {
            _levelSettingsMap[buildingLevelSettings.Level] = buildingLevelSettings;
        }

        Position = buildingEntity.Position;
    }

    public ReadOnlyReactiveProperty<Vector2Int> Position { get; }
    public ReadOnlyReactiveProperty<int> Level { get; }

    // К примеру при наведении на улучшение здание, UI запросит отсюда данные следующего уровня чтобы показать пользователю
    // как изменится здание при улучшении
    public BuildingLevelSettings GetLevelSettings(int level)
    {
        return _levelSettingsMap[level];
    }
}