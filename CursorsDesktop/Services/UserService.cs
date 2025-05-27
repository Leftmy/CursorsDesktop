using CursorsDesktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursorsDesktop.Services
{
    class UserService
    {
        public User login(string email, string password)
        {
            // запит до дб
            return new User();
        }

        public User registration( string name, string email, string password)
        {
            // запит до дб

            return login(email, password);

        }



    }
}
