using System;
using System.Collections.Generic;

public class CommandProcessor : ICommandProcessor   // ���������� ������
{
    private readonly Dictionary<Type, object> _handlesMap = new();  // ��� ����������� �������� ������
    private readonly IGameStateProvider _gameStateProvider;

    public CommandProcessor(IGameStateProvider gameStateProvider)
    {
        _gameStateProvider = gameStateProvider;
    }

    public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _handlesMap[typeof(TCommand)] = handler;
    }

    public bool Process<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (_handlesMap.TryGetValue(typeof(TCommand), out var handler))
        {
            var typedHandler = (ICommandHandler<TCommand>)handler;
            var result = typedHandler.Handle(command);

            if (result)
            {
                // � ����� ����� ���������� ����������� ��������� �������:
                // � ICommandProcessor, ������� �����, ������� ����� ����������� ��� �������� ����������� ������
                // � �� ���� ����� ��������� ����������
                _gameStateProvider.SaveGameState();
            }

            return result;
        }

        return false;
    }
}
