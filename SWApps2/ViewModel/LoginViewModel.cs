using FluentValidation.Results;
using GalaSoft.MvvmLight;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
