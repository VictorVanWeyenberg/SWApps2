using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.Model
{
    public class User : ObservableObject
    {
        //firstname of the user
        private string _firstname;
        //lastname of the user
        private string _lastname;
        //the email address of the user
        //use Email regex?
        private string _email;

        //List of establishments the user is subscribed to
        //Might change later
        private List<Establishment> _subsribedEstablishments;

        //Constructor
        public User(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }

        //FirstName property
        public string FirstName
        {
            ///<summary>
            ///get and set FirstName property
            ///Setter uses <see cref="ObservableObject.Set{T}(string, ref T, T)"/>
            /// </summary>
            get { return _firstname; }
            set { Set("FirstName", ref _firstname, value); }
        }

        //LastName property
        public string LastName
        {
            ///<summary>
            ///get and set LastName property
            ///Setter uses <see cref="ObservableObject.Set{T}(string, ref T, T)"/>
            /// </summary>
            get { return _lastname; }
            set { Set("LastName", ref _lastname, value); }
        }

        //Email property
        public string Email
        {
            ///<summary>
            ///get and set Email property
            ///Setter uses <see cref="ObservableObject.Set{T}(string, ref T, T)"/>
            /// </summary>
            get { return _email; }
            set { Set("Email", ref _email, value); }
        }
    }
}
