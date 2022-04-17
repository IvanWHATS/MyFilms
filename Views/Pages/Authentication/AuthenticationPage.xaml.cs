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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyFilms_.NET_Framework_.Views.Pages.Authentication
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        LogInPage LogInPage = new LogInPage();
        RegistrationPage RegistrationPage = new RegistrationPage();
        public AuthenticationPage()
        {
            InitializeComponent();
            Authentication.Navigate(LogInPage);
            LogInButton.Focus();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LogInButton.IsEnabled = false;
            RegistrationButton.IsEnabled = true;
            Authentication.Navigate(LogInPage);

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationButton.IsEnabled = false;
            LogInButton.IsEnabled = true;
            Authentication.Navigate(RegistrationPage);

        }
    }
}
