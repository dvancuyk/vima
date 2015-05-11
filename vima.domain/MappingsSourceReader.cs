using System.IO;
using CuttingEdge.Conditions;

namespace vima.domain
{
    /// <summary>
    /// Service provider which reads a file and extracts the mappings from the file and converts the information into a <see cref="MappingsSource"/> entity.
    /// </summary>
    public class MappingsSourceReader
    {
        #region : Constructors :

        public MappingsSourceReader()
        {
            SourceTranslator = new SimpleSourceTranslator();
        }

        #endregion
        
        #region : Properties :

        /// <summary>
        /// Gets or sets the component which translates the mappings to the desired format for publishing. Default is pipe-delimited source.
        /// </summary>
        public ISourceTranslator SourceTranslator { get; set; }


        #endregion

        #region : Methods :

        public MappingsSource Read(string fileName)
        {
            Condition.Requires(fileName, "fileName").IsNotNullOrEmpty();

            return new MappingsSource(fileName, 
                SourceTranslator.Parse(File.ReadAllLines(fileName)));
        }

        #endregion
    }
}
