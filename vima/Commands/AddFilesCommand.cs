using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using vima.ViewModels;

namespace vima.Commands
{
    /// <summary>
    /// Encpaulsates operational commands for adding video files to a mapping source.
    ///  </summary>
    public class AddVideoFilesCommand : RoutedUICommand, IRoutedCommand
    {
        #region : Members :

        private readonly MappingsSourceViewModel _mappingsSource;

        #endregion

        #region : Constructors :

        public AddVideoFilesCommand(MappingsSourceViewModel receiver)
            : base("Add Files", "Add Files", typeof(AddVideoFilesCommand), new InputGestureCollection
            {
                new KeyGesture(Key.A, ModifierKeys.Control | ModifierKeys.Shift)
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
            var openFileDialog = new OpenFileDialog
            {
                Filter = "MP4 files (*.mp4)|*.mp4|AVI files (*.avi)|*.avi",
                Multiselect = true

            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var fileName in openFileDialog.FileNames)
                {
                    if (_mappingsSource.Mappings.Any(mapping => mapping.FullPath.Trim().Equals(fileName, StringComparison.CurrentCultureIgnoreCase)))
                        continue;

                    _mappingsSource.Mappings.Add(new MappingViewModel(fileName));
                }
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
