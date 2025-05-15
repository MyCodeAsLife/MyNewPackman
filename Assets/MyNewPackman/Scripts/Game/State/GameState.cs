using System;
using System.Collections.Generic;

[Serializable]  // Для возможности сохранения в JSON-формате
public class GameState
{
    public int GlobalEntityId;              // Счетчик для ID создаваемых сущностей.
    public int CurrentMapId;
    public List<MapState> Maps;

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
