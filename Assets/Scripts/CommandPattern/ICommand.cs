namespace CommandPattern
{
    public interface ICommand
    {
        public bool Execute();
        public bool Undo();
        public bool CanExecute { get; }
    }
}