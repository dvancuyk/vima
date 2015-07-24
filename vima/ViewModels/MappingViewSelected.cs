using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vima.domain;

namespace vima.ViewModels
{
    public class MappingViewSelected : IDomainEvent
    {
        public MappingViewSelected(MappingViewModel selectedViewModel)
        {
            SelectedViewModel = selectedViewModel;
        }

        public MappingViewModel SelectedViewModel { get; private set; }
    }
}
