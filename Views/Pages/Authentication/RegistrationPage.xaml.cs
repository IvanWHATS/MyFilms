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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        #region KeyEnter
        private void LoginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LoginBox1.Focus();
        }
        private void LoginBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) PasswordBox1.PasswordFocus();
        }

        private void PasswordBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) PasswordBox2.PasswordFocus();
        }

        private void PasswordBox2_KeyDown(object sender, KeyEventArgs e)
        {
            Register();
        }

        #endregion

        private void Register()
        {
            
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Register();
        }
    }
}
