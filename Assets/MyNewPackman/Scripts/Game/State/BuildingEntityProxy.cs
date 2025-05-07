using R3;
using UnityEngine;

// ������� Proxy ��� BuildingEntity
public class BuildingEntityProxy
{
    public BuildingEntityProxy(BuildingEntity buildingEntity)
    {
        Id = buildingEntity.Id;
        TypeId = buildingEntity.TypeId;
        Level = new ReactiveProperty<int>(buildingEntity.Level);
        Position = new ReactiveProperty<Vector3Int>(buildingEntity.Position);

        Level.Skip(1).Subscribe(value => buildingEntity.Level = value);         // �������� �� ��������� ���������, ��� ��������� Proxy
        Position.Skip(1).Subscribe(value => buildingEntity.Position = value);   // �������� �� ��������� ���������, ��� ��������� Proxy
    }

    public int Id { get; }              // ������ ��������
    public string TypeId { get; }       // ������ ��������
    public ReactiveProperty<int> Level { get; }             // ����� �������� ����� Level.Value
    public ReactiveProperty<Vector3Int> Position { get; }   // ����� �������� ����� Position.Value
}
