using System;
using UnityEngine;

// ����� "��������" - ������ �����
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
        //Debug.Log("MainMenu - �������� ��� ��������");                         //+++++++++++++++++++++++++++++++
    }
}
