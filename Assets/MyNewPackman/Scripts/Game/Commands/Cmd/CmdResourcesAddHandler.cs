using System.Linq;

public class CmdResourcesAddHandler : ICommandHandler<CmdResourcesAdd>
{
    private readonly GameStateProxy _gameStateProxy;

    public CmdResourcesAddHandler(GameStateProxy gameStateProxy)
    {
        _gameStateProxy = gameStateProxy;
    }

    public bool Handle(CmdResourcesAdd command) // Ќехватает обработки на отрицательное число
    {
        var resource = _gameStateProxy.Resources.FirstOrDefault(
                    resource => resource.ResourceType == command.ResourceType);
        if (resource == null)
            resource = CreateNewResource(command.ResourceType);

        resource.Amount.Value += command.Amount;

        return true;
    }

    private Resource CreateNewResource(ResourceType resourceType)
    {
        var newResourceData = new ResourceData()
        {
            ResourceType = resourceType,
            Amount = 0,
        };

        var newResource = new Resource(newResourceData);
        _gameStateProxy.Resources.Add(newResource);

        return newResource;
    }
}