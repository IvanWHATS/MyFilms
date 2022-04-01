using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyFilms_.NET_Framework_.Views.Pages.Authentication
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private BitmapImage EyeImage = new BitmapImage();
        private BitmapImage HiddenEyeImage = new BitmapImage();
        public LogInPage()
        {
            EyeImage.BeginInit();
            EyeImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            EyeImage.CacheOption = BitmapCacheOption.OnLoad;
            EyeImage.UriSource = new Uri(@"C:\Users\Ivan\Desktop\Курсач по БД\MyFilmsWPF\MyFilms(.NET Framework)\Sourse\Icons\EyeIcon.png", UriKind.Relative);
            EyeImage.EndInit();
            HiddenEyeImage.BeginInit();
            HiddenEyeImage.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            HiddenEyeImage.CacheOption = BitmapCacheOption.OnLoad;
            HiddenEyeImage.UriSource = new Uri(@"C:\Users\Ivan\Desktop\Курсач по БД\MyFilmsWPF\MyFilms(.NET Framework)\Sourse\Icons\HiddenEyeIcon.png", UriKind.Relative);
            HiddenEyeImage.EndInit();
            InitializeComponent();
        }

        private void LogIn() 
        {

        }

        #region 
        private void LoginBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (PasswordBox.Visibility == Visibility.Collapsed)
                {
                    PasswordText.Focus();
                    PasswordText.SelectionStart = PasswordText.Text.Length;
                }
                else PasswordBox.Focus();
                PasswordBox.Focus();
            }
        }

        private void PasswordBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                LogIn();
            }
        }

        #endregion

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
            
        }

        #region ShowPassword
        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordText.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordText.Visibility = Visibility.Visible;
            PasswordText.Focus();
            PasswordText.SelectionStart = PasswordText.Text.Length;
            ShowPasswordIcon.Source = EyeImage;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordText.Text;
            PasswordText.Visibility = Visibility.Collapsed;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Focus();
            ShowPasswordIcon.Source = HiddenEyeImage;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordText.Text = PasswordBox.Password;
        }
        #endregion
    }
}
