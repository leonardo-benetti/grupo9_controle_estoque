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
    private string login;
    private string userLogon;
    private string passLogon;
    Product NewProduct = new();
    Product selectedProduct = new();
    public MainWindow(ApplicationDbContext context)
    {
        this.context = context;
        this.UserController = new UserController(context);
        this.ProductCrontroller = new ProductCrontroller(context);
        this.login = "";
        this.userLogon = "";
        this.passLogon = "";
        InitializeComponent();
        GetProducts();
        GetUsers();
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
    private void GetUsers()
    {
        //UserDataGrid.ItemsSource = this.UserController.GetUsers();
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
        NewProduct = fakeNewProduct();
        this.ProductCrontroller.AddItem(NewProduct);
        GetProducts();
        NewProduct = new Product();
    }
    private void ProductDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
    private void AddUser(object s, RoutedEventArgs e)
    {
        User newUser = fakeNewUser();
        this.UserController.CreateUser(newUser);
        GetUsers();
    }

    private void logon(object s, RoutedEventArgs e)
    {
        bool logon = this.UserController.Logon(this.userLogon, this.passLogon);
        if (logon)
        {
            this.login = this.userLogon;
        }
        this.userLogon = "";
        this.passLogon = "";
    }
}

