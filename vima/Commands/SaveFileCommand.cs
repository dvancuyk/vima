using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using vima.domain;
using vima.ViewModels;

namespace vima.Commands
{
    public class SaveFileCommand : RoutedUICommand, IRoutedCommand
    {
        #region : Members :

        private readonly MappingsSourceViewModel _mappingsSource;

        #endregion

        #region : Constructors :

        public SaveFileCommand(MappingsSourceViewModel receiver)
            : base("Save File", "Save File", typeof(AddVideoFilesCommand), new InputGestureCollection
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            })
        {
            _mappingsSource = receiver;
        }

        #endregion

        public bool CanExecute(object parameter)
        {
            return _mappingsSource != null;
        }

        public void Execute(object parameter)
        {
            if (!string.IsNullOrEmpty(_mappingsSource.FileName))
            {
                Publish();
                return;
            }
            var openFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _mappingsSource.FileName = openFileDialog.FileName;
                Publish();
            }
        }

        private void Publish()
        {
            var publisher = new MappingSourcePublisher();
            publisher.Publish(_mappingsSource.ToDomainEntity());
        }

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanExecute(sender);
        }

        public void Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
