using System.Windows.Input;
using Microsoft.Win32;
using vima.domain;
using vima.ViewModels;

namespace vima.Commands
{
    public class ConvertCommand : RoutedUICommand, IRoutedCommand
    {
        #region : Members :

        private readonly MappingsSourceViewModel _mappingsSource;

        #endregion

        #region : Constructors :

        public ConvertCommand(MappingsSourceViewModel receiver)
            : base("Add Files", "Add Files", typeof (AddVideoFilesCommand), new InputGestureCollection
            {
                new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Alt)
            })
        {
            _mappingsSource = receiver;
        }

        #endregion

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanExecute(sender);
        }

        public void Executed(object sender, ExecutedRoutedEventArgs e)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _mappingsSource != null;
        }

        public void Execute(object parameter)
        {
            var mapper = new Mapper();
            mapper.Map(_mappingsSource.ToDomainEntity());
        }

    }
}