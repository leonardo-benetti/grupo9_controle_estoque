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
    private ProductController productController;
    private List<Product> productList;
    public EditWindow(Product product, ProductController productController)
    {
        this.product = product;
        this.productController = productController;
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
        this.productList.Add(newProduct);
        ProductEditGrid.ItemsSource = this.productList;
    }

    private void SaveChanges(object s, RoutedEventArgs e)
    {
        this.productController.EditProduct(this.product.GUID, this.newProduct);
        this.Close();
    }

    private void DiscartChanges(object s, RoutedEventArgs e)
    {
        this.Close();
    }
}

