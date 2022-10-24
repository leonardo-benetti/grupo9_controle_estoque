using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace grupo9_controle_estoque
{
    /// <summary>
    /// Interaction logic for Description.xaml
    /// </summary>
    public partial class Description : MetroWindow
    {
        private Product product;
        public Description(Product product, Theme? theme)
        {
            this.product = product;
            InitializeComponent();
            if (theme != null)
                ThemeManager.Current.ChangeTheme(this, theme);
            ProductTextGrid.DataContext = product;       
        }
        private void CloseDescription(object s, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
