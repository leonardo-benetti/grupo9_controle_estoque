using grupo9_controle_estoque.Controller;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace grupo9_controle_estoque;

/// <summary>
/// Interaction logic for RegisterWindow.xaml
/// </summary>
public partial class RegisterWindow : Window
{

    private string profilePicturesDir = Path.Combine(Path.GetFullPath(@"..\..\..\"), "PersistentData", "ProfilePictures");

    private string imagePath = string.Empty;

    private class InputData
    {
        public string Name { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
    }

    InputData UserInput = new();

    private UserController userController;

    public RegisterWindow(UserController userController)
    {
        this.userController = userController;
        InitializeComponent();
        imagePath = Path.Combine(profilePicturesDir, "unknown_user.jpg");
        loadImage();
        AddUserGrid.DataContext = UserInput;
    }

    private void loadImage()
    {
        BitmapImage bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
        bitmap.EndInit();
        RegisterWindowProfilePic.Source = bitmap;
    }

    private void ImgClicked(object s, RoutedEventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.InitialDirectory = "c:\\";
        dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
        dlg.RestoreDirectory = true;

        if (dlg.ShowDialog() == true)
        {
            imagePath = Path.Combine(profilePicturesDir, Path.GetFileName(dlg.FileName));
            File.Copy(dlg.FileName, imagePath, true);
            loadImage();
        }
    }

    private void CursorToHand(object s, RoutedEventArgs e)
    {
        RegisterWindowProfilePic.Cursor = Cursors.Hand;
    }

    private void RegisterUser(object s, RoutedEventArgs e)
    {
        bool success = userController.CreateUser(UserInput.Name, UserInput.Pwd, imagePath);

        if (success)
        {
            MessageBox.Show("Cadastro efetuado com sucesso!", "sucesso", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
            
            // reset window
            imagePath = Path.Combine(profilePicturesDir, "unknown_user.jpg");
            loadImage();
            UserInput = new();
            AddUserGrid.DataContext = UserInput;
            this.Close();
        }
        else
        {
            MessageBox.Show("Falha no cadastro", "falha", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
        }
    }

}
