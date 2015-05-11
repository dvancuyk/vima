using System.IO;
using System.Linq;
using vima.domain;
using vimac.Logging;

namespace vimac.ProcessingCommands
{
    public class GenerateMappingsFileCommand : IProcessingCommand
    {
        #region : Members :

        private string[] _arguments;

        #endregion

        #region : Constructors :

        public GenerateMappingsFileCommand(ILogger logger)
        {
            Logger = logger;
        }

        #endregion

        #region : Properties :

        public ILogger Logger { get; }

        #endregion

        #region : Methods :

        public bool ShouldProcess(string[] argumentFlags)
        {
            _arguments = argumentFlags;
            return argumentFlags.HasArgumentValue(Argument.GenerateMappingFileArgument);
        }

        public void Execute(ProcessingOptions options)
        {
            var mappingsFileName = _arguments.GetArgumentValue(Argument.MappingsArgument, "mappings.txt");
            var mappingSource = new MappingsSource(Path.Combine(options.OutputLocation, mappingsFileName),
                options.Files);

            new MappingSourcePublisher()
                .Publish(mappingSource);

            Logger.Log(LoggingLevel.Info, "{0} files added to the file {1}", options.Files.Count(), mappingsFileName);
        }

        #endregion
    }
}
