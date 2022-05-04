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
        bool isEditing = false;

        private Film Film;
        public FilmInfoPage(Film film, decimal? rating)
        {
            Film = film;
            
            InitializeComponent();
            if (Authorization.getInstance().User.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            FilmRating.Text = rating != null? rating.ToString() : "Нет оценок";
            LoadFilmInfo();
        }

        public FilmInfoPage(Film film)
        {
            Film = film;
            InitializeComponent();
            if (Authorization.getInstance().User.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            FilmRating.Text = Film.FilmsRatings.Select(s => s.rating).Average().ToString();
            LoadFilmInfo();
        }

        private void LoadFilmInfo()
        {
            FilmTitle.Text = Film.title;
            FilmTagline.Text = Film.tagline;
            if (Film.description is null) Description.Visibility = Visibility.Collapsed;
            else FilmDescription.Text = Film.description;
            FilmDate.Text = Film.release_date.ToShortDateString();
            FilmGenres.ItemsSource = Film.Genres.ToList();
            FilmRevenue.Text = Film.revenue.ToString() + "$";
            FilmLanguages.ItemsSource = Film.Languages;
            FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();

            FilmDirectos.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 2).Select(c => c.Crew).ToList();
            FilmActors.ItemsSource = Film.FilmsActors.Select(selected => new { selected.character, selected.Actor }).ToList();
            FilmScenarists.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 1).Select(c => c.Crew).ToList();
            FilmCameraMen.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 10).Select(c => c.Crew).ToList();
            FilmSoundEngineers.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 4).Select(c => c.Crew).ToList();
            FilmVisualEffects.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 7).Select(c => c.Crew).ToList();
            FilmLighting.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 11).Select(c => c.Crew).ToList();
            FilmCostumesMakeUp.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 6).Select(c => c.Crew).ToList();
            FilmArt.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 3).Select(c => c.Crew).ToList();
            FilmEditing.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 12).Select(c => c.Crew).ToList();
            FilmProduction.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 8).Select(c => c.Crew).ToList();
            FilmCrew.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 5).Select(c => c.Crew).ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true;
                EditBtnText.Text = "Сохранить";
                FilmTitleEdit.Text = FilmTitle.Text;
                FilmTaglineEdit.Text = FilmTagline.Text;
                FilmTitle.Visibility = Visibility.Collapsed;
                FilmTitle.IsEnabled = false;
                FilmTitleEdit.Visibility = Visibility.Visible;
                FilmTitleEdit.IsEnabled = true;
                FilmTagline.Visibility = Visibility.Collapsed;
                FilmTagline.IsEnabled = false;
                FilmTaglineEdit.Visibility = Visibility.Visible;
                FilmTaglineEdit.IsEnabled = true;
                
            }
            else
            {
                isEditing = false;
                EditBtnText.Text = "Редактировать";
                FilmTitle.Text = FilmTitleEdit.Text;
                FilmTagline.Text = FilmTaglineEdit.Text;
                FilmTitle.Visibility = Visibility.Visible;
                FilmTitle.IsEnabled = true;
                FilmTitleEdit.Visibility = Visibility.Collapsed;
                FilmTitleEdit.IsEnabled = false;
                FilmTagline.Visibility = Visibility.Visible;
                FilmTagline.IsEnabled = true;
                FilmTaglineEdit.Visibility = Visibility.Collapsed;
                FilmTaglineEdit.IsEnabled = false;
            }
        }

        private void OpenCrew(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as Crew;
            using (var db = new MyFilmsEntities())
            {
                CrewInfoPage FilmPage = new CrewInfoPage(db.Crews.Find(dataObject.crew_id));
                NavigationService.Navigate(FilmPage);
            }
        }
    }
}
