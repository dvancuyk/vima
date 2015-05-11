using System.Linq;
using System.Windows;
using vima.ViewModels;

namespace vima
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = RetrieveSource();
        }

        #region : Helpers :

        private MappingsSourceViewModel RetrieveSource()
        {
            const string directory = @"c:\fake";
            return new MappingsSourceViewModel
            {
                FileName = System.IO.Path.Combine(directory, "fakemappings.txt"),
                Mappings = Enumerable.Range(1, 5)
                    .Select(index => new MappingViewModel(System.IO.Path.Combine(directory, string.Format("fakevideo{0}", index))))
                    .ToList()
            };
        }

        #endregion
    }
}
