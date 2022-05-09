using Microsoft.Win32;
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
        MyFilmsEntities db = new MyFilmsEntities();
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
            FilmRuntime.Text = Film.runtime.ToString();
            FilmTagline.Text = Film.tagline;
            if (Film.description is null) Description.Visibility = Visibility.Collapsed;
            else FilmDescription.Text = Film.description;


            FilmDate.Text = Film.release_date.ToShortDateString();
            FilmGenres.ItemsSource = Film.Genres.ToList();
            FilmRevenue.Text = Film.revenue.ToString() + "$";
            FilmLanguages.ItemsSource = Film.Languages;
            FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();
            UserFilmStatuses.ItemsSource = db.UserFilmStatuses.Select(s => s.name).ToList();
            FilmDirectos.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 2).Select(c => c.Crew).ToList();
            FilmActors.ItemsSource = Film.FilmsActors.Select(selected => new FilmsActor 
            {
                character = selected.character,
                Actor = selected.Actor 
            }).ToList();
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

            if (Film.poster is null)
                FilmPoster.Source = new BitmapImage(new Uri("pack://application:,,,/Source/NoImage.png"));
            else
                FilmPoster.Source = ImageConverter.BytesToImage(Film.poster);
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
                FilmTitleEdit.Visibility = Visibility.Visible;
                FilmTagline.Visibility = Visibility.Collapsed;
                FilmTaglineEdit.Visibility = Visibility.Visible;

                FilmDateEdit.Text = FilmDate.Text;
                FilmDate.Visibility = Visibility.Collapsed;
                FilmDateEdit.Visibility = Visibility.Visible;
                FilmRuntimeEdit.Text = FilmRuntime.Text;
                FilmRuntime.Visibility = Visibility.Collapsed;
                FilmRuntimeEdit.Visibility = Visibility.Visible;
                FilmLanguagesEdit.Visibility = Visibility.Visible;
                FilmGenresEdit.Visibility = Visibility.Visible;
                FilmProductionCompaniesEdit.Visibility = Visibility.Visible;
                FilmRevenueEdit.Text = FilmRevenue.Text;
                FilmRevenue.Visibility = Visibility.Collapsed;
                FilmRevenueEdit.Visibility = Visibility.Visible;
                FilmDirectosEdit.Visibility = Visibility.Visible;
                FilmActorsEdit.Visibility = Visibility.Visible;
                FilmScenaristsEdit.Visibility = Visibility.Visible;
                FilmCameraMenEdit.Visibility = Visibility.Visible;
                FilmSoundEngineersEdit.Visibility = Visibility.Visible;
                FilmVisualEffectsEdit.Visibility = Visibility.Visible;
                FilmLightingEdit.Visibility = Visibility.Visible;
                FilmCostumesMakeUpEdit.Visibility = Visibility.Visible;
                FilmEditingEdit.Visibility = Visibility.Visible;
                FilmProductionEdit.Visibility = Visibility.Visible;
                FilmCrewEdit.Visibility = Visibility.Visible;
            }
            else
            {
                isEditing = false;
                EditBtnText.Text = "Редактировать";
                FilmTitle.Text = FilmTitleEdit.Text;
                db.Films.Find(Film.film_id).title = FilmTitle.Text;
                FilmTagline.Text = FilmTaglineEdit.Text;
                db.Films.Find(Film.film_id).tagline = FilmTagline.Text;
                FilmTitle.Visibility = Visibility.Visible;
                FilmTitleEdit.Visibility = Visibility.Collapsed;
                FilmTagline.Visibility = Visibility.Visible;
                FilmTaglineEdit.Visibility = Visibility.Collapsed;

                FilmDate.Text = FilmDateEdit.Text;
                db.Films.Find(Film.film_id).release_date = Convert.ToDateTime(FilmDate.Text);
                FilmDate.Visibility = Visibility.Visible;
                FilmDateEdit.Visibility = Visibility.Collapsed;
                FilmRuntime.Text = FilmRuntimeEdit.Text;
                db.Films.Find(Film.film_id).runtime = Convert.ToDecimal(FilmRuntime.Text);
                FilmRuntime.Visibility = Visibility.Visible;
                FilmRuntimeEdit.Visibility = Visibility.Collapsed;
                FilmLanguagesEdit.Visibility = Visibility.Collapsed;
                FilmGenresEdit.Visibility = Visibility.Collapsed;
                FilmProductionCompaniesEdit.Visibility = Visibility.Collapsed;
                FilmRevenue.Text = FilmRevenueEdit.Text;
                db.Films.Find(Film.film_id).revenue = Convert.ToDecimal(FilmRevenue.Text);
                FilmRevenue.Visibility = Visibility.Visible;
                FilmRevenueEdit.Visibility = Visibility.Collapsed;
                FilmDirectosEdit.Visibility = Visibility.Collapsed;
                FilmActorsEdit.Visibility = Visibility.Collapsed;
                FilmScenaristsEdit.Visibility = Visibility.Collapsed;
                FilmCameraMenEdit.Visibility = Visibility.Collapsed;
                FilmSoundEngineersEdit.Visibility = Visibility.Collapsed;
                FilmVisualEffectsEdit.Visibility = Visibility.Collapsed;
                FilmLightingEdit.Visibility = Visibility.Collapsed;
                FilmCostumesMakeUpEdit.Visibility = Visibility.Collapsed;
                FilmEditingEdit.Visibility = Visibility.Collapsed;
                FilmProductionEdit.Visibility = Visibility.Collapsed;
                FilmCrewEdit.Visibility = Visibility.Collapsed;


                db.SaveChanges();
            }
        }

        private void OpenCrew(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as Crew;
            using (var db = new MyFilmsEntities())
            {
                CrewInfoPage CrewPage = new CrewInfoPage(db.Crews.Find(dataObject.crew_id));
                NavigationService.Navigate(CrewPage);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                if (Authorization.getInstance().User.FilmsRatings.Where(f => f.film_id == Film.film_id) == null)
                using (var db = new MyFilmsEntities())
                    {
                    db.FilmsRatings.Add(new FilmsRating
                    {
                        login = Authorization.getInstance().User.login,
                        film_id = Film.film_id,
                        status_id = db.FilmStatuses.Where(w => w.name == UserFilmRating.Text).Select(i => i.status_id).FirstOrDefault(),
                        rating = Convert.ToDecimal(UserFilmRating.Text)
                    }); 
                    }
                
        }

        private void UploadPoster(object sender, MouseButtonEventArgs e)
        {
            if (isEditing)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Images |*.png;*.jpg";
                var result = fileDialog.ShowDialog();
                if (result == true)
                {
                    Film.poster = ImageConverter.GetImageBytes(fileDialog.FileName);
                    db.Films.Find(Film.film_id).poster = Film.poster;
                    FilmPoster.Source = ImageConverter.BytesToImage(Film.poster);
                }

            }
        }

        private void OpenActor(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsActor;
            using (var db = new MyFilmsEntities())
            {
                ActorInfoPage ActorPage = new ActorInfoPage(db.Actors.Find(dataObject.Actor.actor_id));
                NavigationService.Navigate(ActorPage);
            }
        }
    }
}
