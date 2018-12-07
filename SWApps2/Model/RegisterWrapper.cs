using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class is a wrapper for use with an AbstractValidator for registration
    /// <see cref="FluentValidation.AbstractValidator{T}"/>
    /// </summary>
    [NotMapped]
    public class RegisterWrapper
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordRepeat { get; set; }

        public RegisterWrapper()
        {
            FirstName = "";
            LastName = "";
            Password = "";
            PasswordRepeat = "";
            Email = "";
        }
    }
}
