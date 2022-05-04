using MyFilms_.NET_Framework_.Infrastructure;
using MyFilms_.NET_Framework_.Views;
using MyFilms_.NET_Framework_.Views.Pages.MainPages;
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

namespace MyFilms_.NET_Framework_.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsWindowFullScreen => Width == SystemParameters.WorkArea.Width && Height == SystemParameters.WorkArea.Height;

        private BitmapImage ExpandImage = new BitmapImage(new Uri("pack://application:,,,/Sourse/Icons/ExpandIcon.png"));
        private BitmapImage CollapseImage = new BitmapImage(new Uri("pack://application:,,,/Sourse/Icons/CollapseIcon.png"));

        public delegate void LogInDelegate(bool isAuthorized);

        private double top;
        private double left;

        BlurEffect blurEffect = new BlurEffect() { Radius = 10 };

        Authorization auth = Authorization.getInstance();
        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            top = Top;
            left = Left;
            WorkGrid.Effect = blurEffect;
            auth.AuthorizationChanged += LogIn;
            auth.AuthorizationChanged += LogOut;
        }

        private void LogOut(bool obj)
        {
            if (!obj)
            {
                Authentication.IsEnabled = true;
                Authentication.Visibility = Visibility.Visible;
                WorkGrid.IsEnabled = false;
                WorkGrid.Effect = blurEffect;
            }
        }

        private void LogIn(bool obj)
        {
            if (obj)
            {
                Authentication.IsEnabled = false;
                Authentication.Visibility = Visibility.Collapsed;
                WorkGrid.IsEnabled = true;
                WorkGrid.Effect = null;
                Search search = new Search();
                MainFrame.Navigate(search);
            }
        }


        #region WindowControls
        private void Drag(object sender, MouseButtonEventArgs e)
        {
            if (!IsWindowFullScreen) DragMove();

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Resize(object sender, RoutedEventArgs e)
        {
            if (!IsWindowFullScreen)
            {
                Width = SystemParameters.WorkArea.Width;
                Height = SystemParameters.WorkArea.Height;
                WindowState = WindowState.Normal;
                Top = 0;
                Left = 0;
                ResizeIcon.Source = CollapseImage;
            }
            else
            {
                
                Height = MinHeight;
                Width = MinWidth;
                Top = top;
                Left = left;
                ResizeIcon.Source = ExpandImage;
            }
        }

        private void StopDrag(object sender, MouseEventArgs e)
        {
            top = Top;
            left = Left;
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            auth.LogOut();
        }
    }
}
