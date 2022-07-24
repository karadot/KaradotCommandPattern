using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
    using Commands;

    public class CommandRecorder
    {
        private Stack<MovementCommandBase> _commands = new Stack<MovementCommandBase>();
        private List<MovementCommandBase> _commandQueue = new List<MovementCommandBase>();
        public MovementCommandBase LastMovementCommandBase => _commands is {Count: > 0} ? _commands.Peek() : null;

        public void ExecuteCommand(MovementCommandBase newMovementCommandBase)
        {
            if (newMovementCommandBase.Execute())
            {
                _commands.Push(newMovementCommandBase);
            }
        }

        public void AddCommandToList(MovementCommandBase newMovementCommandBase)
        {
            _commandQueue.Add(newMovementCommandBase);
        }

        MovementCommandBase ExecuteCommandOnList()
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