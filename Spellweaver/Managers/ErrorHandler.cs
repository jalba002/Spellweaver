using Spellweaver.Interfaces;
using System.Windows;

namespace Spellweaver.Managers;
public class ErrorHandler : IErrorHandler
{
    private static void ThrowMessageBoxError(string message, string title = "Error")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void HandleError(Exception ex)
    {
        ThrowMessageBoxError(ex.Message);
    }
}
