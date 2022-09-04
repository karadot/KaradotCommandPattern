using UnityEngine;

namespace CommandPattern.Commands
{
    public abstract class MovementCommandBase : ICommand
    {
        public MovementCommandBase(Unit unit, Vector3 movement)
        {
            Unit = unit;
            Movement = movement;
        }

        protected Unit Unit { get; set; }

        protected Vector3 Movement;


        public bool CanExecute => Unit.CanMove;
        public abstract bool Execute();
        public abstract bool Undo();
    }
}