using FluentValidation.Results;
using GalaSoft.MvvmLight;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class RegisterViewModel : ViewModelBase, IValidate
    {
        private RegisterValidator _validator;
        private RegisterWrapper _wrapper;
        private ValidationResult _validationResult;

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

        #endregion

        #region validation properties
        //When validating, call validate on the validator with the object to validate, then loop through the errors and set them here.
        //These properties should be bound with the view for error displays
        public string FirstNameError { get; set; }
        public string LastNameError { get; set; }

        public string EmailError { get; set; }

        public string PasswordError { get; set; }

        public string PasswordRepeatError { get; set; }

        public void Validate()
        {
            _validationResult =  _validator.Validate(_wrapper);
            //When not valid, loop through the errors
            if (!_validationResult.IsValid)
            {
                foreach (ValidationFailure fail in _validationResult.Errors)
                {
                    MapErrorToProperty(fail);
                }
            }
            else {
                ResetValidationErrors();
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
        }
        #endregion
    }
}
