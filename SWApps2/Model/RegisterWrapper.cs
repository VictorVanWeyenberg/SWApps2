using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class is a wrapper for use with an <see cref="FluentValidation.AbstractValidator{T}"/> for registration
    /// </summary>
    public class RegisterWrapper
    {

        #region Properties

        /// <summary>
        /// The firstname for the registration request
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The lastname for the registration request
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email for the registration request
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The password for the registration request
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// A field to match the password to during registration, to ensure a correct password is given
        /// </summary>
        public string PasswordRepeat { get; set; }

        public bool IsEntrepreneur{ get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }




        #endregion
        public RegisterWrapper()
        {
            FirstName = "";
            LastName = "";
            Password = "";
            PasswordRepeat = "";
            Email = "";
            IsEntrepreneur = false;
        }
    }
}
