using System;
using System.Collections.Generic;

namespace vimac
{
    /// <summary>
    /// Defines an argument which 
    /// </summary>
    public class Argument
    {
        #region : Constructors :

        public Argument(string name, string shortHand, string description)
        {
            ShortHand = shortHand;
            Name = name;
            Description = description;
        }

        #endregion

        #region : Constant Members :

        public static readonly Argument RegexArgument = new Argument("pattern", "p", ArgumentsResource.RegexDescription);

        public static readonly Argument GenerateMappingFileArgument = new Argument("generate", "g",
            ArgumentsResource.GenerateFileDescription);

        public static readonly Argument MappingsArgument = new Argument("mappingFile", "m",
            ArgumentsResource.MappingsFileDescription);

        public static readonly Argument OutputDirectoryArgument = new Argument("output", "o",
            ArgumentsResource.OutputDirectoryDescription);

        public static readonly Argument HelpArgument = new Argument("help", "h", ArgumentsResource.HelpDescription);

        public static readonly Argument UndoArgument = new Argument("undo", "u", ArgumentsResource.UndoDescription);

        #endregion

        #region : Properties :

        public static IEnumerable<Argument> AvailableArguments => new List<Argument>
        {
            RegexArgument,
            GenerateMappingFileArgument,
            MappingsArgument,
            OutputDirectoryArgument,
            HelpArgument,
            UndoArgument
        };

        public string ShortHand { get; }
        public string Name { get; }
        public string Description { get; }

        #endregion

        #region : Methods :

        public virtual bool Matches(string value)
        {
            var argumentValue = value.Replace("-", string.Empty);
            return argumentValue.Equals(ShortHand, StringComparison.CurrentCultureIgnoreCase)
                   || argumentValue.Equals(Name, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion
    }
}
