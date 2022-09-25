using grupo9_controle_estoque.Model;
using System.Windows;

namespace grupo9_controle_estoque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext context;

        public MainWindow(ApplicationDbContext context)
        {
            this.context = context;
            InitializeComponent();
        }
    }
}
