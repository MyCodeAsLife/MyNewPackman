using System;
using UnityEngine;

// Класс "заглушка" - уровня сцены
public class SomeGameplayService : IDisposable
{
    private readonly SomeCommonService _someCommonService;

    public SomeGameplayService(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;

        Debug.Log(GetType().Name + " has been created");            //+++++++++++++++++++++++++++++++
    }

    public void Dispose()
    {
        Debug.Log("Gameplay - Очистить все подписки");                         //+++++++++++++++++++++++++++++++
    }
}

