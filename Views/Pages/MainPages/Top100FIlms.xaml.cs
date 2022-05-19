using MyFilms_.NET_Framework_.Models;
using MyFilms_.NET_Framework_.Views.Pages.InfoPages;
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

namespace MyFilms_.NET_Framework_.Views.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для Top100FIlms.xaml
    /// </summary>
    public partial class Top100FIlms : Page
    {
        public Top100FIlms()
        {
            InitializeComponent();
            using (var db = new MyFilmsEntities())
            {
                FilmsItemControl.ItemsSource = db.Top100Films.Take(50).ToList();
                /*FilmsItemControl.ItemsSource = db.FilmsAVGRatings.Select(selector => new
                {
                    film_id = selector.film_id,
                    title = selector.title,
                    avg_rating = selector.avg_rating,
                    poster = selector.poster == null ? new BitmapImage(new Uri("pack://application:,,,/Sourse/NoImage.png")) : ImageConverter.BytesToImage(selector.poster)
                }).ToList();*/
            }
        }

        private void OpenFilm(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as Top100Films;
            using (var db = new MyFilmsEntities())
            {
                FilmInfoPage FilmPage = new FilmInfoPage(db.Films.Find(dataObject.film_id), dataObject.avg_rating);
                NavigationService.Navigate(FilmPage);
            }


        }
    }
}
