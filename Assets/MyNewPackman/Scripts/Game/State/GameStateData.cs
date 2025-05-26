using System.Collections.Generic;

public class GameStateData
{
    public int GlobalEntityId { get; set; }                  // Счетчик для ID создаваемых сущностей.
    public int CurrentMapId { get; set; }
    public List<MapData> Maps { get; set; }
    public List<ResourceData> Resources { get; set; }        // Почему валюта не часть Map ?

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
