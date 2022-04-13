using MyFilms_.NET_Framework_.Infrastructure;
using MyFilms_.NET_Framework_.Infrastructure.Commands;
using MyFilms_.NET_Framework_.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyFilms_.NET_Framework_.ViewModels
{
    internal class LogInViewModel : ViewModelBase
    {
        private string _loginBoxText = "";

        public string LoginBoxText
        {
            get => _loginBoxText;
            set => SetProperty(ref _loginBoxText, value);
        }

        private string _passwordBoxText = "";

        public string PasswordBoxText
        {
            get => _passwordBoxText;
            set => SetProperty(ref _passwordBoxText, value);
        }

        #region LogInCommand
        public ICommand LogInCommand { get; }

        private bool CanLogInCommandExecute(object p)
        {
            return _loginBoxText != "" && _passwordBoxText != "";
        }

        private void OnLogInCommandExecute(object p)
        {
            Authorization auth = Authorization.getInstance();
            auth.LogIn();

        }

        #endregion

        public LogInViewModel()
        {
            LogInCommand = new RelayCommand(OnLogInCommandExecute, CanLogInCommandExecute);
        }
    }
}
