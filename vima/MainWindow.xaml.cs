using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using vima.Commands;
using vima.ViewModels;

namespace vima
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MappingsSourceViewModel _current;
        private bool _isPlaying = false;

        public MainWindow()
        {
            InitializeComponent();
            LoadSource(new MappingsSourceViewModel());
            
        }

        #region : Events :

        public void AddFiles(object sender, RoutedEventArgs arguments)
        {
            var addCommand = new AddVideoFilesCommand(_current);
            addCommand.Execute(arguments);

            if (_current.Mappings.Any())
                ConvertFilesMenuItem.IsEnabled = true;
        }

        public void SelectFolder(object sender, RoutedEventArgs arguments)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();

            _current.DestinationPath = dialog.SelectedPath;
            
        }

        public void SaveMappingsFile(object sender, RoutedEventArgs arguments)
        {
            var saveCommand = new SaveFileCommand(_current);
            saveCommand.Execute(arguments);
        }

        public void OpenFile(object sender, RoutedEventArgs arguments)
        {
            var openCommand = new OpenFileCommand();
            openCommand.Execute(arguments);

            LoadSource(openCommand.MappingsSource);
        }

        public void ConvertFiles(object sender, RoutedEventArgs arguments)
        {
            new ConvertCommand(_current).Execute(arguments);
        }
        
        public void SetCurrentMedia(object sender, SelectionChangedEventArgs arguments)
        {
            _current.CurrentSelection = arguments.AddedItems[0] as MappingViewModel;
            CurrentPreview.Pause();

        }

        public void PreviewContentLoaded(object sender, RoutedEventArgs arguments)
        {
            CurrentPreview.Position = new TimeSpan(0, 0, 5);
            CurrentPreview.Play();
            CurrentPreview.Pause();
        }

        public void ChangePreviewContentViewingState(object sender, RoutedEventArgs arguments)
        {
            if (_isPlaying)
            {
                _isPlaying = false;
                CurrentPreview.Pause();
                return;
            }

            _isPlaying = true;
            CurrentPreview.Play();
            
        }

        #endregion

        #region : Helpers :

        private void LoadSource(MappingsSourceViewModel source)
        {
            _current = source;
            DataContext = _current;

            if (source == null) return;

            SaveFilesMenuItem.IsEnabled = true;
            AddFilesMenuItem.IsEnabled = true;
            if (source.Mappings.Any())
            {
                ConvertFilesMenuItem.IsEnabled = true;
            }
        }


        #endregion
    }
}
