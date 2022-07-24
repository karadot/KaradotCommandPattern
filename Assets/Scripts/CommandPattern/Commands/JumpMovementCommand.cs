using UnityEngine;

namespace CommandPattern.Commands
{
    public class JumpMovementCommand : IMovementCommand
    {
        private Vector3 _movement;

        public JumpMovementCommand(Unit targetUnit, Vector3 movement)
        {
            Unit = targetUnit;
            _movement = movement;
        }

        public override bool Execute()
        {
            var canExecute = Unit.CanExecuteCommand;
            if (canExecute)
            {
                Unit.StartJump(_movement);
            }

            return canExecute;
        }

        public override bool Undo()
        {
            var canExecute = Unit.CanExecuteCommand;
            if (canExecute)
                Unit.StartJump(-_movement);
            return canExecute;
        }
    }
}