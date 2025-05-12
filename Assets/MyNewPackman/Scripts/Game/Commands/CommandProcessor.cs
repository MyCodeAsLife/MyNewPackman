using System;
using System.Collections.Generic;

public class CommandProcessor : ICommandProcessor   // Обработчик команд
{
    private readonly Dictionary<Type, object> _handlesMap = new();  // Для регистрации входящих команд
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
                // А лучше всего сохранение реализовать следующим образом:
                // У ICommandProcessor, создать ивент, который будет срабатывать при успешных выполнениях команд
                // И на этот ивент подписать сохранение
                _gameStateProvider.SaveGameState();
            }

            return result;
        }

        return false;
    }
}
