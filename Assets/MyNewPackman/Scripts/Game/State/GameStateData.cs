using System;
using System.Collections.Generic;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class GameStateData
{
    public int GlobalEntityId;              // ������� ��� ID ����������� ���������.
    public int CurrentMapId;
    public List<MapStateData> Maps;
    public List<ResourceData> Resources;        // ������ ������ �� ����� Map ?

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
