using Microsoft.Win32;
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

namespace MyFilms_.NET_Framework_.Views.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для CrweInfoPage.xaml
    /// </summary>
    public partial class CrewInfoPage : Page
    {
        bool isEditing = false;

        private Crew Crew;

        MyFilmsEntities db = new MyFilmsEntities();
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
                FilmsItemControl.ItemsSource = Crew.FilmsCrews.Select(s => s.Film).Distinct().
                    Join(db.FilmsAVGRatings,
                    f => f.film_id,
                    a => a.film_id,
                    (f,a) => new FilmsAVGRating
                    {
                        film_id = f.film_id,
                        title = f.title,
                        poster = f.poster,
                        avg_rating = a.avg_rating
                    }).ToList();

            if (Crew.photo is null)
                CrewPhoto.Source = new BitmapImage(new Uri("pack://application:,,,/Source/NoImage.png"));
            else
                CrewPhoto.Source = ImageConverter.BytesToImage(Crew.photo);

        }



        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true;
                EditBtnText.Text = "Сохранить";
            }
            else
            {
                isEditing = false;
                EditBtnText.Text = "Редактировать";
                db.SaveChanges();
            }
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

        private void UploadCrewPhoto(object sender, MouseButtonEventArgs e)
        {
            if (isEditing)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Images |*.png;*.jpg";
                var result = fileDialog.ShowDialog();
                if (result == true)
                {
                    Crew.photo = ImageConverter.GetImageBytes(fileDialog.FileName);
                    db.Crews.Find(Crew.crew_id).photo = Crew.photo;
                    CrewPhoto.Source = ImageConverter.BytesToImage(Crew.photo);
                }

            }
        }
    }
}
