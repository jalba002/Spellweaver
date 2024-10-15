using System.Windows.Input;

namespace Spellweaver.ViewModel
{
    // https://stackoverflow.com/questions/23941314/wpf-how-can-i-create-menu-and-submenus-using-binding
    public class CommandViewModel : ICommand
    {
        private readonly Action _action;

        public CommandViewModel(Action action)
        {
            _action = action;
        }

        public void Execute(object o)
        {
            _action();
        }

        public bool CanExecute(object o)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
