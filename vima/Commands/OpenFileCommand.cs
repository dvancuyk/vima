using System.Windows.Input;
using Microsoft.Win32;
using vima.domain;
using vima.ViewModels;

namespace vima.Commands
{
    public class OpenFileCommand : RoutedUICommand, IRoutedCommand
    {
        #region : Properties :

        public MappingsSourceViewModel MappingsSource { get; private set; }

        #endregion

        #region : Constructors :

        public OpenFileCommand()
            : base("Open File", "Open File", typeof(AddVideoFilesCommand), new InputGestureCollection
            {
                new KeyGesture(Key.O, ModifierKeys.Control | ModifierKeys.Shift)
            })
        {

        }

        #endregion

        #region : Properties :



        #endregion

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = Constants.FileFilter,
                Multiselect = false

            };

            if (openFileDialog.ShowDialog() == true)
            {
                var reader = new MappingsSourceReader();
                var source = reader.Read(openFileDialog.FileName);

                MappingsSource = new MappingsSourceViewModel
                {
                    FileName = openFileDialog.FileName,

                    Mappings = new MappingsCollection(source.Mappings)
                };
            }
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
