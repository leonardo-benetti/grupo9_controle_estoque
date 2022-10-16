using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Windows;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ApplicationDbContext context;
    private readonly UserController UserController;
    private readonly ProductController ProductCrontroller;
    private readonly NotificationController NotificationController;

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

    private class SellProductData
    {
        public int ProductId { get; set; }
        public int SellQuantity { get; set; }
    }

    private LoginAttempt loginAttempt = new();

    private SellProductData sellProductData = new();

    User LoggedUser = new();

    private SearchText searchText = new();

    Product NewProduct = new();
    Product selectedProduct = new();
    public MainWindow(ApplicationDbContext context)
    {
        this.context = context;
        this.UserController = new UserController(context);
        this.ProductCrontroller = new ProductController(context);
        this.NotificationController = new NotificationController(context);
        InitializeComponent();
        GetProducts();
        AddItemGrid.DataContext = NewProduct;
        LoginForm.DataContext = loginAttempt;
        SellProductFooter.DataContext = sellProductData;
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
        BitmapImage bitmapNotify = new BitmapImage();
        bitmapNotify.BeginInit();

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

        bitmapNotify.UriSource = new Uri(Path.Combine(CurrentDir, "PersistentData", "Icons", "notifyIcon.jpg"), UriKind.Absolute);
        bitmapNotify.EndInit();
        NotifyImage.Source = bitmapNotify;
    }
    private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        string text = SearchBox.Text;
        if (text != "" && text != "Filtro")
        {
            ProductDataGrid.ItemsSource = this.ProductCrontroller.GetProducts().FindAll(product => product.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
        else
        {
            GetProducts();
        }
    }
    private void GetProducts()
    {
        ProductDataGridOff.ItemsSource = new List<Product>();
        ProductDataGrid.ItemsSource = this.ProductCrontroller.GetProducts();
    }

    private void EditProduct(object s, RoutedEventArgs e)
    {
        selectedProduct = (s as FrameworkElement).DataContext as Product;
        this.ProductCrontroller.EditProduct(selectedProduct.Id, NewProduct);
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
        if (this.ProductCrontroller.AddItem(NewProduct))
        {
            GetProducts();
            MessageBox.Show($"{NewProduct.Name} inserido com sucesso",
                "ok", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.Yes);
            NewProduct = new Product();
            AddItemGrid.DataContext = NewProduct;
        }
        else
        {
            MessageBox.Show($"Falha ao inserir produto, verifique os dados fornecidos!",
                "erro", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
        }
        
    }
    private void ProductDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
    private void AddUser(object s, RoutedEventArgs e)
    {
        RegisterWindow registerWindow = new RegisterWindow(this.UserController);
        registerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        registerWindow.Show();
    }
    private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        this.loginAttempt.Pwd = LoginPasswordBox.Password;
    }

    private void TryLogin(object sender, RoutedEventArgs e)
    {
        User? result = this.UserController.Logon(loginAttempt.Name, loginAttempt.Pwd);
        if (result != null)
        {
            this.isLogedIn = true;

            LoginVisibility(true);


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
        LoginPasswordBox.Password = String.Empty;
    }

    private void LoginVisibility(bool logged)
    {
        UserControlProductsIn.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlProductsOff.Visibility = !logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlLoggedIn.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlLoggedOff.Visibility = !logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlExportButton.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlInsertProduct.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlSellProduct.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;
        UserControlNotification.Visibility = logged ? Visibility.Visible : Visibility.Collapsed;

    }
    private void Logout(object sender, RoutedEventArgs e)
    {
        loginAttempt = new();
        LoggedUser = new();
        LoginPasswordBox.Password = String.Empty;
        LoginForm.DataContext = loginAttempt;
        MainWindowUserName.Text = LoggedUser.Name;
        this.isLogedIn = false;

        loadUserImage();

        LoginVisibility(false);
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
        editWindow.ShowDialog();
        GetProducts();
    }  
    private void ShowNotifications(object s, RoutedEventArgs e)
    {
        NotificationWindow notifications = new NotificationWindow(this.NotificationController, this.ProductCrontroller, this.LoggedUser);
        notifications.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        notifications.ShowDialog();
        GetProducts();
    }

    private void ExportExcel(object s, RoutedEventArgs e)
    {
        try
        {
            List<Product> dataList = this.ProductCrontroller.GetProducts();
            var workbook = new XLWorkbook();     //creates the workbook
            var wsDetailedData = workbook.AddWorksheet("Stock"); //creates the worksheet with sheetname
            wsDetailedData.Cell(1, 1).InsertTable(dataList); //inserts the data to cell A1 
            var path = Path.Combine(CurrentDir, "PersistentData", "data.xlsx");
            workbook.SaveAs(path); //saves the workbook
            string messageBoxText = "Dados Exportados com sucesso";
            string caption = "Sucesso";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        catch (Exception)
        {
            string messageBoxText = "Não foi possível salvar os dados";
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }


    }

    private void SellProduct(object sender, RoutedEventArgs e)
    {
        Product? resultProduct = this.ProductCrontroller.SellProduct(sellProductData.ProductId, sellProductData.SellQuantity);
        
        if (resultProduct != null)
        {
            MessageBox.Show($"{sellProductData.SellQuantity} unidades de {resultProduct.Name} vendidas com sucesso",
                "ok", MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.Yes);
            GetProducts();
        }
        else
        {
            MessageBox.Show($"Falha ao vender produto",
                "erro", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Yes);
        }
        sellProductData = new();
        SellProductFooter.DataContext = sellProductData;
    }

    private void ClearSell(object sender, RoutedEventArgs e)
    {
        sellProductData = new();
        SellProductFooter.DataContext = sellProductData;
    }

}

