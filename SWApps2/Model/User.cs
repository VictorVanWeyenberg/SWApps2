using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    /// <summary>
    /// This class represents an application user that is not an <see cref="Entrepreneur"/>
    /// </summary>
    public class User : AbstractUser
    {
        /// <summary>
        /// A list of establishments the user is subscribed to
        /// </summary>
        public List<Establishment> SubsribedEstablishments { get; }


        public User(string firstname, string lastname, string email) : base(firstname, lastname, email)
        {
            SubsribedEstablishments = new List<Establishment>();
        }
    }
}
