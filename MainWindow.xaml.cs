using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Linq;
using System.Windows;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly ApplicationDbContext context;
    private readonly UserController UserController;
    private readonly ProductCrontroller ProductCrontroller;

    private bool isLogedIn = false;

    private class LoginAttempt
    {
        public string Name { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
    }

    private LoginAttempt loginAttempt = new();

    Product NewProduct = new();
    Product selectedProduct = new();
    public MainWindow(ApplicationDbContext context)
    {
        this.context = context;
        this.UserController = new UserController(context);
        this.ProductCrontroller = new ProductCrontroller(context);
        InitializeComponent();
        GetProducts();
        AddItemGrid.DataContext = NewProduct;
        LoginForm.DataContext = loginAttempt;
        MainWindowUserName.Text = loginAttempt.Name;
    }
    private Product fakeNewProduct()
    {
        return new Product()
        {
            Name = "TESTE",
            Description = "123",
            Price = 1000,
            Quantity = 10
        };

    }
    private User fakeNewUser()
    {
        return new User("TESTE_USER", "123");
    }
    private void GetProducts()
    {
        ProductDataGrid.ItemsSource = this.ProductCrontroller.GetProducts();
    }

    private void EditProduct(object s, RoutedEventArgs e)
    {
        NewProduct = fakeNewProduct();
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
}

