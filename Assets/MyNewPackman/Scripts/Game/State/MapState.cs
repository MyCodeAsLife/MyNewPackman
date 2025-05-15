using System;
using System.Collections.Generic;

[Serializable]
public class MapState
{
    public int GlobalEntityId;              // Счетчик для ID создаваемых сущностей.
    public int Id;
    public List<BuildingEntity> Buildings   /*= new()*/;
}
