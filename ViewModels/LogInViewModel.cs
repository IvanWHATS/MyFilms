using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilms_.NET_Framework_.ViewModels
{
    internal class LogInViewModel : Base.ViewModelBase
    {
        private string _loginBoxText;

        public string LoginBoxText
        {
            get => _loginBoxText;
            set => SetProperty(ref _loginBoxText, value);
        }

        private string _passwordBoxText;

        public string PasswordBoxText
        {
            get => _passwordBoxText;
            set => SetProperty(ref _passwordBoxText, value);
        }



        public LogInViewModel()
        {

        }
    }
}
