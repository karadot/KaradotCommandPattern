using UnityEngine;

namespace CommandPattern.Commands
{
    public class SimpleMovementCommand : MovementCommandBase
    {
        public SimpleMovementCommand(Unit unit, Vector3 movement) : base(unit, movement)
        {
        }

        public override bool Execute()
        {
            var canExecute = Unit.CanMove;
            if (canExecute)
            {
                Unit.StartMove(Movement);
            }

            return canExecute;
        }

        public override bool Undo()
        {
            var canExecute = Unit.CanMove;
            if (canExecute)
                Unit.StartMove(-Movement);
            return canExecute;
        }
    }
}