using System.Text;
using System.Windows;
using The_Movie.ViewModels;

namespace The_Movie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// REN MVVM - Minimal Code-Behind!
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // Ingen Click event handlers! 
        // Al forretningslogik er flyttet til ViewModel via ICommand
    }
}