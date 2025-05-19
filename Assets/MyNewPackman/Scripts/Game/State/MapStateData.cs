using System;
using System.Collections.Generic;

[Serializable]
public class MapStateData
{
    public int GlobalEntityId;              // Счетчик для ID создаваемых сущностей.
    public int Id;
    public List<BuildingEntityData> Buildings;
}
