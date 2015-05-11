using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CuttingEdge.Conditions;

namespace vima.domain
{
    public class MappingsSource
    {
        #region : Members :

        private readonly List<Mapping> _mappings; 

        #endregion
        
        #region : Constructors :

        private MappingsSource(string fileName)
        {
            Condition.Requires(fileName, "fileName").IsNotNullOrEmpty();
            
            FullName = fileName;
            Name = Path.GetFileNameWithoutExtension(fileName);
            Location = Path.GetDirectoryName(fileName);

            _mappings = new List<Mapping>();
        }

        public MappingsSource(string fileName, IEnumerable<string> files)
            : this(fileName)
        {
            if (files.HasElements())
            {
                _mappings.AddRange(files.Select(f => new Mapping(f)));
            }
        }

        public MappingsSource(string fileName, IEnumerable<Mapping> mappings)
            : this(fileName)
        {
            Condition.Requires(mappings, "mappings").IsNotNull();

            if (mappings.HasElements())
            {
                _mappings.AddRange(mappings);
            }
        }

        #endregion

        #region : Properties :

        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Location { get; private set; }

        public IReadOnlyCollection<Mapping> Mappings => _mappings;

        #endregion

        #region : Helpers :

        private string GetDesiredName(string sourceName)
        {
            var mapping = _mappings
                .FirstOrDefault(m => m.SourceName.Equals(sourceName, StringComparison.CurrentCultureIgnoreCase));

            return mapping == null
                ? string.Empty
                : mapping.DesiredName;
        }

        #endregion
    }
}
