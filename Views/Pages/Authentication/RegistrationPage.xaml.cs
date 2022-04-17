using MyFilms_.NET_Framework_.Infrastructure;
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
        Authorization auth = Authorization.getInstance();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        #region KeyEnter
        private void LoginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) NicknameBox.Focus();
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
            if (e.Key == Key.Enter) Register();
        }


        #endregion


        private void Register()
        {
            if (string.IsNullOrWhiteSpace(LoginBox.Text)
                || string.IsNullOrWhiteSpace(PasswordBox1.Password)
                || string.IsNullOrWhiteSpace(PasswordBox2.Password)
                || string.IsNullOrWhiteSpace(NicknameBox.Text))
            {
                ErrorText.Text = "Поля не должны быть пустыми!";
                ErrorText.Visibility = Visibility.Visible;
            }
            else
                if (PasswordBox1.Password != PasswordBox2.Password)
            {
                ErrorText.Text = "Пороли не совпадают!";
                ErrorText.Visibility = Visibility.Visible;
            }
            else
                    if (!auth.Register(LoginBox.Text, PasswordBox1.Password, NicknameBox.Text))
            {
                ErrorText.Text = "Ошибка регистрации!";
                ErrorText.Visibility = Visibility.Visible;
            }

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Register();    
        }
    }
}
