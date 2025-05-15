using System;
using System.Collections.Generic;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class GameState
{
    public int GlobalEntityId;              // ������� ��� ID ����������� ���������.
    public int CurrentMapId;
    public List<MapState> Maps;

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
