using System.IO;

namespace vima.domain
{
    /// <summary>
    /// Contains the information for a single mapping between an existing file and the desired name.
    /// </summary>
    public class Mapping
    {
        #region : Members :

        private string _desiredName;
        
        #endregion

        #region : Constructors :

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping"/> class.
        /// </summary>
        /// <param name="fileName">Name of the source.</param>
        public Mapping(string fileName)
        {
            SourceName = Path.GetFileNameWithoutExtension(fileName).Trim();
            Extension = Path.GetExtension(fileName);
            Location = Path.GetDirectoryName(fileName);
        }

        #endregion

        #region : Properties :

        /// <summary>
        /// Gets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName { get; private set; }

        /// <summary>
        /// Gets the location where the source is located.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; private set; }

        public string Extension { get; private set; }

        /// <summary>
        /// Gets or sets the desired name of the file.
        /// </summary>
        /// <value>
        /// The name of the desired.
        /// </value>
        public string DesiredName
        {
            get { return _desiredName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace(Location, string.Empty).Trim();
                }

                _desiredName = value;
            }
        }

        public bool HasNewName => !string.IsNullOrEmpty(DesiredName);

        #endregion

        #region : Methods :

        public string GetOriginalFileName()
        {
            return Path.Combine(Location, SourceName) + Extension;
        }

        public string GetDesiredFileName()
        {
            return string.IsNullOrEmpty(DesiredName)
                ? string.Empty
                : Path.Combine(Location, DesiredName) + Extension;
        }

        #endregion
    }
}
