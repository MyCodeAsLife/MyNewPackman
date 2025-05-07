using System;
using UnityEngine;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class BuildingEntity     // ����� ������� ������ ������ �������� ���������� �� �������
{
    public int Id;
    public int Level;
    public string TypeId;
    public Vector3Int Position;
}
