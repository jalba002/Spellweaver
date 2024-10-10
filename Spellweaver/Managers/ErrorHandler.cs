using System.Windows;

namespace Spellweaver.Managers;
internal static class ErrorHandler
{
    internal static void ThrowMessageBoxError(string message, string title = "Error")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
