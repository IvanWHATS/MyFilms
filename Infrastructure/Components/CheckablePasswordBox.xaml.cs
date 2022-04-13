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

namespace MyFilms_.NET_Framework_.Infrastructure.Components
{
    /// <summary>
    /// Логика взаимодействия для CheckablePasswordBox.xaml
    /// </summary>
    public partial class CheckablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;   
        private BitmapImage EyeImage = new BitmapImage(new Uri("pack://application:,,,/Sourse/Icons/EyeIcon.png"));
        private BitmapImage HiddenEyeImage = new BitmapImage(new Uri("pack://application:,,,/Sourse/Icons/HiddenEyeIcon.png"));

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CheckablePasswordBox), 
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordProperyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordProperyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CheckablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public CheckablePasswordBox()
        {
            InitializeComponent();
            CheckBoxIcon.Source = EyeImage;
        }


        private void PasswordBx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = PasswordBx.Password;
            PasswordText.Text = PasswordBx.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging) PasswordBx.Password = Password;
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordText.Text = PasswordBx.Password;
            PasswordBx.Visibility = Visibility.Collapsed;
            PasswordText.Visibility = Visibility.Visible;
            PasswordText.Focus();
            PasswordText.SelectionStart = PasswordText.Text.Length;
            CheckBoxIcon.Source = HiddenEyeImage;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBx.Password = PasswordText.Text;
            PasswordText.Visibility = Visibility.Collapsed;
            PasswordBx.Visibility = Visibility.Visible;
            PasswordBx.Focus();
            CheckBoxIcon.Source = EyeImage;
        }

        public void PasswordFocus()
        {
            if (PasswordBx.Visibility == Visibility.Collapsed)
            {
                PasswordText.Focus();
                PasswordText.SelectionStart = PasswordText.Text.Length;
            }
            else PasswordBx.Focus();
            PasswordBx.Focus();
        }
    }
}
