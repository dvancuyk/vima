using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using vima.Annotations;

namespace vima.ViewModels
{
    public class MappingViewModel : INotifyPropertyChanged
    {
        #region : Members :

        private string _desired;

        #endregion

        #region : Constructors :

        public MappingViewModel()
        {
            
        }

        public MappingViewModel(string sourcePath)
        {
            FullPath = sourcePath;
            Source = Path.GetFileNameWithoutExtension(sourcePath);
        }

        #endregion

        #region : Properties :

        public string FullPath { get; set; }
        public string Source { get; set; }

        public string Desired
        {
            get { return _desired; }
            set
            {
                if (!_desired.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                {
                    OnPropertyChanged();
                }

                _desired = value;
            }
        }

        #endregion

        #region : Methods :

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}