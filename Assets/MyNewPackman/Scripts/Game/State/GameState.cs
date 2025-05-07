using System;
using System.Collections.Generic;

[Serializable]  // Для возможности сохранения в JSON-формате
public class GameState
{
    public List<BuildingEntity> Buildings = new();
}
