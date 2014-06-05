using Cinema.Phone.ExecuteQueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Phone.Model
{
    public class SingletonUser
    {
        private static User user = null;

        public static User User
        {
            get
            {
                if (user == null)
                {
                    user = new User();
                }

                return SingletonUser.user;
            }
        }

        private SingletonUser()
        {
        }
    }
}
