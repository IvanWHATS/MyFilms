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
using MyFilms_.NET_Framework_.Infrastructure;

namespace MyFilms_.NET_Framework_.Views.Pages.Authentication
{
    /// <summary>
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {

        public LogInPage()
        {
            
            InitializeComponent();
        }

        #region KeyEnter
        private void LoginBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) PasswordBox.PasswordFocus();
            
        }

        private void PasswordBox_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LogIn();
        }


        #endregion
        private void LogIn()
        {
            var auth = Authorization.getInstance();
            auth.LogIn(LoginBox.Text, PasswordBox.Password);
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
        }
    }
}
