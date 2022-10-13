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
    private List<Notification> notificationList;
    public NotificationWindow(NotificationController notificationController)
    {
        InitializeComponent();
        this.notificationController = notificationController;
        NotificationGridTriggeredAlerts.ItemsSource = this.notificationController.GetNotifications();
        NotificationGridAllAlerts.ItemsSource = new List<Notification>();

    }

    private void SelectionBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if(UserControlTriggeredAlerts != null && UserControlAllAlerts != null)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;
            MessageBox.Show(text);

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
