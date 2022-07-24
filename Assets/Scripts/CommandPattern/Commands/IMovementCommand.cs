namespace CommandPattern.Commands
{
    public abstract class IMovementCommand
    {
        protected Unit Unit { get; set; }
        public bool CanExecute => Unit.CanExecuteCommand;
        public abstract bool Execute();
        public abstract bool Undo();
    }
}