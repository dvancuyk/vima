using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using vima.domain;
using vimac.Logging;

namespace vimac
{
    class Program
    {
        #region : Members :

        private static ILogger _logger;
        private static string _outputDirectory;

        #endregion

        static void Main(string[] args)
        {
            _logger = new ConsoleLogger();

            if (args.HasArgumentValue(Argument.HelpArgument))
            {
                PrintHelp();
            }
            else if (args.HasArgumentValue(Argument.GenerateMappingFileArgument))
            {
                GenerateMappingsFile(args);
            }
            else
            {
                MapFiles(args);
            }

            Console.ReadLine();
        }

        private static void SetOutptDirectory(string[] arguments)
        {
            if (!arguments.HasArgumentValue(Argument.OutputDirectoryArgument))
            {
                Console.WriteLine("Enter a directory: ");
                _outputDirectory = Console.ReadLine();
            }
            else
            {
                _outputDirectory = arguments.GetArgumentValue(Argument.OutputDirectoryArgument);
            }

            if (!Directory.Exists(_outputDirectory))
            {
                _logger.Log(LoggingLevel.Warning, "The provided directory '{0}' does not exist.", _outputDirectory);
            }
        }

        private static IList<string> RetrieveFiles(string[] args)
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(_outputDirectory)
                .Where(file => Path.GetExtension(file).EndsWith("mp4")));

            if (!args.HasArgumentValue(Argument.RegexArgument))
            {
                return files;
            }

            var pattern = args.GetArgumentValue(Argument.RegexArgument);
            return files.Where(file => Regex.IsMatch(Path.GetFileNameWithoutExtension(file), pattern)).ToList();
        }

        private static void MapFiles(string[] arguments)
        {
            SetOutptDirectory(arguments);

            var reader = new MappingsSourceReader();

            var mappingSource = reader.Read(
                arguments.GetArgumentValue(Argument.MappingsArgument, "mappings.txt"));

            var mapper = new Mapper();

            if (arguments.HasArgumentValue(Argument.UndoArgument))
            {
                mapper.Direction = MappinDirection.Reverse;
            }

            mapper.Map(mappingSource);

            _logger.Log(LoggingLevel.Info, "{0} files mapped from the instructions foudn in {1}",
                mapper.MappedFiles, mappingSource.FullName);

        }
        private static void GenerateMappingsFile(string[] arguments)
        {
            SetOutptDirectory(arguments);
            var files = RetrieveFiles(arguments);

            var mappingsFileName = arguments.GetArgumentValue(Argument.MappingsArgument, "mappings.txt");


            var mappingSource = new MappingsSource(Path.Combine(_outputDirectory, mappingsFileName),
                files);

            new MappingSourcePublisher()
                .Publish(mappingSource);

            _logger.Log(LoggingLevel.Info, "{0} files added to the file {1}", files.Count(), mappingsFileName);
        }

        private static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine(ArgumentsResource.HelpDescription);

            foreach (var argument in Argument.AvailableArguments)
            {
                Console.WriteLine();
                Console.WriteLine("{0}\t{1}\t{2}", argument.Name, argument.ShortHand, argument.Description);
            }
        }
    }
}
