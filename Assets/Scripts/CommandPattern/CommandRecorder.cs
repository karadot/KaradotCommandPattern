using System.Collections.Generic;


namespace CommandPattern
{
    using Commands;
    public class CommandRecorder
    {
        private Stack<IMovementCommand> _commands = new Stack<IMovementCommand>();
        private List<IMovementCommand> _commandQueue = new List<IMovementCommand>();
        public IMovementCommand LastMovementCommand => _commands is {Count: > 0} ? _commands.Peek() : null;

        public void ExecuteCommand(IMovementCommand newMovementCommand)
        {
            if (newMovementCommand.Execute())
            {
                _commands.Push(newMovementCommand);
            }
        }

        public void AddCommandToList(IMovementCommand newMovementCommand)
        {
            _commandQueue.Add(newMovementCommand);
        }

        public IMovementCommand ExecuteCommandOnList()
        {
            if (_commandQueue.Count == 0)
                return null;
            var commandToExecute = _commandQueue[0];
            _commandQueue.RemoveAt(0);
            _commands.Push(commandToExecute);
            commandToExecute.Execute();
            return commandToExecute;
        }

        public void Undo()
        {
            if (_commands.Count == 0) return;
            var lastCommand = _commands.Peek();
            if (lastCommand.Undo())
                _commands.Pop();
        }
    }
}