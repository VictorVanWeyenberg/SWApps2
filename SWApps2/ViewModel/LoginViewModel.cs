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

        public LoginViewModel() {
            _wrapper = new LoginWrapper();
            _validator = new LoginValidator();
            ResetValidationErrors();
        }

        #region properties
        public string Email {
            get { return _wrapper.Email; }
            set { _wrapper.Email = value; }
        }
        public string Password {
            get { return _wrapper.Password; }
            set { _wrapper.Password = value; }
        }

        public string EmailError { get; set; }
        public string PasswordError { get; set; }

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

        public void ResetValidationErrors()
        {
            EmailError = "";
            PasswordError = "";
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
            else
            {
                ResetValidationErrors();
            }
        }
    }
}
