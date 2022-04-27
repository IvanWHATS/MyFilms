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

namespace MyFilms_.NET_Framework_.Views.Pages.MainPages
{
    /// <summary>
    /// Логика взаимодействия для Seach.xaml
    /// </summary>
    public partial class Search : Page
    {

        private Frame ParantFrame;
        private List<FilmsAVGRating> FilmsCollection;

        public Search(Frame parantFrame)
        {
            ParantFrame = parantFrame;
            using (var db = new MyFilmsEntities())
            {
                FilmsCollection = db.FilmsAVGRatings.OrderBy(p => p.film_id).Take(10).ToList();
                
            }
            
            InitializeComponent();
            if (FilmsCollection != null)
                FilmsItemControl.ItemsSource = FilmsCollection;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyFilmsEntities())
            {
                FilmsCollection = db.FilmsAVGRatings.Where(t => t.title.Contains(SearchBox.Text)).ToList();
            }
            if (FilmsCollection != null)
                FilmsItemControl.ItemsSource = FilmsCollection;
        }

        private void OpenFilm(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsAVGRating;
            FilmInfoPage FilmPage = new FilmInfoPage(dataObject);
            ParantFrame.Navigate(FilmPage);
        }
    }
}
