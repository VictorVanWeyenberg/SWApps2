using FluentValidation.Results;
using GalaSoft.MvvmLight;
using NodaTime;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWApps2.ViewModel
{
    public class RegisterEstablishmentViewModel : ViewModelBase, IValidate
    {
        private Establishment _establishment;
        private RegisterEstablishmentValidator _validator;
        private ValidationResult _validationResult;

        private string _nameError;
        private string _streetErrror;
        private string _numberError;

        public RegisterEstablishmentViewModel()
        {
            _establishment = new Establishment("", new Address("", 0), new ServiceHours(), EstablishmentType.RESTAURANT, null);
            _validator = new RegisterEstablishmentValidator();
            ResetValidationErrors();
        }

        #region properties
        public bool IsValid { get; private set; }

        public string Name {
            get { return _establishment.Name; }
            set {
                if (_establishment.Name != value)
                {
                    _establishment.Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public string Street {
            get { return _establishment.Address.Street; }
            set
            {
                if (_establishment.Address.Street != value)
                {
                    _establishment.Address.Street = value;
                    RaisePropertyChanged(nameof(Street));
                }
            }
        }

        public int Number {
            get { return _establishment.Address.Number; }
            set
            {
                if (_establishment.Address.Number != value)
                {
                    _establishment.Address.Number = value;
                    RaisePropertyChanged(nameof(Number));
                }
            }
        }

        public ServiceHours ServiceHours { get { return _establishment.ServiceHours; } }

        public EstablishmentType Type {
            get { return _establishment.Type; }
            set {
                    if (_establishment.Type != value)
                    {
                        _establishment.Type = value;
                        RaisePropertyChanged(nameof(Type));
                    }
            }
        }

        public IEnumerable<string> Tags {
            get { return _establishment.Tags as IEnumerable<string>; }
        }

        #endregion

        #region error properties

        public string NameError {
            get { return _nameError; }
            set {
                    if (_nameError != value)
                    {
                        _nameError = value;
                        RaisePropertyChanged(nameof(NameError));
                    }
            }
        }

        public string StreetError {
            get { return _streetErrror; }
            set
            {
                if (_streetErrror != value)
                {
                    _streetErrror = value;
                    RaisePropertyChanged(nameof(StreetError));
                }
            }
        }

        public string NumberError {
            get { return _numberError; }
            set
            {
                if (_numberError != value)
                {
                    _numberError = value;
                    RaisePropertyChanged(nameof(NumberError));
                }
            }
        }

        #endregion

        #region methods

        public void AddServiceHour(int day, int startHour, int startMinute, int endHour, int endMinute)
        {
            if (day < 0 || day > 6) return;
            TimeInterval interval = null;
            try
            {
                interval = new TimeInterval(new LocalTime(startHour, startMinute), new LocalTime(endHour, endMinute));
                if (!_establishment.ServiceHours.Hours[day].Equals(interval))
                {
                    _establishment.ServiceHours.Hours[day] = interval;
                    RaisePropertyChanged(nameof(ServiceHours));
                }
            }
            catch (ArgumentOutOfRangeException)
            { }
        }

        public void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            if (_establishment.Tags.Contains(tag)) return;
            _establishment.Tags.Add(tag);
            RaisePropertyChanged(nameof(Tags));
        }

        public void Validate()
        {
            _validationResult = _validator.Validate(_establishment);
            if (!_validationResult.IsValid)
            {
                foreach (ValidationFailure fail in _validationResult.Errors)
                {
                    MapErrorToProperty(fail);
                }
                return;
            }
            ResetValidationErrors();
            IsValid = _validationResult.IsValid;
        }

        public void MapErrorToProperty(ValidationFailure fail)
        {
            switch (fail.PropertyName)
            {
                case nameof(_establishment.Address.Street): StreetError = fail.ErrorMessage;
                    break;
                case nameof(_establishment.Address.Number): NumberError = fail.ErrorMessage;
                    break;
                case nameof(_establishment.Name): NameError = fail.ErrorMessage;
                    break;
            }
        }

        private void ResetValidationErrors()
        {
            NameError = "";
            NumberError = "";
            StreetError = "";
        }

        #endregion
    }
}
