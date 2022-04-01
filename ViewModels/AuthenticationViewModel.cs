using MyFilms_.NET_Framework_.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyFilms_.NET_Framework_.ViewModels
{
    internal class AuthenticationViewModel : Base.ViewModelBase
    {
        #region Страницы

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private Page LogInPage;
        private Page RegistrationPage;

        #endregion

        #region LogInButtonClickCommand
        public ICommand LogInButtonClickCommand {get;}

        private bool CanLogInButtonClickCommandExecute(object p)
        {
            return _currentPage == RegistrationPage;
        }

        private void OnLogInButtonClickCommandExecute(object p)
        {
            CurrentPage = LogInPage;
        }

        #endregion

        #region RegistrationButtonClickCommand
        public ICommand RegistrationButtonClickCommand { get; }

        private bool CanRegistrationButtonClickCommandExecute(object p)
        {
            return _currentPage == LogInPage;
        }

        private void OnRegistrationButtonClickCommandExecute(object p)
        {
            CurrentPage = RegistrationPage;
        }

        #endregion

        public AuthenticationViewModel()
        {
            LogInPage = new Views.Pages.Authentication.LogInPage();
            RegistrationPage = new Views.Pages.Authentication.RegistrationPage();

            CurrentPage = LogInPage;


            LogInButtonClickCommand = new RelayCommand(OnLogInButtonClickCommandExecute, CanLogInButtonClickCommandExecute);
            RegistrationButtonClickCommand = new RelayCommand(OnRegistrationButtonClickCommandExecute, CanRegistrationButtonClickCommandExecute);
        }

        

    }
}
