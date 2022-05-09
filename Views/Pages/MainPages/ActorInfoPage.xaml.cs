using MyFilms_.NET_Framework_.Infrastructure;
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
    /// Логика взаимодействия для ActorInfoPage.xaml
    /// </summary>
    public partial class ActorInfoPage : Page
    {
        bool isEditing = false;

        private Actor Actor;
        public ActorInfoPage(Actor actor)
        {
            InitializeComponent();
            Actor = actor;
            if (Authorization.getInstance().User.user_type_id == 1)
            { 
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            ActorName.Text = Actor.name;
            ActorCharacters.ItemsSource = Actor.FilmsActors.Select(c => c.character).Distinct().ToList();
            ActorDateOfBirth.Text = Actor.date_of_birth.ToString();
            if (Actor.date_of_birth != null)
                ActorDateOfBirth.Text = Convert.ToDateTime(Actor.date_of_birth).ToShortDateString();
            else ActorDateOfBirth.Text = "нет инф.";
            switch (this.Actor.gender)
            {
                case 1: { ActorGender.Text = "жен"; break; }
                case 2: { ActorGender.Text = "муж"; break; }
                case 0: { ActorGender.Text = "нет инф."; break; }
            }
            if (Actor.country != null)
                ActorCountry.Text = Actor.country;
            else ActorCountry.Text = "нет инф.";
            using (var db = new MyFilmsEntities())
                FilmsItemControl.ItemsSource = Actor.FilmsActors.Select(s => s.Film).Distinct().
                    Join(db.FilmsAVGRatings,
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

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
                FilmInfoPage FilmPage = new FilmInfoPage(db.Films.Find(dataObject.film_id), dataObject.avg_rating);
                NavigationService.Navigate(FilmPage);
            }
        }
    }
}
