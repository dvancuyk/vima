using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace vima.domain
{
    public class MappingSourcePublisher
    {
        #region : Constructors : 

        public MappingSourcePublisher()
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

        public void Publish(MappingsSource mappingsSource)
        {
            var directoryName = mappingsSource.Location;
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            using (var writer = new StreamWriter(mappingsSource.FullName, false))
            {
                PublishLines(writer, SourceTranslator.GetHeaderInformation(mappingsSource));
                PublishLines(writer, SourceTranslator.FormatMappings(mappingsSource));
                PublishLines(writer, SourceTranslator.GetFooter(mappingsSource));
            }
        }

        #endregion

        private static void PublishLines(TextWriter writer, IEnumerable<string> content)
        {
            if (content == null || !content.Any())
            {
                return;
            }

            foreach (var line in content)
            {
                writer.WriteLine(line);
            }

            writer.Flush();
        } 
    }
}
