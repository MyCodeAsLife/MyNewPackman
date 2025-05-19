using System;
using UnityEngine;

[Serializable]  // ��� ����������� ���������� � JSON-�������
public class BuildingEntityData : EntityData     // ����� ������� ������ ������ �������� ���������� �� �������
{
    public int Level;
    public string TypeId;
    public Vector3Int Position;
}
