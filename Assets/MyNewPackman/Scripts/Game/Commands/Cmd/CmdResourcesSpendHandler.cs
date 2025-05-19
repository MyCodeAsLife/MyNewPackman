using System.Linq;
using UnityEngine;

public class CmdResourcesSpendHandler : ICommandHandler<CmdResourcesSpend>
{
    private readonly GameStateProxy _gameStateProxy;

    public CmdResourcesSpendHandler(GameStateProxy gameStateProxy)
    {
        _gameStateProxy = gameStateProxy;
    }

    public bool Handle(CmdResourcesSpend command)
    {
        var resource = _gameStateProxy.Resources.FirstOrDefault(resource => resource.ResourceType == command.ResourceType);

        if (resource == null)
        {
            Debug.LogError("Trying to spend not existed resource");
            return false;
        }

        if (resource.Amount.Value < command.Amount)
        {
            Debug.LogError($"Trying to spend more resources than existen ({resource.ResourceType}). " +
                $"Exists: {resource.Amount.Value}. Neded: {command.Amount}.");
            return false;
        }

        resource.Amount.Value -= command.Amount;
        return true;
    }
}