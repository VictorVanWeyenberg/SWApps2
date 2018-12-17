using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class represents an application user
    /// </summary>
    public abstract class AbstractUser
    {
        /// <summary>
        /// Sole constructor
        /// </summary>
        /// <param name="firstname">The first name of the user</param>
        /// <param name="lastname">The last name of the user</param>
        /// <param name="email">The email of the user</param>
        protected AbstractUser(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
        protected AbstractUser(int id, string firstname, string lastname, string email) : 
            this(firstname, lastname, email)
        {
            ID = id;
        }

        #region Properties

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        #endregion
    }
}
