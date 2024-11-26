using System.Windows.Data;

namespace Spellweaver.Extensions
{
    public class SettingBindingExtension : Binding
    {
        public SettingBindingExtension()
        {
            Initialize();
        }

        public SettingBindingExtension(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Source = Spellweaver.Properties.Settings.Default;
            this.Mode = BindingMode.TwoWay;
        }
    }
}
