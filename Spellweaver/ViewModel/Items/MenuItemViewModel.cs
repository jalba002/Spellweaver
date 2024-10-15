using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Spellweaver.ViewModel
{
    //https://stackoverflow.com/questions/23941314/wpf-how-can-i-create-menu-and-submenus-using-binding
    public class MenuItemViewModel
    {
        private ICommand _command;
        public ICommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }
        public string Header { get; set; }
        public MenuItemViewModel()
        {
            
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
