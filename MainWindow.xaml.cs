using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Windows;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using System.Collections.Generic;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ApplicationDbContext context;
    private readonly UserController UserController;
    private readonly ProductController ProductCrontroller;

    private readonly string CurrentDir = Path.GetFullPath(@"..\..\..\");

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

    User LoggedUser = new();

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

        MainWindowUserName.Text = LoggedUser.Name;
        SearchBoxGrid.DataContext = searchText;
        SearchBox.Text = searchText.Text;

        loadUserImage();
    }

    private void loadUserImage()
    {
        // Create a BitmapSource
        BitmapImage bitmap = new BitmapImage();
        bitmap.BeginInit();

        if (LoggedUser.Profile_pic == string.Empty)
        {
            bitmap.UriSource = new Uri(Path.Combine(CurrentDir, "PersistentData", "ProfilePictures", "unknown_user.jpg"), UriKind.Absolute);
        }
        else
        {
            bitmap.UriSource = new Uri(Path.Combine(CurrentDir, "PersistentData", "ProfilePictures", LoggedUser.Profile_pic), UriKind.Absolute);
        }
        bitmap.EndInit();
        // Add Image to Window
        MainWindowProfilePic.Source = bitmap;
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
        //bool success = this.UserController.CreateUser(loginAttempt.Name, loginAttempt.Pwd);
        //if (success)
        //{
        //    this.isLogedIn = true;
        //    UserControlLoggedOff.Visibility = Visibility.Collapsed;
        //    UserControlLoggedIn.Visibility = Visibility.Visible;
        //    return;
        //}
        //loginAttempt = new();
        //LoginForm.DataContext = loginAttempt;

        RegisterWindow registerWindow = new RegisterWindow();
        registerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        registerWindow.Show();
    }

    private void TryLogin(object sender, RoutedEventArgs e)
    {
        User? result = this.UserController.Logon(loginAttempt.Name, loginAttempt.Pwd);
        if (result != null)
        {
            this.isLogedIn = true;


            UserControlLoggedOff.Visibility = Visibility.Collapsed;
            UserControlLoggedIn.Visibility = Visibility.Visible;


            LoggedUser = result;
            loadUserImage();

            MainWindowUserName.Text = LoggedUser.Name;
            
            return;
        }
        string messageBoxText = "Credenciais inválidas";
        string caption = "Error";
        MessageBoxButton button = MessageBoxButton.OK;
        MessageBoxImage icon = MessageBoxImage.Warning;
        MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        loginAttempt = new();
        LoginForm.DataContext = loginAttempt;
    }

    private void Logout(object sender, RoutedEventArgs e)
    {
        loginAttempt = new();
        LoggedUser = new();
        LoginForm.DataContext = loginAttempt;
        MainWindowUserName.Text = LoggedUser.Name;
        this.isLogedIn = false;

        loadUserImage();

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

