using MyFilms_.NET_Framework_.Infrastructure;
using MyFilms_.NET_Framework_.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyFilms_.NET_Framework_.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private Visibility authenticationVisability = Visibility.Visible;

        public Visibility AuthenticationVisability
        {
            get => authenticationVisability;
            set => SetProperty(ref authenticationVisability, value);
        }

        Authorization auth = new Authorization();

        public MainWindowViewModel()
        {
            auth.AuthorizationChanged += LogIn;
        }

        private void LogIn(bool obj)
        {
            if (obj)
            AuthenticationVisability = Visibility.Collapsed;
        }
    }
}
