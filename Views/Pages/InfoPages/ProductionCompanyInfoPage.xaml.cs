using MyFilms_.NET_Framework_.Models;
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

namespace MyFilms_.NET_Framework_.Views.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для ProductionCompaniesInfoPage.xaml
    /// </summary>
    public partial class ProductionCompanyInfoPage : Page
    {
        ProductionCompany ProductionCompany; 

        public ProductionCompanyInfoPage(ProductionCompany productionCompany)
        {
            ProductionCompany = productionCompany;
            InitializeComponent();
            ProductionCompanyName.Text = ProductionCompany.name;
            using (var db = new MyFilmsEntities())
                FilmsItemControl.ItemsSource = ProductionCompany.Films.Distinct()
                    .Join(db.FilmsAVGRatings,
                    f => f.film_id,
                    a => a.film_id,
                    (f, a) => new FilmsAVGRating
                    {
                        film_id = f.film_id,
                        title = f.title,
                        poster = f.poster,
                        avg_rating = a.avg_rating
                    }).ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFilm(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsAVGRating;
            using (var db = new MyFilmsEntities())
            {
                FilmInfoPage FilmPage = new FilmInfoPage(dataObject.film_id, dataObject.avg_rating);
                NavigationService.Navigate(FilmPage);
            }
        }
    }
}
