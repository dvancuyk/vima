using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using vima.Commands;

namespace vima.ViewModels
{
    public class MappingsSourceViewModel
    {
        #region : Properties :

        public ICollection<MappingViewModel> Mappings { get; set; }
        public string FileName { get; set; }

        public ICommand AddFilesCommand { get; set; }

        #endregion

        #region : Constructors :

        public MappingsSourceViewModel()
        {
            AddFilesCommand = new AddVideoFilesCommand(this);
        }

        #endregion

        #region : Helpers :


        #endregion
    }

}
