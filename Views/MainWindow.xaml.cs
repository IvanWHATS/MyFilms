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
        
        public MainWindow()
        {
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 10;
            InitializeComponent();
            MainFrame.Effect = blurEffect;
            MainFrame.IsEnabled = false;
            //MyGrid.Children.Remove(LogInFrame);
        }
    }
}
