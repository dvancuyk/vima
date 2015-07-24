
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using vima.Annotations;
using vima.domain;

namespace vima.ViewModels
{
    public class MappingsSourceViewModel : INotifyPropertyChanged
    {
        #region : Members :

        private string _destinationFile;
        private string _currentFile;
        private MappingViewModel _currentSelection;

        #endregion

        #region : Properties :

        public string FileName { get; set; }
        public MappingsCollection Mappings { get; set; }

        public MappingViewModel CurrentSelection
        {
            get { return _currentSelection; }
            set
            {
                if (value == null || _currentSelection == value)
                {
                    _currentSelection = value;
                }
                _currentSelection = value;

                OnPropertyChanged();
            }
        }

        public string CurrentFile
        {
            get { return _currentFile; }
            set
            {
                if (_currentFile != value)
                {
                    OnPropertyChanged();
                }
                _currentFile = value;
            }
        }
        public string DestinationPath
        {
            get { return _destinationFile; }
            set
            {
                if(_destinationFile != value)
                {
                    OnPropertyChanged();
                }

                _destinationFile = value;
            }

        }

        #endregion

        #region : Constructors :

        public MappingsSourceViewModel()
        {
            Mappings = new MappingsCollection();
            DomainEvents.Register<MappingViewSelected>(HandleViewSelected);
        }

        #endregion

        #region : Events :

        public void HandleViewSelected(MappingViewSelected selectedEvent)
        {
            CurrentFile = selectedEvent.SelectedViewModel.FullPath;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public MappingsSource ToDomainEntity()
        {
            return  new MappingsSource(FileName,
                    Mappings.Select(mapping => new Mapping(mapping.FullPath)
                    {
                        DesiredName = mapping.Desired
                    }));
        }
    }

}
