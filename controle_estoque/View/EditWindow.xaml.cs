using System;
using System.Collections.Generic;
using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Windows;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for EditWindow.xaml
/// </summary>
public partial class EditWindow : MetroWindow
{
    private Product product;
    private Product newProduct;
    private ProductController productController;
    private List<Product> productList;
    public EditWindow(Product product, ProductController productController, Theme? theme)
    {
        this.product = product;
        this.productController = productController;
        this.newProduct = new Product() {
            Id = product.Id,
            Category = product.Category,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description
        };
        InitializeComponent();
        if (theme != null)
            ThemeManager.Current.ChangeTheme(this, theme);
        this.productList = new List<Product>();
        this.productList.Add(newProduct);
        ProductEditGrid.ItemsSource = this.productList;
    }

    private void SaveChanges(object s, RoutedEventArgs e)
    {
        this.productController.EditProduct(this.product.Id, this.newProduct);
        this.Close();
    }

    private void DiscartChanges(object s, RoutedEventArgs e)
    {
        this.Close();
    }
}

