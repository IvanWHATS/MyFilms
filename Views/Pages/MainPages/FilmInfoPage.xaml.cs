using MyFilms_.NET_Framework_.Infrastructure;
using MyFilms_.NET_Framework_.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MyFilms_.NET_Framework_.Views.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для FilmInfoPage.xaml
    /// </summary>
    public partial class FilmInfoPage : Page
    {
        private FilmsAVGRating Film;
        private User user = Authorization.getInstance().User;
        public FilmInfoPage(FilmsAVGRating film)
        {
            Film = film;
            
            InitializeComponent();
            if (user.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            Title.Text = Film.title;
            Tagline.Text = Film.tagline;
            if (Film.poster != null)
            {
                var ms = new MemoryStream();
                ms.Write(Film.poster, 0, Film.poster.Length);
                var bim = new BitmapImage();
                bim.BeginInit();
                bim.StreamSource = ms;
                bim.EndInit();
                Poster.Source = bim;

            }
        }
    }
}
