using System;
using System.Collections.Generic;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class GameState
{
    public int GlobalEntityId;              // ������� ��� ID ����������� ���������.
    public List<BuildingEntity> Buildings   /*= new()*/;
}
