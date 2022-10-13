using System.Windows;
using System.Collections.Generic;
using grupo9_controle_estoque.Model;
using grupo9_controle_estoque.Controller;
using System.Windows.Controls;

namespace grupo9_controle_estoque;
/// <summary>
/// Interaction logic for NotificationWindow.xaml
/// </summary>
public partial class NotificationWindow : Window
{
    private NotificationController notificationController;
    private ProductController productController;
    private List<Notification> notificationTriggeredList;
    public NotificationWindow(NotificationController notificationController, ProductController productController)
    {
        InitializeComponent();
        this.notificationController = notificationController;
        this.productController = productController;

        this.notificationTriggeredList = this.GetTriggeredNotifications();

        NotificationGridTriggeredAlerts.ItemsSource = this.notificationTriggeredList;
        NotificationGridAllAlerts.ItemsSource = this.notificationController.GetNotifications();
    }
    private Product? FindProduct(string ID)
    {
        return this.productController.GetProducts().Find(product => product.GUID == ID);
    }
    private List<Notification> GetTriggeredNotifications()
    {
        var triggeredNotifications = this.notificationController.GetNotifications().FindAll(notification => FindProduct(notification.ProductID)?.Quantity <= notification.MinQuantity);
        return triggeredNotifications;
    }
    private void SelectionBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if(UserControlTriggeredAlerts != null && UserControlAllAlerts != null)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;

            if(text == "Notificações alertas")
            {
                UserControlTriggeredAlerts.Visibility = Visibility.Visible;
                UserControlAllAlerts.Visibility = Visibility.Collapsed;
            }
            if(text == "Todas Notificações")
            {
                UserControlTriggeredAlerts.Visibility = Visibility.Collapsed;
                UserControlAllAlerts.Visibility = Visibility.Visible;
            }
        }
    }

    private void Save(object s, RoutedEventArgs e)
    {
        this.Close();
    }
    private void Cancel(object s, RoutedEventArgs e)
    {
        this.Close();
    }
}
