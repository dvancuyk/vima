using System.Collections.Generic;
using CuttingEdge.Conditions;
using vimac.ProcessingCommands;

namespace vimac
{
    /// <summary>
    ///     Options which are provided to an instance of the <see cref="IProcessingCommand" />
    /// </summary>
    public class ProcessingOptions
    {
        #region : Members :

        private readonly List<string> _files;

        #endregion

        #region : Constructors :

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessingOptions" /> class.
        /// </summary>
        /// <param name="outputLocation">The output location.</param>
        /// <param name="files">The files.</param>
        public ProcessingOptions(string outputLocation, IEnumerable<string> files)
        {
            Condition.Requires(outputLocation, "outputLocation").IsNotNullOrEmpty();
            Condition.Requires(files, "files").IsNotNull();

            OutputLocation = outputLocation;
            _files = new List<string>(files);
        }

        #endregion

        #region : Properties :

        /// <summary>
        ///     Gets the directory where any output generated from the processing command is placed.
        /// </summary>
        public string OutputLocation { get; private set; }

        /// <summary>
        ///     Gets the list of files which should be processed.
        /// </summary>
        public IReadOnlyCollection<string> Files => _files;

        #endregion
    }
}