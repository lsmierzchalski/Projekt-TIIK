using Project_TIIK_WPF.ViewModels;
using System.Windows.Controls;

namespace Project_TIIK_WPF.Pages
{
    /// <summary>
    /// Interaction logic for TestLZ77Page.xaml
    /// </summary>
    public partial class TestLZ77Page : Page
    {
        public TestLZ77Page()
        {
            InitializeComponent();
            DataContext = new TestLZ77ViewModel();
        }
    }
}
