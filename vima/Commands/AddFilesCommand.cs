using System;
using System.Windows.Input;
using Microsoft.Win32;
using vima.ViewModels;

namespace vima.Commands
{
    /// <summary>
    /// Encpaulsates operational commands for adding video files to a mapping source.
    ///  </summary>
    public class AddVideoFilesCommand : SimpleCommand
    {
        #region : Members :

        private readonly MappingsSourceViewModel _mappingsSource;

        #endregion

        #region : Constructors :

        public AddVideoFilesCommand(MappingsSourceViewModel receiver)
        {
            _mappingsSource = receiver;
            CanExecuteDelegate = (parameter) => _mappingsSource != null;
            ExecuteDelegate = (parameter) => SelectFiles();
        }

        #endregion

        private void SelectFiles()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "MP4 files (*.mp4)|*.mp4|AVI files (*.avi)|*.avi",
                Multiselect = true

            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    _mappingsSource.Mappings.Add(new MappingViewModel(fileName));
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        public void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanExecute(sender);
        }

        public void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }
    }
}
