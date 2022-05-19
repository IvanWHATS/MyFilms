using MyFilms_.NET_Framework_.Infrastructure;
using MyFilms_.NET_Framework_.Models;
using MyFilms_.NET_Framework_.Views;
using MyFilms_.NET_Framework_.Views.Pages.InfoPages;
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

        private readonly BitmapImage ExpandImage = new BitmapImage(new Uri("pack://application:,,,/Source/Icons/ExpandIcon.png"));
        private readonly BitmapImage CollapseImage = new BitmapImage(new Uri("pack://application:,,,/Source/Icons/CollapseIcon.png"));

        public delegate void LogInDelegate(bool isAuthorized);


        private double top;
        private double left;

        readonly BlurEffect blurEffect = new BlurEffect() { Radius = 10 };

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
                if (Authorization.getInstance().User.user_type_id == 1)
                {
                    AddFilm.Visibility = Visibility.Visible;
                    AddCrew.Visibility = Visibility.Visible;
                    AddProductionCompany.Visibility = Visibility.Visible;
                }
                if (MainFrame.Content == null)
                MainFrame.Navigate(new Search());
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


        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            auth.LogOut();
        }

        private void Top100Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainFrame.Content is Top100FIlms))
                MainFrame.Navigate(new Top100FIlms());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainFrame.Content is Search))
            MainFrame.Navigate(new Search());
        }

        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FilmInfoPage(new Film()));
        }

        private void AddCrew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductionCompany_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
