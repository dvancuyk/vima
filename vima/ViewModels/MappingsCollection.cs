using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vima.domain;

namespace vima.ViewModels
{
    public class MappingsCollection : ObservableCollection<MappingViewModel>
    {
        public MappingsCollection()
        {
            
        }

        public MappingsCollection(IEnumerable<Mapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                Items.Add(new MappingViewModel
                {
                    Desired = mapping.DesiredName,
                    FullPath = mapping.GetOriginalFileName(),
                    Source = mapping.SourceName
                });
            }
        }
    }
}
