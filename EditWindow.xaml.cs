using System;
using System.Collections.Generic;
using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Windows;


namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for EditWindow.xaml
/// </summary>
public partial class EditWindow : Window
{
    private Product product;
    private Product newProduct;
    private List<Product> productList;
    public EditWindow(Product product)
    {
        this.product = product;
        this.newProduct = new Product() {
            GUID = product.GUID,
            Category = product.Category,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description
        };
        InitializeComponent();
        this.productList = new List<Product>();
        this.productList.Add(product);
        ProductEditGrid.ItemsSource = this.productList;
    }

    private void SaveChanges(object s, RoutedEventArgs e)
    {
        this.Close();
    }

    private void DiscartChanges(object s, RoutedEventArgs e)
    {
        this.Close();
    }
}

