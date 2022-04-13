using MyFilms_.NET_Framework_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilms_.NET_Framework_.Infrastructure    
{
    internal class Authorization
    {

        private static Authorization _instance;
        public static Authorization getInstance() => _instance ?? (_instance = new Authorization());


        private User _user = null;
        public User User
        {
            get => _instance._user;
            protected set => _instance._user = value;
        }



        public event Action<bool> AuthorizationChanged;

        private bool _isAuthorized = false;
        public bool IsAuthorized
        {
            get => _isAuthorized;
            protected set
            {
                _isAuthorized = value; 
                AuthorizationChanged?.Invoke(value);
            }
        }

        public void LogIn(string login, string password)
        {
            var db = new MyFilmsEntities();
            Models.User user = new User();
            user = db.Users.Where(login == user.login)
            this.User = 
            IsAuthorized = true;
        }

        public void LogOut()
        {
            IsAuthorized = false;
        }
    }
}
