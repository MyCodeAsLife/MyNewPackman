using System;
using System.Collections.Generic;

[Serializable]  // Для возможности сохранения в JSON-формате
public class GameStateData
{
    public int GlobalEntityId;              // Счетчик для ID создаваемых сущностей.
    public int CurrentMapId;
    public List<MapStateData> Maps;
    public List<ResourceData> Resources;        // Почему валюта не часть Map ?

    public int CreateEntityId()
    {
        return GlobalEntityId++;
    }
}
