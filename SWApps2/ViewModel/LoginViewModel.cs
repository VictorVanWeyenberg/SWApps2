using FluentValidation.Results;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SWApps2.Converters;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SWApps2.ViewModel
{
    public class LoginViewModel : ViewModelBase, IValidate
    {
        private LoginWrapper _wrapper;
        private LoginValidator _validator;
        private ValidationResult _validationResult;
        private bool _isValid;

        private string _emailError;
        private string _passwordError;

        private const string loginUrl = "http://localhost:54100/api/login";
        private const string getUserUrl = "http://localhost:54100/api/getUser";

        public LoginViewModel() {
            _wrapper = new LoginWrapper();
            _validator = new LoginValidator();
            EmailError = "";
            PasswordError = "";
        }

        #region properties
        public string Email {
            get { return _wrapper.Email; }
            set {
                if (_wrapper.Email != value)
                {
                    _wrapper.Email = value;
                    RaisePropertyChanged(nameof(Email));
                }
            }
        }
        public string Password {
            get { return _wrapper.Password; }
            set {
                if (_wrapper.Password != value)
                {
                    _wrapper.Password = value;
                    RaisePropertyChanged(nameof(Password));
                }
            }
        }

        public string EmailError { get { return _emailError; }
            set {
                if (_emailError != value)
                {
                    _emailError = value;
                    RaisePropertyChanged(nameof(EmailError));
                }
            }
        }
        public string PasswordError {
            get { return _passwordError; } set {
                if (_passwordError != value)
                {
                    _passwordError = value;
                    RaisePropertyChanged(nameof(PasswordError));
                }
            }
        }

        public bool IsValid {
            get { return _isValid; }
            set { if (value != _isValid)
                    _isValid = value;
                    RaisePropertyChanged(nameof(IsValid));
                }
        }

        #endregion

        public void MapErrorToProperty(ValidationFailure fail)
        {
            if (fail.PropertyName.Equals(nameof(Email)))
            {
                EmailError = fail.ErrorMessage;
            }
            if (fail.PropertyName.Equals(nameof(Password)))
            {
                PasswordError = fail.ErrorMessage;
            }
        }

        public void Validate()
        {
            _validationResult = _validator.Validate(_wrapper);
            if (!_validationResult.IsValid)
            {
                foreach (ValidationFailure fail in _validationResult.Errors)
                {
                    MapErrorToProperty(fail);
                }
            }
            IsValid = _validationResult.IsValid;
        }

        public async Task<bool> DoLoginAPICall()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var emailJsonObject = new { Email = Email };
            string jsonContent = JsonConvert.SerializeObject(emailJsonObject);
            var result = await client.PostAsync(new Uri(loginUrl), new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                string jsonResult = await result.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(jsonResult);
                string hash = jObject.Value<string>("Hash");
                string salt = jObject.Value<string>("Salt");
                if (SecurePassword.ConfirmPassword(hash, salt, Password))
                    return await LoginUser(jObject.Value<int>("ID"));
                else
                    PasswordError = "Wrong email password combination.";
            } else
            {
                PasswordError = "Server Error " + result.StatusCode + ": " + result.ReasonPhrase;
            }
            return false;
        }

        private async Task<bool> LoginUser(int id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var idJsonObject = new { Id = id };
            string jsonContent = JsonConvert.SerializeObject(idJsonObject);
            var result = await client.PostAsync(new Uri(getUserUrl), new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            JObject jsonResponse = JObject.Parse(await result.Content.ReadAsStringAsync());
            string type = jsonResponse.Value<string>("Type");
            switch (type)
            {
                case "Entrepreneur":
                    (Application.Current as App).User = JsonConvert.DeserializeObject<Entrepreneur>(jsonResponse.ToString(), new EntrepreneurJsonConverter());
                    return true;
                case "User":
                    (Application.Current as App).User = JsonConvert.DeserializeObject<User>(jsonResponse.ToString());
                    return true;
                default:
                    return false;
            }
        }
    }
}
