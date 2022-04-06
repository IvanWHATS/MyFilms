using MyFilms_.NET_Framework_.Views.Pages.Authentication;
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
using System.Windows.Shapes;

namespace MyFilms_.NET_Framework_.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        /*public AuthenticationWindow()
        {
            InitializeComponent();
        }*/
         LogInPage LogInPage = new LogInPage();
         RegistrationPage RegistrationPage = new RegistrationPage();
        public AuthenticationWindow()
        {

            InitializeComponent();
            AuthenticationFrame.Navigate(LogInPage);
            LogInButton.Focus();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationFrame.Content is RegistrationPage) AuthenticationFrame.Navigate(LogInPage);

        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticationFrame.Content is LogInPage) AuthenticationFrame.Navigate(RegistrationPage);

        }
    }
}
