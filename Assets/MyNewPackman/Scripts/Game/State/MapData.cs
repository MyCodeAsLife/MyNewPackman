using System.Collections.Generic;

public class MapData
{
    //public int GlobalEntityId;              // Счетчик для ID создаваемых сущностей. Ненужен????????????
    public int Id { get; set; }
    public List<EntityData> Entities { get; set; }
}
