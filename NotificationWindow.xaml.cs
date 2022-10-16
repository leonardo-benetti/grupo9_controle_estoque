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
    private User LoggedUser;
    private Notification newNotification = new();

    public NotificationWindow(NotificationController notificationController, ProductController productController, User LoggedUser)
    {
        InitializeComponent();
        this.notificationController = notificationController;
        this.productController = productController;
        this.LoggedUser = LoggedUser;
        AddNotificationFooter.DataContext = newNotification;

        this.GetTriggeredNotifications();
        this.GetAllNotifications();
    }

    private Product? FindProduct(int ID)
    {
        return this.productController.GetProducts().Find(product => product.Id == ID);
    }

    private void GetTriggeredNotifications()
    {
        var triggeredNotifications = this.notificationController.GetNotifications().FindAll(notification => FindProduct(notification.ProductID)?.Quantity <= notification.MinQuantity);
        NotificationGridTriggeredAlerts.ItemsSource = triggeredNotifications;
    }

    private void GetAllNotifications()
    {
        NotificationGridAllAlerts.ItemsSource = this.notificationController.GetNotifications();
    }

    private void SelectionBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (UserControlTriggeredAlerts != null && UserControlAllAlerts != null)
        {
            string? text = (e.AddedItems[0] as ComboBoxItem).Content as string;
            ChangeVisibility(text);
        }
    }

    private void ChangeVisibility(string? comboBoxText)
    {
        if (comboBoxText == "Alertas")
        {
            UserControlTriggeredAlerts.Visibility = Visibility.Visible;
            UserControlAllAlerts.Visibility = Visibility.Collapsed;
            UserControlButtonsAlerts.Visibility = Visibility.Visible;
            UserControlButtonsAllNotifications.Visibility = Visibility.Collapsed;
        }
        if (comboBoxText == "Notificações criadas")
        {
            UserControlTriggeredAlerts.Visibility = Visibility.Collapsed;
            UserControlAllAlerts.Visibility = Visibility.Visible;
            UserControlButtonsAlerts.Visibility = Visibility.Collapsed;
            UserControlButtonsAllNotifications.Visibility = Visibility.Visible;
        }
    }

    private void CreateAlert(object s, RoutedEventArgs e)
    {
        this.newNotification.UserID = this.LoggedUser.Id;
        this.notificationController.AddNotification(this.newNotification);
        this.newNotification = new();
        this.GetAllNotifications();
        this.GetTriggeredNotifications();
    }

    private void DeleteNotification(object s, RoutedEventArgs e)
    {
        var notificationToDelete = (s as FrameworkElement).DataContext as Notification;
        this.notificationController.DeleteNotification(notificationToDelete);
        this.GetAllNotifications();
        this.GetTriggeredNotifications();
    }

    private void Save(object s, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Close(object s, RoutedEventArgs e)
    {
        this.Close();
    }
}
