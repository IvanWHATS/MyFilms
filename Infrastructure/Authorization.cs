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

        private Authorization()
        {
        }

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

        public bool LogIn(string login, string password)
        {
            using (var db = new MyFilmsEntities())
            {
                var user = new Models.User();
                if ((user = db.Users.Find(login)) != null)
                    if (user.password == password)
                    {
                        this.User = user;
                        IsAuthorized = true;
                        return true;
                    }
            }
            return false;
        }

        public bool Register(string login, string password, string nickname)
        {
            using (var db = new MyFilmsEntities())
            {
                if (db.Users.Find(login) == null)
                {
                    User = new User
                    {
                        login = login,
                        password = password,
                        nickname = nickname,
                        picture = null,
                        user_type_id = 0
                    };
                    db.Users.Add(User);
                    db.SaveChanges();
                    IsAuthorized = true;
                    return true;
                }

            }
            return false;
        }

        public void LogOut()
        {
            this.User = null;
            IsAuthorized = false;
        }
    }
}
