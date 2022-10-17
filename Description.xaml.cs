using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using MahApps.Metro.Controls;

namespace grupo9_controle_estoque
{
    /// <summary>
    /// Interaction logic for Description.xaml
    /// </summary>
    public partial class Description : MetroWindow
    {
        private Product product;
        public Description(Product product)
        {
            this.product = product;
            InitializeComponent();
            ProductTextGrid.DataContext = product;       
        }
        private void CloseDescription(object s, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
