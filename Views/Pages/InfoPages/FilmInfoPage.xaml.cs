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
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для FilmInfoPage.xaml
    /// </summary>
    public partial class FilmInfoPage : Page
    {
        bool isEditing = false;

        private Film Film;
        FilmsRating rating;
        ToggleButton button1;
        int status;
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
            LoadFilmInfo();
        }

        private void LoadFilmInfo()
        {
            FilmTitle.Text = Film.title;
            FilmRuntime.Text = Film.runtime.ToString();
            FilmTagline.Text = Film.tagline;
            FilmDescription.Text = Film.description;


            FilmDate.Text = Film.release_date.ToShortDateString();
            FilmStatus.Text = Film.FilmStatus.name;
            FilmStatus.Text = Film.FilmStatus.name;
            FilmGenres.ItemsSource = Film.Genres.ToList();
            FilmRevenue.Text = Film.revenue.ToString();
            FilmLanguages.ItemsSource = Film.Languages;
            FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();
            UserFilmStatuses.ItemsSource = db.UserFilmStatuses.ToList();
            FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 2).Select(selected => new FilmsCrew 
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmActors.ItemsSource = Film.FilmsActors.Select(selected => new FilmsActor 
            {
                character = selected.character,
                Actor = selected.Actor 
            }).ToList();
            FilmScenarists.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 1).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmCameraMen.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 10).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmSoundEngineers.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 4).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmVisualEffects.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 7).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmLighting.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 11).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmCostumesMakeUp.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 6).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmArt.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 3).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmEditing.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 12).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmProduction.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 8).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();
            FilmCrew.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 5).Select(selected => new FilmsCrew
            {
                Crew = selected.Crew,
                job = selected.job
            }).ToList();

            if (Film.poster is null)
                FilmPoster.Source = new BitmapImage(new Uri("pack://application:,,,/Source/NoImage.png"));
            else
                FilmPoster.Source = ImageConverter.BytesToImage(Film.poster);

            rating = db.FilmsRatings.Find(Authorization.getInstance().User.login, Film.film_id);
            if (rating != null) Ratings.Text = rating.rating.ToString();
            
        }

        private void ChangeEditingMode()
        {
            if (!isEditing)
            {
                EditBtnText.Text = "Сохранить";
                FilmTitleEdit.Text = FilmTitle.Text;
                FilmTitle.Visibility = Visibility.Collapsed;
                FilmTitleEdit.Visibility = Visibility.Visible;
                FilmTaglineEdit.Text = FilmTagline.Text;
                FilmTagline.Visibility = Visibility.Collapsed;
                FilmTaglineEdit.Visibility = Visibility.Visible;
                FilmDescriptionEdit.Text = FilmDescription.Text;
                FilmDescription.Visibility = Visibility.Collapsed;
                FilmDescriptionEdit.Visibility = Visibility.Visible;
                FilmDateEdit.Text = FilmDate.Text;
                FilmDate.Visibility = Visibility.Collapsed;
                FilmDateEdit.Visibility = Visibility.Visible;
                FilmStatusEdit.Visibility = Visibility.Visible;
                FilmRuntimeEdit.Text = FilmRuntime.Text;
                FilmRuntime.Visibility = Visibility.Collapsed;
                FilmRuntimeEdit.Visibility = Visibility.Visible;
                FilmLanguagesEdit.Visibility = Visibility.Visible;
                FilmGenresEdit.Visibility = Visibility.Visible;
                FilmProductionCompaniesEdit.Visibility = Visibility.Visible;
                FilmRevenueEdit.Text = FilmRevenue.Text;
                FilmRevenue.Visibility = Visibility.Collapsed;
                FilmRevenueEdit.Visibility = Visibility.Visible;
                FilmDirectorsEdit.Visibility = Visibility.Visible;
                FilmActorsEdit.Visibility = Visibility.Visible;
                FilmScenaristsEdit.Visibility = Visibility.Visible;
                FilmCameraMenEdit.Visibility = Visibility.Visible;
                FilmSoundEngineersEdit.Visibility = Visibility.Visible;
                FilmVisualEffectsEdit.Visibility = Visibility.Visible;
                FilmLightingEdit.Visibility = Visibility.Visible;
                FilmCostumesMakeUpEdit.Visibility = Visibility.Visible;
                FilmArtEdit.Visibility = Visibility.Visible;
                FilmEditingEdit.Visibility = Visibility.Visible;
                FilmProductionEdit.Visibility = Visibility.Visible;
                FilmCrewEdit.Visibility = Visibility.Visible;
            }
            else
            {
                EditBtnText.Text = "Редактировать";
                FilmTitle.Text = FilmTitleEdit.Text;
                FilmTitle.Visibility = Visibility.Visible;
                FilmTitleEdit.Visibility = Visibility.Collapsed;
                FilmTagline.Text = FilmTaglineEdit.Text;
                FilmTagline.Visibility = Visibility.Visible;
                FilmTaglineEdit.Visibility = Visibility.Collapsed;
                FilmDescription.Text = FilmDescriptionEdit.Text;
                FilmDescription.Visibility = Visibility.Visible;
                FilmDescriptionEdit.Visibility = Visibility.Collapsed;
                FilmDate.Text = FilmDateEdit.Text;
                FilmDate.Visibility = Visibility.Visible;
                FilmDateEdit.Visibility = Visibility.Collapsed;
                FilmStatusEdit.Visibility = Visibility.Collapsed;
                FilmRuntime.Text = FilmRuntimeEdit.Text;
                FilmRuntime.Visibility = Visibility.Visible;
                FilmRuntimeEdit.Visibility = Visibility.Collapsed;
                FilmLanguagesEdit.Visibility = Visibility.Collapsed;
                FilmGenresEdit.Visibility = Visibility.Collapsed;
                FilmProductionCompaniesEdit.Visibility = Visibility.Collapsed;
                FilmRevenue.Text = FilmRevenueEdit.Text;
                FilmRevenue.Visibility = Visibility.Visible;
                FilmRevenueEdit.Visibility = Visibility.Collapsed;
                FilmDirectorsEdit.Visibility = Visibility.Collapsed;
                FilmActorsEdit.Visibility = Visibility.Collapsed;
                FilmScenaristsEdit.Visibility = Visibility.Collapsed;
                FilmCameraMenEdit.Visibility = Visibility.Collapsed;
                FilmSoundEngineersEdit.Visibility = Visibility.Collapsed;
                FilmVisualEffectsEdit.Visibility = Visibility.Collapsed;
                FilmLightingEdit.Visibility = Visibility.Collapsed;
                FilmCostumesMakeUpEdit.Visibility = Visibility.Collapsed;
                FilmArtEdit.Visibility = Visibility.Collapsed;
                FilmEditingEdit.Visibility = Visibility.Collapsed;
                FilmProductionEdit.Visibility = Visibility.Collapsed;
                FilmCrewEdit.Visibility = Visibility.Collapsed;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                ChangeEditingMode();
                isEditing = true;
                FilmStatusCombobox.ItemsSource = db.FilmStatuses
                                                .Where(w => w.status_id != Film.FilmStatus.status_id).ToList();

                FilmLanguagesCombobox.ItemsSource = db.Languages.ToList()
                                                .Where(w => !Film.Languages
                                                    .Any(a => w.language_code == a.language_code));

                FilmGenresCombobox.ItemsSource = db.Genres.ToList()
                                                .Where(w => !Film.Genres.ToList()
                                                    .Any(a => w.genre_id == a.genre_id));

                FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.Take(50).ToList()
                                                                .Where(w => !Film.ProductionCompanies.ToList()
                                                                    .Any(a => w.production_company_id == a.production_company_id));

                FilmDirectorsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 2).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmActorsCombobox.ItemsSource = db.Actors.Take(200).ToList()
                                                       .Where(w => !Film.FilmsActors.ToList()
                                                           .Any(a => w.actor_id == a.actor_id));

                FilmScenaristsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 1).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmCameraMenCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                       .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 10).Select(c => c.Crew).ToList()
                                                           .Any(a => w.crew_id == a.crew_id));

                FilmSoundEngineersCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 4).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmVisualEffectsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 7).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmLightingCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 11).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmCostumesMakeUpCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 6).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmArtCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 3).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmEditingCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 12).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmProductionCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 8).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));

                FilmCrewCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                        .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 5).Select(c => c.Crew).ToList()
                                                            .Any(a => w.crew_id == a.crew_id));
            }
            else
            {
                ChangeEditingMode();
                isEditing = false;
                

                db.Films.Find(Film.film_id).title = FilmTitle.Text;
                db.Films.Find(Film.film_id).tagline = FilmTagline.Text;
                db.Films.Find(Film.film_id).description = FilmDescription.Text;
                db.Films.Find(Film.film_id).release_date = Convert.ToDateTime(FilmDate.Text);
                db.Films.Find(Film.film_id).runtime = Convert.ToDecimal(FilmRuntime.Text);
                db.Films.Find(Film.film_id).revenue = Convert.ToDecimal(FilmRevenue.Text);

                db.SaveChanges();
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
        private void OpenCrew(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsCrew;
            using (var db = new MyFilmsEntities())
            {
                CrewInfoPage CrewPage = new CrewInfoPage(db.Crews.Find(dataObject.Crew.crew_id));
                NavigationService.Navigate(CrewPage);

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
        private void OpenProductionCompany(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as ProductionCompany;
            using (var db = new MyFilmsEntities())
            {
                ProductionCompanyInfoPage ProductionCompanyPage = new ProductionCompanyInfoPage(db.ProductionCompanies.Find(dataObject.production_company_id));
                NavigationService.Navigate(ProductionCompanyPage);

            }
        }

        private void FilmStatus_Checked(object sender, RoutedEventArgs e)
        {
            var btn = sender as ToggleButton;
            if (button1 != null) button1.IsChecked = false;
            button1 = btn;
            status = (btn.DataContext as UserFilmStatus).status_id;
        }


        private void Ratings_DropDownClosed(object sender, EventArgs e)
        {
            var sen = sender as ComboBox;
            if (sen.SelectedItem != null)
            {
                if (rating != null)
                {
                    using (var db = new MyFilmsEntities())
                    {
                        db.FilmsRatings.Find(Authorization.getInstance().User.login, Film.film_id).rating = Convert.ToDecimal(Ratings.Text);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var login = Authorization.getInstance().User.login;
                    var film_id = Film.film_id;
                    int? status_id = null;
                    /*if (button1 == null) status_id = null;
                    else status_id = (button1.DataContext as UserFilmStatus).status_id;*/
                    var rating = Convert.ToDecimal(sen.Text);
                    using (var db = new MyFilmsEntities())
                    {
                        db.FilmsRatings.Add(new FilmsRating
                        {
                            login = login,
                            film_id = film_id,
                            status_id = status_id,
                            rating = rating
                        });
                        db.SaveChanges();
                    }
                }
            }
        }

        #region AddButtons
        private void AddStatus(object sender, RoutedEventArgs e)
        {
            if (FilmStatusCombobox.SelectedItem != null)
            {

                Film.FilmStatus = FilmStatusCombobox.SelectedItem as FilmStatus;
                FilmStatus.Text = Film.FilmStatus.name;
                FilmStatusCombobox.ItemsSource = FilmStatusCombobox.ItemsSource = db.FilmStatuses.ToList()
                                                .Where(w => w.status_id != Film.FilmStatus.status_id);
            }
        }
        private void AddLanguage(object sender, RoutedEventArgs e)
        {
            if (FilmLanguagesCombobox.SelectedItem != null)
            {

                Film.Languages.Add(FilmLanguagesCombobox.SelectedItem as Language);
                FilmLanguages.ItemsSource = Film.Languages.ToList();
                FilmLanguagesCombobox.ItemsSource = db.Languages.ToList()
                                                .Where(w => !Film.Languages.ToList()
                                                    .Any(a => w.language_code == a.language_code));
            }
        }

        private void AddGenre(object sender, RoutedEventArgs e)
        {
            if (FilmGenresCombobox.SelectedItem != null)
            {

                Film.Genres.Add(FilmGenresCombobox.SelectedItem as Genre);
                FilmGenres.ItemsSource = Film.Genres.ToList();
                FilmGenresCombobox.ItemsSource = db.Genres.ToList()
                                                .Where(w => !Film.Genres.ToList()
                                                    .Any(a => w.genre_id == a.genre_id));
            }
        }

        private void AddProductionCompany(object sender, RoutedEventArgs e)
        {
            if (FilmProductionCompaniesCombobox.SelectedItem != null)
            {

                Film.ProductionCompanies.Add(FilmProductionCompaniesCombobox.SelectedItem as ProductionCompany);
                FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();
                FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.ToList()
                                                .Where(w => !Film.ProductionCompanies.ToList()
                                                    .Any(a => w.production_company_id == a.production_company_id));
            }
        }

        private void AddDirector(object sender, RoutedEventArgs e)
        {
            if (FilmDirectorsCombobox.SelectedItem != null)
            {

                Film.FilmsCrews.Add(new FilmsCrew
                {
                    Crew = FilmDirectorsCombobox.SelectedItem as Crew,
                    Film = Film,
                    department_id = 2
                });
                FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 2).Select(c => c.Crew).ToList();
                FilmDirectorsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                .Where(w => !Film.FilmsCrews.Where(d => d.department_id == 2).Select(c => c.Crew).ToList()
                                                    .Any(a => w.crew_id == a.crew_id));
            }
        }

        private void AddActor(object sender, RoutedEventArgs e)
        {
            if (FilmActorsCombobox.SelectedItem != null && !string.IsNullOrWhiteSpace(ActorCharacter.Text))
            {

                Film.FilmsActors.Add(new FilmsActor 
                { 
                    Actor = FilmActorsCombobox.SelectedItem as Actor,
                    character = ActorCharacter.Text,
                    Film = Film
                });
                FilmActors.ItemsSource = Film.FilmsActors.ToList();
                FilmActorsCombobox.ItemsSource = db.Actors.Take(200).ToList()
                                                .Where(w => !Film.FilmsActors.ToList()
                                                    .Any(a => w.actor_id == a.actor_id));
            }
        }

        private void AddScenarist(object sender, RoutedEventArgs e)
        {
            if (FilmScenaristsCombobox.SelectedItem != null)
            {

                Film.FilmsCrews.Add(new FilmsCrew
                {
                    Crew = FilmScenaristsCombobox.SelectedItem as Crew,
                    Film = Film,
                    department_id = 1
                });
                FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 1).Select(c => c.Crew).ToList();
                FilmScenaristsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                .Where(w => !Film.FilmsCrews.ToList()
                                                    .Any(a => w.crew_id == a.crew_id));
            }
        }

        private void AddCameraMen(object sender, RoutedEventArgs e)
        {
            if (FilmCameraMenCombobox.SelectedItem != null)
            {

                Film.FilmsCrews.Add(new FilmsCrew
                {
                    Crew = FilmCameraMenCombobox.SelectedItem as Crew,
                    Film = Film,
                    department_id = 10
                });
                FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 10).Select(c => c.Crew).ToList();
                FilmCameraMenCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                .Where(w => !Film.FilmsCrews.ToList()
                                                    .Any(a => w.crew_id == a.crew_id));
            }
        }

        private void AddSoundEngineer(object sender, RoutedEventArgs e)
        {
            if (FilmSoundEngineersCombobox.SelectedItem != null)
            {

                Film.FilmsCrews.Add(new FilmsCrew
                {
                    Crew = FilmSoundEngineersCombobox.SelectedItem as Crew,
                    Film = Film,
                    department_id = 4
                });
                FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 4).Select(c => c.Crew).ToList();
                FilmSoundEngineersCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                .Where(w => !Film.FilmsCrews.ToList()
                                                    .Any(a => w.crew_id == a.crew_id));
            }
        }

        private void AddVisualEffectsCrew(object sender, RoutedEventArgs e)
        {
            if (FilmVisualEffectsCombobox.SelectedItem != null)
            {

                Film.FilmsCrews.Add(new FilmsCrew
                {
                    Crew = FilmVisualEffectsCombobox.SelectedItem as Crew,
                    Film = Film,
                    department_id = 4
                });
                FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 7).Select(c => c.Crew).ToList();
                FilmVisualEffectsCombobox.ItemsSource = db.Crews.Take(200).ToList()
                                                .Where(w => !Film.FilmsCrews.ToList()
                                                    .Any(a => w.crew_id == a.crew_id));
            }
        }


        #endregion

    }
}
