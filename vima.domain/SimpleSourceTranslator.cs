using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vima.domain
{
    public class SimpleSourceTranslator : ISourceTranslator
    {
        #region : Members :

        private const char Separator = '|';

        #endregion
        public string[] GetHeaderInformation(MappingsSource mappings)
        {
            return null;
        }

        public string[] GetFooter(MappingsSource mappings)
        {
            return null;
        }

        public IEnumerable<string> FormatMappings(MappingsSource mappings)
        {
            return mappings.Mappings
                .Select(m => string.Format("{0} {1} {2}", m.GetOriginalFileName(), Separator, m.GetDesiredFileName()));
        }

        public IEnumerable<Mapping> Parse(IEnumerable<string> lines)
        {
            if (!lines.HasElements())
            {
                return new Mapping[0];
            }

            var mappings = new List<Mapping>();

            foreach (var line in lines)
            {
                var components = line.Split(Separator);
                if (components.Length > 0)
                {
                    var mapping = new Mapping(components[0])
                    {
                        DesiredName = components.Length >= 2
                            ? components[1]
                            : string.Empty
                    };

                    mappings.Add(mapping);
                }
            }

            return mappings;
        }
    }
}
