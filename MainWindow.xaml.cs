﻿using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using grupo9_controle_estoque;
using System.Collections.Generic;
using System.Windows;
using System;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ApplicationDbContext context;
    private readonly UserController UserController;
    private readonly ProductController ProductCrontroller;
    private class SearchText
    {
        public string Text { get; set; } = "Filtro";
    }
    private bool isLogedIn = false;

    private class LoginAttempt
    {
        public string Name { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
    }

    private LoginAttempt loginAttempt = new();
    private SearchText searchText = new();
    Product NewProduct = new();
    Product selectedProduct = new();
    public MainWindow(ApplicationDbContext context)
    {
        this.context = context;
        this.UserController = new UserController(context);
        this.ProductCrontroller = new ProductController(context);
        InitializeComponent();
        GetProducts();
        AddItemGrid.DataContext = NewProduct;
        LoginForm.DataContext = loginAttempt;
        MainWindowUserName.Text = loginAttempt.Name;
        SearchBoxGrid.DataContext = searchText;
        SearchBox.Text = searchText.Text;
    }
    private void GetFilteredProducts(object s, RoutedEventArgs e)
    {
        if(this.searchText.Text != "")
        {
            ProductDataGrid.ItemsSource = this.ProductCrontroller.GetProducts().FindAll(product => product.Name.Contains(this.searchText.Text, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
        else
        {
            GetProducts();
        }

    }
    private void GetProducts()
    {
        ProductDataGrid.ItemsSource = this.ProductCrontroller.GetProducts();
    }

    private void EditProduct(object s, RoutedEventArgs e)
    {
        selectedProduct = (s as FrameworkElement).DataContext as Product;
        this.ProductCrontroller.EditProduct(selectedProduct.GUID, NewProduct);
        NewProduct = new Product();
        GetProducts();
    }

    private void DeleteProduct(object s, RoutedEventArgs e)
    {
        var productToDelete = (s as FrameworkElement).DataContext as Product;
        this.ProductCrontroller.DeleteProduct(productToDelete);
        GetProducts();
    }
    private void AddItem(object s, RoutedEventArgs e)
    {
        this.ProductCrontroller.AddItem(NewProduct);
        GetProducts();
        NewProduct = new Product();
        AddItemGrid.DataContext = NewProduct;
    }
    private void ProductDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
    private void AddUser(object s, RoutedEventArgs e)
    {
        bool success = this.UserController.CreateUser(loginAttempt.Name, loginAttempt.Pwd);
        if (success)
        {
            this.isLogedIn = true;
            UserControlLoggedOff.Visibility = Visibility.Collapsed;
            UserControlLoggedIn.Visibility = Visibility.Visible;
            return;
        }
        loginAttempt = new();
        LoginForm.DataContext = loginAttempt;
    }

    private void TryLogin(object sender, RoutedEventArgs e)
    {
        bool success = this.UserController.Logon(loginAttempt.Name, loginAttempt.Pwd);
        if (success)
        {
            this.isLogedIn = true;
            UserControlLoggedOff.Visibility = Visibility.Collapsed;
            UserControlLoggedIn.Visibility = Visibility.Visible;
            MainWindowUserName.Text = loginAttempt.Name;
            return;
        }
        loginAttempt = new();
        LoginForm.DataContext = loginAttempt;
    }

    private void Logout(object sender, RoutedEventArgs e)
    {
        loginAttempt = new();
        LoginForm.DataContext = loginAttempt;
        MainWindowUserName.Text = loginAttempt.Name;
        this.isLogedIn = false;
        UserControlLoggedIn.Visibility = Visibility.Collapsed;
        UserControlLoggedOff.Visibility = Visibility.Visible;
    }
    private void SelectProductToSeeDescription(object s, RoutedEventArgs e)
    {
        var productDescription = (s as FrameworkElement).DataContext as Product;
        Description windowDescription = new Description(productDescription);
        windowDescription.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        windowDescription.Show();
    }

    private void SelectProductToEdit(object s, RoutedEventArgs e)
    {
        var productToEdit = (s as FrameworkElement).DataContext as Product;
        EditWindow editWindow = new EditWindow(productToEdit, this.ProductCrontroller);
        editWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        editWindow.Show();
    }
}

