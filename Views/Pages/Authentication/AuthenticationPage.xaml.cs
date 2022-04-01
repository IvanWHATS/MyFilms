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
            if (Authentication.Content is RegistrationPage) 
            {
                // RegistrationButton.FontWeight = FontWeights.Regular;
                // LogInButton.FontWeight = FontWeights.Bold;
                //LogInButton.Foreground = (Brush)Application.Current.Resources["OrangeElementBrush"];
                //RegistrationButton.Foreground = (Brush)Application.Current.Resources["WhiteElementBrush"];
                Authentication.Navigate(LogInPage);
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Authentication.Content is LogInPage)
            {
                //RegistrationButton.FontWeight = FontWeights.Bold;
                // LogInButton.FontWeight = FontWeights.Regular;
                //RegistrationButton.Foreground = (Brush)Application.Current.Resources["OrangeElementBrush"];
                //LogInButton.Foreground = (Brush)Application.Current.Resources["WhiteElementBrush"];
                Authentication.Navigate(RegistrationPage);
            }
        }
    }
}
