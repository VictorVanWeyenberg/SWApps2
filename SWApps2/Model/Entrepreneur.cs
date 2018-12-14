using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// An Entrepreneur is an owner of an <see cref="Establishment"/>
    /// </summary>
    public class Entrepreneur : AbstractUser
    {
        public Entrepreneur() : this(null,null,null,null)
        {}

        /// <summary>
        /// Sole constructor
        /// </summary>
        /// <param name="firstname">The entrepreneur's first name</param>
        /// <param name="lastname">The entrepreneur's last name</param>
        /// <param name="email">The entrepreneur's email</param>
        /// <param name="establishment">The entrepreneur's establishment</param>
        public Entrepreneur(string firstname, string lastname, string email, Establishment establishment)
            : base(firstname, lastname, email)
        {
            Establishment = establishment;
        }

        public Establishment Establishment { get; set; }
    }
}
