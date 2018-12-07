using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class is a wrapper for use with an AbstractValidator for login
    /// <see cref="FluentValidation.AbstractValidator{T}"/>
    /// </summary>
    [NotMapped]
    public class LoginWrapper
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginWrapper()
        {
            Email = "";
            Password = "";
        }
    }
}
