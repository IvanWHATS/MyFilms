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
        public FilmInfoPage(int film_id, decimal? rating)
        {
            Film = db.Films.Find(film_id);
            
            InitializeComponent();
            if (Authorization.getInstance().User.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            FilmRating.Text = rating != null? rating.ToString() : "Нет оценок";
            LoadFilmInfo();
        }

        public FilmInfoPage()
        {
            Film = db.Films.Add(new Film());
            InitializeComponent();
            if (Authorization.getInstance().User.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            ChangeEditingMode();
            isEditing = true;
            FilmStatusCombobox.ItemsSource = db.FilmStatuses.ToList();

            FilmLanguagesCombobox.ItemsSource = db.Languages.ToList();

            FilmGenresCombobox.ItemsSource = db.Genres.ToList();

            FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.Take(100).ToList();

            FilmDirectorsCombobox.ItemsSource = GetCrews(200, 2);

            FilmActorsCombobox.ItemsSource = db.Actors.Take(200).ToList();

            FilmScenaristsCombobox.ItemsSource = GetCrews(200, 1);

            FilmCameraMenCombobox.ItemsSource = GetCrews(200, 10);

            FilmSoundEngineersCombobox.ItemsSource = GetCrews(200, 4);

            FilmVisualEffectsCombobox.ItemsSource = GetCrews(200, 7);

            FilmLightingCombobox.ItemsSource = GetCrews(200, 11);

            FilmCostumesMakeUpCombobox.ItemsSource = GetCrews(200, 6);

            FilmArtCombobox.ItemsSource = GetCrews(200, 3);

            FilmEditingCombobox.ItemsSource = GetCrews(200, 12);

            FilmProductionCombobox.ItemsSource = GetCrews(200, 8);

            FilmCrewCombobox.ItemsSource = GetCrews(200, 5);
            //LoadFilmInfo();
        }

        private void LoadFilmInfo()// Загружает информацию о фильме в соответствующие поля
        {
            FilmTitle.Text = Film.title;
            FilmRuntime.Text = Film.runtime.ToString();
            FilmTagline.Text = Film.tagline;
            FilmDescription.Text = Film.description;


            FilmDate.Text = Film.release_date.ToShortDateString();
            FilmStatus.Text = Film.FilmStatus.name;
            FilmGenres.ItemsSource = Film.Genres.ToList();
            FilmRevenue.Text = Film.revenue.ToString();
            FilmLanguages.ItemsSource = Film.Languages;
            FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();
            UserFilmStatuses.ItemsSource = db.UserFilmStatuses.ToList();
            FilmDirectors.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 2).ToList();
            FilmActors.ItemsSource = Film.FilmsActors.ToList();
            FilmScenarists.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 1).ToList();
            FilmCameraMen.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 10).ToList();
            FilmSoundEngineers.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 4).ToList();
            FilmVisualEffects.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 7).ToList();
            FilmLighting.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 11).ToList();
            FilmCostumesMakeUp.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 6).ToList();
            FilmArt.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 3).ToList();
            FilmEditing.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 12).ToList();
            FilmProduction.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 8).ToList();
            FilmCrew.ItemsSource = Film.FilmsCrews.Where(d => d.department_id == 5).ToList();

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

                FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.Take(100).ToList()
                                                                .Where(w => !Film.ProductionCompanies.ToList()
                                                                    .Any(a => w.production_company_id == a.production_company_id));

                FilmDirectorsCombobox.ItemsSource = GetCrews(200, 2);

                FilmActorsCombobox.ItemsSource = db.Actors.Take(200).ToList()
                                                       .Where(w => !Film.FilmsActors.ToList()
                                                           .Any(a => w.actor_id == a.actor_id));

                FilmScenaristsCombobox.ItemsSource = GetCrews(200, 1);

                FilmCameraMenCombobox.ItemsSource = GetCrews(200, 10);

                FilmSoundEngineersCombobox.ItemsSource = GetCrews(200, 4);

                FilmVisualEffectsCombobox.ItemsSource = GetCrews(200, 7);

                FilmLightingCombobox.ItemsSource = GetCrews(200, 11);

                FilmCostumesMakeUpCombobox.ItemsSource = GetCrews(200, 6);

                FilmArtCombobox.ItemsSource = GetCrews(200, 3);

                FilmEditingCombobox.ItemsSource = GetCrews(200, 12);

                FilmProductionCombobox.ItemsSource = GetCrews(200, 8);

                FilmCrewCombobox.ItemsSource = GetCrews(200, 5);
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
                db.Films.Find(Film.film_id).FilmStatus = Film.FilmStatus;
                db.Films.Find(Film.film_id).Languages = Film.Languages;
                db.Films.Find(Film.film_id).Genres = Film.Genres;
                db.Films.Find(Film.film_id).ProductionCompanies = Film.ProductionCompanies;
                db.Films.Find(Film.film_id).FilmsActors = Film.FilmsActors;
                db.Films.Find(Film.film_id).FilmsCrews = Film.FilmsCrews;

                db.SaveChanges();
            }
        }

        private List<Crew> GetCrews(int top, int department_id)
        {
            return db.Crews.Take(top).ToList().Where(w => !Film.FilmsCrews.Where(d => d.department_id == department_id).Select(c => c.Crew).ToList().Any(a => w.crew_id == a.crew_id)).ToList();
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

        private void LanguageClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as Language;
            if (!isEditing)
            {

            }
            else
            {
                Film.Languages.Remove(dataObject);
                FilmLanguages.ItemsSource = Film.Languages.ToList();
                FilmLanguagesCombobox.ItemsSource = db.Languages.ToList()
                                                .Where(w => !Film.Languages.ToList()
                                                    .Any(a => w.language_code == a.language_code));
            }

        }

        private void GenreClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as Genre;
            if (!isEditing)
            {

            }
            else
            {
                Film.Genres.Remove(dataObject);
                FilmGenres.ItemsSource = Film.Genres.ToList();
                FilmGenresCombobox.ItemsSource = db.Genres.ToList()
                                               .Where(w => !Film.Genres.ToList()
                                                   .Any(a => w.genre_id == a.genre_id));
            }

        }

        private void CrewClick(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsCrew;
            if (!isEditing)
            {
                using (var db = new MyFilmsEntities())
                {
                    CrewInfoPage CrewPage = new CrewInfoPage(db.Crews.Find(dataObject.Crew.crew_id));
                    NavigationService.Navigate(CrewPage);

                }
            }
            else
            {
                int department_id = Convert.ToInt32(btn.Tag);
                switch (department_id)
                {
                    case 1: { DeleteCrew(ref FilmScenarists,dataObject, department_id); break; }
                    case 2: { DeleteCrew(ref FilmDirectors, dataObject, department_id); break; }
                    case 3: { DeleteCrew(ref FilmArt, dataObject, department_id); break; }
                    case 4: { DeleteCrew(ref FilmSoundEngineers, dataObject, department_id); break; }
                    case 5: { DeleteCrew(ref FilmCrew, dataObject,  department_id); break; }
                    case 6: { DeleteCrew(ref FilmCostumesMakeUp,dataObject, department_id); break; }
                    case 7: { DeleteCrew(ref FilmVisualEffects, dataObject, department_id); break; }
                    case 8: { DeleteCrew(ref FilmProduction, dataObject, department_id); break; }
                    case 10: { DeleteCrew(ref FilmCameraMen, dataObject, department_id); break; }
                    case 11: { DeleteCrew(ref FilmLighting, dataObject, department_id); break; }
                    case 12: { DeleteCrew(ref FilmEditing, dataObject, department_id); break; }
                }
            }
            
        }

        private void ActorClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as FilmsActor;
            if(!isEditing)
            {
                using (var db = new MyFilmsEntities())
                {

                    ActorInfoPage ActorPage = new ActorInfoPage(db.Actors.Find(dataObject.Actor.actor_id));
                    NavigationService.Navigate(ActorPage);
                }
            }
            else
            {
                Film.FilmsActors.Remove(dataObject);
                FilmActors.ItemsSource = Film.FilmsActors.ToList();
            }
        }

        private void ProductionCompanyClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var dataObject = btn.DataContext as ProductionCompany;
            if (!isEditing) 
            {
                using (var db = new MyFilmsEntities())
                {
                    ProductionCompanyInfoPage ProductionCompanyPage = new ProductionCompanyInfoPage(db.ProductionCompanies.Find(dataObject.production_company_id));
                    NavigationService.Navigate(ProductionCompanyPage);

                }
            }
            else 
            {
                Film.ProductionCompanies.Remove(dataObject);
                FilmProductionCompanies.ItemsSource = Film.ProductionCompanies.ToList();
                FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.Take(100).ToList()
                                                                .Where(w => !Film.ProductionCompanies.ToList()
                                                                    .Any(a => w.production_company_id == a.production_company_id));
            }

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


        private void DeleteCrew(ref ItemsControl itemsControl, FilmsCrew filmsCrew, int department_id)
        {
            Film.FilmsCrews.Remove(filmsCrew);
            itemsControl.ItemsSource = Film.FilmsCrews
                    .Where(d => d.department_id == department_id).ToList();
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
                FilmProductionCompaniesCombobox.ItemsSource = db.ProductionCompanies.Take(100).ToList()
                                                .Where(w => !Film.ProductionCompanies.ToList()
                                                    .Any(a => w.production_company_id == a.production_company_id));
            }
        }

        private void AddCrew(object sender, RoutedEventArgs e)
        {
           
            
                var btn = sender as Button;
                int department_id = Convert.ToInt32(btn.Tag);
                
                switch (department_id)
                {
                    case 1: { AddCrew(ref FilmScenaristsCombobox, ref ScenaristJob, ref FilmScenarists, department_id); break; }
                    case 2: { AddCrew(ref FilmDirectorsCombobox, ref DirectorJob, ref FilmDirectors, department_id); break; }
                    case 3: { AddCrew(ref FilmArtCombobox, ref FilmArtCrewJob, ref FilmArt, department_id); break; }
                    case 4: { AddCrew(ref FilmSoundEngineersCombobox, ref SoundEngineerJob, ref FilmSoundEngineers, department_id); break; }
                    case 5: { AddCrew(ref FilmCrewCombobox, ref CrewJob, ref FilmCrew, department_id); break; }
                    case 6: { AddCrew(ref FilmCostumesMakeUpCombobox, ref CostumesMakeUpCrewJob, ref FilmCostumesMakeUp, department_id); break; }
                    case 7: { AddCrew(ref FilmVisualEffectsCombobox, ref VisualEffectsCrewJob, ref FilmVisualEffects, department_id); break; }
                    case 8: { AddCrew(ref FilmProductionCombobox, ref ProductionCrewJob, ref FilmProduction, department_id); break; }
                    case 10: { AddCrew(ref FilmCameraMenCombobox, ref CameraManJob, ref FilmCameraMen, department_id); break; }
                    case 11: { AddCrew(ref FilmLightingCombobox, ref LightingCrewJob, ref FilmLighting, department_id); break; }
                    case 12: { AddCrew(ref FilmEditingCombobox, ref EditingCrewJob, ref FilmEditing, department_id); break; }
                }
            
        }

        private void AddCrew(ref ComboBox comboBox, ref TextBox textBox, ref ItemsControl itemsControl, int department_id)
        {
            if (comboBox.SelectedItem != null && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                var crew = new FilmsCrew
                {
                Crew = comboBox.SelectedItem as Crew,
                Film = Film,
                Department = db.Departments.Find(department_id),
                crew_id = (comboBox.SelectedItem as Crew).crew_id,
                department_id = department_id,
                job = textBox.Text
                };
                if (Film.FilmsCrews.Where(w => w.crew_id == crew.crew_id && w.job == crew.job).ToList().Count == 0)
                    Film.FilmsCrews.Add(crew);
                itemsControl.ItemsSource = Film.FilmsCrews
                    .Where(d => d.department_id == department_id).ToList();

            }

        }

        private void AddActor(object sender, RoutedEventArgs e)
        {
            if (FilmActorsCombobox.SelectedItem != null && !string.IsNullOrWhiteSpace(ActorCharacter.Text))
            {

                var actor = new FilmsActor 
                { 
                    Actor = FilmActorsCombobox.SelectedItem as Actor,
                    character = ActorCharacter.Text,
                    Film = Film
                };
                if (Film.FilmsActors.Where(w => w.actor_id == actor.actor_id && w.character == actor.character).ToList().Count == 0)
                    Film.FilmsActors.Add(actor);
                FilmActors.ItemsSource = Film.FilmsActors.ToList();
                FilmActorsCombobox.ItemsSource = db.Actors.Take(200).ToList()
                                                .Where(w => !Film.FilmsActors.ToList()
                                                    .Any(a => w.actor_id == a.actor_id));
            }
        }


        #endregion
        private void FilmStatus_Checked(object sender, RoutedEventArgs e)
        {
            var btn = sender as ToggleButton;
            if (button1 != null) button1.IsChecked = false;
            button1 = btn;
            status = (btn.DataContext as UserFilmStatus).status_id;
        }

    }
}
