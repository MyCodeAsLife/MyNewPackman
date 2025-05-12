using System;
using UnityEngine;

// Класс "заглушка" - уровня сцены
public class SomeMainMenuService : IDisposable
{
    private readonly SomeCommonService _someCommonService;

    public SomeMainMenuService(SomeCommonService someCommonService)
    {
        _someCommonService = someCommonService;

        //Debug.Log(GetType().Name + " has been created");            //+++++++++++++++++++++++++++++++
    }

    public void Dispose()
    {
        //Debug.Log("MainMenu - Очистить все подписки");                         //+++++++++++++++++++++++++++++++
    }
}
