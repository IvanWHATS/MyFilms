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
    /// Логика взаимодействия для CrweInfoPage.xaml
    /// </summary>
    public partial class CrewInfoPage : Page
    {
        bool isEditing = false;

        private Crew Crew;
        public CrewInfoPage(Crew crew)
        {
            InitializeComponent();
            Crew = crew;
            if (Authorization.getInstance().User.user_type_id == 1)
            {
                EditBtn.Visibility = Visibility.Visible;
                EditBtn.IsEnabled = true;
            }
            CrewName.Text = Crew.name;
            CrewDepartments.ItemsSource =  Crew.FilmsCrews.Select(c => c.Department).Distinct().ToList();
            CrewDateOfBirth.Text = Crew.date_of_birth.ToString();
            if (Crew.date_of_birth != null)
                CrewDateOfBirth.Text = Convert.ToDateTime(Crew.date_of_birth).ToShortDateString();
            else CrewDateOfBirth.Text = "нет инф.";
            switch (this.Crew.gender)
            {
                case 1: { CrewGender.Text = "жен"; break; }
                case 2: { CrewGender.Text = "муж"; break; }
                case 0: { CrewGender.Text = "нет инф."; break; }
            }
            if (Crew.country != null)
                CrewCountry.Text = Crew.country;
            else CrewCountry.Text = "нет инф.";
            using (var db = new MyFilmsEntities())
                FilmsItemControl.ItemsSource = Crew.FilmsCrews.Select(s => s.Film).Distinct().ToList();

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
