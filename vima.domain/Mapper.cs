using System.IO;
using System.Linq;

namespace vima.domain
{
    public enum MappinDirection
    {
        Normal,
        Reverse
    }

    public class Mapper
    {
        #region : Constructors :

        public Mapper()
        {
            Direction = MappinDirection.Normal;
        }

        #endregion

        #region : Properties :

        public int MappedFiles { get; private set; }
        /// <summary>
        /// Indicates the direction the mapping source should be mapped. If the mode is set to normal, the mapper will map
        /// from the source name to the desired name. If set to Reverse, it will map from the desired name to the original name effectively undoing 
        /// a previously requested mapping process.
        /// </summary>
        public MappinDirection Direction  { get; set; }

        #endregion

        #region : Methods :

        public void Map(MappingsSource source)
        {
            MappedFiles = 0;

            foreach (var mapping in source.Mappings.Where(m => m.HasNewName && File.Exists(m.SourceName))
                .Select(GetFileInfo))
            {
                File.Move(mapping.Source, mapping.Destination);

                MappedFiles++;
            }
        }

        #endregion

        #region : Helpers : 

        private MappingFileInfo GetFileInfo(Mapping mapping)
        {
            return Direction == MappinDirection.Normal
                ? new MappingFileInfo(mapping.GetOriginalFileName(), mapping.GetDesiredFileName())
                : new MappingFileInfo(mapping.GetDesiredFileName(), mapping.GetOriginalFileName());

        }
            
        private struct MappingFileInfo
        {
            public MappingFileInfo(string source, string destination)
            {
                Source = source;
                Destination = destination;

            }
            public string Source { get; private set; }

            public string Destination { get; private set; }
        }

        #endregion
    }
}
