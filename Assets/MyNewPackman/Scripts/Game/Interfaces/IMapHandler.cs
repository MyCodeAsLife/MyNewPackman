using UnityEngine;

public interface IMapHandler
{
    //public int GetTile(Vector3Int position);
    public void ChangeTile(Vector3 position, int objectNumber);
    //public bool TryFindPositionByObjectNumber(int number, ref Vector3Int position);
    public bool IsIntersaction(int x, int y);
}
