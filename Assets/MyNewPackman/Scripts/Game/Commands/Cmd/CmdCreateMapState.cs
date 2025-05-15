public class CmdCreateMapState : ICommand
{
    public readonly int MapId;

    public CmdCreateMapState(int mapId)
    {
        MapId = mapId;
    }
}