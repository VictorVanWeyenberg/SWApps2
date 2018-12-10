using GalaSoft.MvvmLight;

namespace SWApps2.Model
{
    /// <summary>
    /// This class is a wrapper for use with an <see cref="FluentValidation.AbstractValidator{T}"/> for login
    /// </summary>
    public class LoginWrapper
    {
        /// <summary>
        /// The email to use for the login request
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The password to use for the login request
        /// </summary>
        public string Password { get; set; }

        public LoginWrapper()
        {
            Email = "";
            Password = "";
        }
    }
}
