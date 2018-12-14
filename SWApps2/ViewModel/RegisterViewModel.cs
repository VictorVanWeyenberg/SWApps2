using FluentValidation.Results;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using SWApps2.Converters;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SWApps2.ViewModel
{
    public class RegisterViewModel : ViewModelBase, IValidate
    {
        private RegisterValidator _validator;
        private RegisterWrapper _wrapper;
        private ValidationResult _validationResult;
        private string _globalError;
        private string _firstnameError;
        private string _lastnameError;
        private string _emailError;
        private string _passwordError;
        private string _passwordRepeatError;
        private string _radioButtonError;
        private AbstractUser _user;

        private const string url = "http://localhost:54100/api/register";

        public RegisterViewModel() {
            _validator = new RegisterValidator();
            _wrapper = new RegisterWrapper();
            ResetValidationErrors();
        }

        #region properties

        public string FirstName {
            get { return _wrapper.FirstName; }
            set { _wrapper.FirstName = value; }
        }

        public string LastName {
            get { return _wrapper.LastName; }
            set { _wrapper.LastName = value; }
        }

        public string Email {
            get { return _wrapper.Email; }
            set { _wrapper.Email = value; }
        }

        public string Password {
            get { return _wrapper.Password; }
            set { _wrapper.Password = value; }
        }

        public string PasswordRepeat
        {
            get { return _wrapper.PasswordRepeat; }
            set { _wrapper.PasswordRepeat = value; }
        }

        public bool IsValid { get; private set; }

        #endregion

        #region validation properties

        public string GlobalError {
            get { return _globalError; }
            set {
                if (_globalError != value)
                {
                    _globalError = value;
                    RaisePropertyChanged(nameof(GlobalError));
                }
            }
        }

        //When validating, call validate on the validator with the object to validate, then loop through the errors and set them here.
        //These properties should be bound with the view for error displays
        public string FirstNameError {
            get { return _firstnameError; }
            set {
                if (_firstnameError != value)
                {
                    _firstnameError = value;
                    RaisePropertyChanged(nameof(FirstNameError));
                }
            }
        }
        public string LastNameError {
            get { return _lastnameError; }
            set {
                if (_lastnameError != value)
                {
                    _lastnameError = value;
                    RaisePropertyChanged(nameof(LastNameError));
                }
            }
        }

        public string EmailError {
            get { return _emailError; }
            set {
                if (_emailError != value)
                {
                    _emailError = value;
                    RaisePropertyChanged(nameof(EmailError));
                }
            }
        }

        public string PasswordError {
            get { return _passwordError; }
            set {
                if (_passwordError != value)
                {
                    _passwordError = value;
                    RaisePropertyChanged(nameof(PasswordError));
                }
            }
        }

        public string PasswordRepeatError {
            get { return _passwordRepeatError; }
            set {
                if (_passwordRepeatError != value)
                {
                    _passwordRepeatError = value;
                    RaisePropertyChanged(nameof(PasswordRepeatError));
                }
            }
        }

        public string RadioButtonError {
            get { return _radioButtonError; }
            set {
                    if (_radioButtonError != value)
                    {
                        _radioButtonError = value;
                        RaisePropertyChanged(nameof(RadioButtonError));
                    }
            }
        }

        public bool IsEntrepreneur()
        {
            return _user is Entrepreneur;
        }

        #endregion

        #region methods

        public void Validate()
        {
            if (_user == null)//No radio checked
            {
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
                RadioButtonError = resourceLoader.GetString("RegisterUserBtnError");
            }
            //Validate the rest with validator
            _validationResult =  _validator.Validate(_wrapper);
            //When not valid, loop through the errors
            if (!_validationResult.IsValid)
            {
                foreach (ValidationFailure fail in _validationResult.Errors)
                {
                    MapErrorToProperty(fail);
                }
            }
            else if (_user != null)//Radio checked
            {
                ResetValidationErrors();
                _user.Email = _wrapper.Email;
                _user.LastName = _wrapper.LastName;
                _user.FirstName = _wrapper.FirstName;
                IsValid = _validationResult.IsValid;
            }
        }

        public void MapErrorToProperty(ValidationFailure fail)
        {
            switch (fail.PropertyName)
            {
                case nameof(FirstName): FirstNameError = fail.ErrorMessage;
                break;
                case nameof(LastName): LastNameError = fail.ErrorMessage;
                break;
                case nameof(Email): EmailError = fail.ErrorMessage;
                break;
                case nameof(Password): PasswordError = fail.ErrorMessage;
                break;
                case nameof(PasswordRepeat): PasswordRepeatError = fail.ErrorMessage;
                break;
            }
        }

        public void ResetValidationErrors()
        {
            FirstNameError = "";
            LastNameError = "";
            EmailError = "";
            PasswordError = "";
            PasswordRepeatError = "";
            RadioButtonError = "";
        }

        #endregion

        public void SetUser(string checkedBtnText)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            if (checkedBtnText.Equals(resourceLoader.GetString("Entrepreneur")))
            {
                _user = new Entrepreneur();
            }
            if (checkedBtnText.Equals(resourceLoader.GetString("User")))
            {
                _user = new User();
            }
        }

        public async Task<bool> DoRegisterAPICall()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string contentString = GeneratePostRequestContent();
            StringContent content = new StringContent(contentString, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(new Uri(url), content);
            if (result.IsSuccessStatusCode)
            {
                string jsonresult = await result.Content.ReadAsStringAsync();
                DownloadCompleted(jsonresult);
                return true;
            } else
            {
                GlobalError = "Server Error " + result.StatusCode + ": " + result.Content.ReadAsStringAsync();
            }
            return false;
        }

        private string GeneratePostRequestContent()
        {
            byte[] salt = SecurePassword.GetSalt();
            string hash = SecurePassword.Hash(Password, salt);

            _wrapper.Salt = Convert.ToBase64String(salt);
            _wrapper.Hash = hash;
            _wrapper.IsEntrepreneur = _user is Entrepreneur;

            return JsonConvert.SerializeObject(_wrapper);
        }

        private void DownloadCompleted(string json)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
            AbstractUser user = null;
            if (_user is Entrepreneur)
            {
                user = JsonConvert.DeserializeObject<Entrepreneur>(json, new EntrepreneurJsonConverter());
            }
            if (_user is User)
            {
                user = JsonConvert.DeserializeObject<User>(json);
            }
            var app = Application.Current as App;
            app.User = user;
        }
    }
}
