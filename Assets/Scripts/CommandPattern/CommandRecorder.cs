using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
    using Commands;

    public class CommandRecorder
    {
        private Stack<ICommand> _commands = new Stack<ICommand>();
        private List<ICommand> _commandQueue = new List<ICommand>();
        public ICommand LastMovementCommandBase => _commands is {Count: > 0} ? _commands.Peek() : null;

        public void ExecuteCommand(ICommand newMovementCommandBase)
        {
            if (newMovementCommandBase.Execute())
            {
                _commands.Push(newMovementCommandBase);
            }
        }

        public void AddCommandToList(ICommand newMovementCommandBase)
        {
            _commandQueue.Add(newMovementCommandBase);
        }

        ICommand ExecuteCommandOnList()
        {
            if (_commandQueue.Count == 0)
                return null;
            var commandToExecute = _commandQueue[0];
            _commandQueue.RemoveAt(0);
            _commands.Push(commandToExecute);
            commandToExecute.Execute();
            return commandToExecute;
        }

        public IEnumerator ExecuteAllCommandsOnList()
        {
            var last = ExecuteCommandOnList();
            while (last != null)
            {
                var last1 = last;
                yield return new WaitUntil(() => last1.CanExecute);
                yield return new WaitForSeconds(.05f);
                last = ExecuteCommandOnList();
            }
        }

        void Undo()
        {
            if (_commands.Count == 0) return;
            var lastCommand = _commands.Peek();
            if (lastCommand.Undo())
                _commands.Pop();
        }

        public IEnumerator FullUndo()
        {
            var last = LastMovementCommandBase;
            while (last != null)
            {
                Undo();
                var last1 = last;
                yield return new WaitUntil(() => last1.CanExecute);
                yield return new WaitForSeconds(.05f);
                last = LastMovementCommandBase;
            }
        }
    }
}