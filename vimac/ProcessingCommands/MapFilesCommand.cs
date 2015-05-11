using System;
using System.IO;
using System.Linq;
using vima.domain;
using vimac.Logging;

namespace vimac.ProcessingCommands
{
    public class MapFilesCommand : IProcessingCommand
    {
        #region : Constructors :

        public MapFilesCommand(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region : Properties :

        public ILogger Logger { get; private set; }

        #endregion

        #region : Members :

        private string[] _argumentFlags;
        private const string DefaultFileName = "mappings.text";

        #endregion

        #region : IProcessingCommand Members :

        public bool ShouldProcess(string[] argumentFlags)
        {
            _argumentFlags = argumentFlags;
            return !argumentFlags.HasArgumentValue(Argument.GenerateMappingFileArgument);
        }

        public void Execute(ProcessingOptions options)
        {
            var outputDirectory = options.OutputLocation;
            var files = options.Files;

            if (!files.HasElements()) return;

            var mappingFileName = _argumentFlags.GetArgumentValue(Argument.MappingsArgument, DefaultFileName);
            if (Path.GetFileName(mappingFileName) == mappingFileName)
            {
                mappingFileName = Path.Combine(Path.GetDirectoryName(files.First()), mappingFileName);
            }

            if (!File.Exists(mappingFileName))
            {
                Logger.Log(LoggingLevel.Warning, "The file {0} is expected for mapping the files but +does not exist.", mappingFileName);
                return;
            }

            var source = new MappingsSource(mappingFileName, options.Files);
            var mapper = new Mapper();
            mapper.Map(source);
        }

        #endregion

    }
}
