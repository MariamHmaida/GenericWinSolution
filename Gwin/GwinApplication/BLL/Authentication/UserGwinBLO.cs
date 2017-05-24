
using App.Gwin.Entities.Secrurity.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Application.BAL.Authentication
{
    /// <summary>
    /// User Gwin Business Logic
    /// </summary>
    public class UserGwinBLO
    {

        /// <summary>
        /// Create Guest User
        /// </summary>
        /// <returns></returns>
        public User CreateGuestUser()
        {
            User user = new User();
            user.LastName.Current = "Guest";
            return user;
        }

      
        
    }
}
