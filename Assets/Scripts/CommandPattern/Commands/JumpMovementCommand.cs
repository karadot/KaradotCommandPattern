using UnityEngine;

namespace CommandPattern.Commands
{
    public class JumpMovementCommand : MovementCommandBase
    {
        public JumpMovementCommand(Unit unit, Vector3 movement) : base(unit, movement)
        {
        }

        public override bool Execute()
        {
            var canExecute = Unit.CanMove;
            if (canExecute)
            {
                Unit.StartJump(Movement);
            }

            return canExecute;
        }

        public override bool Undo()
        {
            var canExecute = Unit.CanMove;
            if (canExecute)
                Unit.StartJump(-Movement);
            return canExecute;
        }
    }
}