using System;
using System.Collections.Generic;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class GameState
{
    public List<BuildingEntity> Buildings = new();
}
