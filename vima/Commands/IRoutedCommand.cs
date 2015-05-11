using System.Windows.Input;

namespace vima.Commands
{
    public interface IRoutedCommand
    {
        void CanExecute(object sender, CanExecuteRoutedEventArgs e);
        void Executed(object sender, ExecutedRoutedEventArgs e);
    }
}