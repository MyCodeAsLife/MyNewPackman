using System;
using UnityEngine;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class BuildingEntity : Entity     // ����� ������� ������ ������ �������� ���������� �� �������
{
    public int Level;
    public string TypeId;
    public Vector3Int Position;
}
