using ObservableCollections;
using R3;
using System;
using System.Linq;
using UnityEngine;

// Класс "заглушка" - уровня сцены
public class SomeGameplayService : IDisposable
{
    private readonly GameStateProxy _gameState;
    private readonly SomeCommonService _someCommonService;

    public SomeGameplayService(GameStateProxy gameState, SomeCommonService someCommonService)
    {
        _gameState = gameState;
        _someCommonService = someCommonService;

        Debug.Log(GetType().Name + " has been created");            //+++++++++++++++++++++++++++++++

        gameState.Buildings.ForEach(element => Debug.Log($"Building: {element.TypeId}"));
        // Подписки на добавление\удаление новых объектов Building
        gameState.Buildings.ObserveAdd().Subscribe(addEvent => Debug.Log($"Building add type: {addEvent.Value.TypeId}"));
        gameState.Buildings.ObserveRemove().Subscribe(addEvent => Debug.Log($"Building remove type: {addEvent.Value.TypeId}"));

        // Добавление и удаление объектов
        AddBuilding("Test First");
        AddBuilding("Test Second");
        AddBuilding("Test Third");

        RemoveBuilding("Test Second");
    }

    public void Dispose()
    {
        Debug.Log("Gameplay - Очистить все подписки");                         //+++++++++++++++++++++++++++++++
    }

    private void AddBuilding(string buildingTypeId)
    {
        var building = new BuildingEntity
        {
            TypeId = buildingTypeId,
        };
        var buildingProxy = new BuildingEntityProxy(building);
        _gameState.Buildings.Add(buildingProxy);
    }

    private void RemoveBuilding(string buildingTypeId)
    {
        var buildingEntityProxy = _gameState.Buildings.FirstOrDefault(b => b.TypeId == buildingTypeId);

        if (buildingTypeId != null)
        {
            _gameState.Buildings.Remove(buildingEntityProxy);
        }
    }
}
