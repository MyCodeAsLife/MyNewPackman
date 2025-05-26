using System.Collections.Generic;

public class GameStateData
{
    public int GlobalEntityId { get; set; }                  // ������� ��� ID ����������� ���������.
    public int CurrentMapId { get; set; }
    public List<MapData> Maps { get; set; }
    public List<ResourceData> Resources { get; set; }        // ������ ������ �� ����� Map ?

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
