using FluentValidation.Results;
using GalaSoft.MvvmLight;
using NodaTime;
using SWApps2.Model;
using SWApps2.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SWApps2.ViewModel
{
    public class RegisterEstablishmentViewModel : ViewModelBase, IValidate
    {
        private const string CLOSED = "CLOSED";
        private const string POSTURL = "http://localhost:54100/api/establishment/new";

        private Establishment _establishment;
        private Entrepreneur _entrepreneur;
        private RegisterEstablishmentValidator _validator;
        private ValidationResult _validationResult;

        private string _nameError;
        private string _streetErrror;
        private string _addTagError;
        private string _tempTag;
        private int _hourTorRemove;
        private int _tagToRemove;
        private string _serverError;

        public RegisterEstablishmentViewModel()
        {
            _establishment = new Establishment("", new Address("", 0), new ServiceHours(), EstablishmentType.RESTAURANT, null);
            _validator = new RegisterEstablishmentValidator();
            ResetValidationErrors();
            ServerError = "";
            Tags = new ObservableCollection<string>();
            InitializeObservableHours();
        }

        #region properties
        public bool IsValid { get; private set; }

        public string DayForHourToAdd { get; set; }

        public int StartHour { get; set; }
        public int StartMinute { get; set; }

        public int EndHour { get; set; }
        public int EndMinute { get; set; }

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

        public string TagToAdd {
            get { return _tempTag; }
            set {
                if (_tempTag != value)
                {
                    _tempTag = value;
                    RaisePropertyChanged(nameof(TagToAdd));
                }
            }
        }

        public ObservableCollection<string> ServiceHours { get; private set; }

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

        public ObservableCollection<string> Tags { get; }

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

        public string AddTagError {
            get { return _addTagError; }
            set {
                if (_addTagError != value)
                {
                    _addTagError = value;
                    RaisePropertyChanged(nameof(AddTagError));
                }
            }
        }

        public string ServerError { get { return _serverError; }
            private set {
                if (_serverError != value)
                {
                    _serverError = value;
                    RaisePropertyChanged(nameof(ServerError));
                }
            }
        }

        #endregion

        #region methods

        private void InitializeObservableHours()
        {
            ServiceHours = new ObservableCollection<string>();
            foreach (var day in Enum.GetValues(typeof(DayOfWeek)))
            {
                ServiceHours.Add(string.Format("{0}:", day));
            }
        }

        /// <summary>
        /// Returns a string representation of the opening hours for a certain day
        /// </summary>
        /// <param name="number">The day of the week as an integer in the range [0-6]</param>
        /// <returns></returns>
        public string HoursForDayToString(int number)
        {
            if (number >= 0 && number < 7)
            {
                //Get the day as string
                string dayOfWeek = ((DayOfWeek)number).ToString();
                //Get the hours
                TimeInterval day = _establishment.ServiceHours.Hours[number];
                //If there is an object -> hours available
                //Else they are closed on said day
                return string.Format("{0}: {1}", dayOfWeek, day.ToString() ?? CLOSED);
            }
            //Invalid day
            return "";
        }

        public void AddServiceHour()
        {
            DayOfWeek dow = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayForHourToAdd);
            int day = (int)dow;
            TimeInterval newHours = new TimeInterval(new LocalTime(StartHour, StartMinute), new LocalTime(EndHour, EndMinute));
            TimeInterval currentHour = _establishment.ServiceHours.Hours[day];
            if (currentHour?.Equals(newHours) != true)
            {
                _establishment.ServiceHours.Hours[day] = newHours;
                ServiceHours[day] = HoursForDayToString(day);
                RaisePropertyChanged(nameof(ServiceHours));
            }
        }

        public void AddTag()
        {
            if (string.IsNullOrWhiteSpace(TagToAdd))
            {
                AddTagError = "Tag should not be whitespace";
                return;
            }
            if (_establishment.Tags.Contains(TagToAdd))
            {
                AddTagError = "Tag already exists";
                return;
            } 
            _establishment.Tags.Add(TagToAdd);
            Tags.Add(TagToAdd);
            RaisePropertyChanged(nameof(Tags));
        }

        public void SetTagToRemove(int selectedIndex)
        {
            _tagToRemove = selectedIndex;
        }

        public void RemoveTag()
        {
            _establishment.Tags.RemoveAt(_tagToRemove);
            Tags.RemoveAt(_tagToRemove);
            RaisePropertyChanged(nameof(Tags));
        }

        public async Task<bool> DoRegisterEstablishmentAPICall()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            JObject body = new JObject();
            body.Add(new JProperty("Email", _entrepreneur.Email));
            body.Add(new JProperty("Name", _establishment.Name));
            body.Add(new JProperty("Type", Enum.GetName(typeof(EstablishmentType), _establishment.Type)));
            body.Add(new JProperty("Street", _establishment.Address.Street));
            body.Add(new JProperty("Number", _establishment.Address.Number));
            body.Add(new JProperty("Tags", MapTagsToJArray(_establishment.Tags.ToArray())));
            body.Add(new JProperty("ServiceHours", MapServiceHoursToJArray()));
            
            string jsonContent = JsonConvert.SerializeObject(body);
            var result = await client.PostAsync(new Uri(POSTURL), new StringContent(jsonContent, Encoding.UTF8, "application/json"));
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else {
                ServerError = string.Format("Something went wrong: {0}", result.ReasonPhrase);
                return false;
            }
        }

        public void Validate()
        {
            ResetValidationErrors();
            _validationResult = _validator.Validate(_establishment);
            if (!_validationResult.IsValid)
            {
                foreach (ValidationFailure fail in _validationResult.Errors)
                {
                    MapErrorToProperty(fail);
                }
            }
            IsValid = _validationResult.IsValid;
            if (IsValid)
            {
                _entrepreneur.Establishment = _establishment;
            }
        }

        public void RemoveHourForSelectedDay()
        {
            _establishment.ServiceHours.Hours[_hourTorRemove] = null;
            ServiceHours[_hourTorRemove] = string.Format("{0}:", Enum.GetName(typeof(DayOfWeek), _hourTorRemove));
            RaisePropertyChanged(nameof(ServiceHours));
        }

        public void SetServiceHourToRemove(int selectedIndex)
        {
            _hourTorRemove = selectedIndex;
        }

        public void MapErrorToProperty(ValidationFailure fail)
        {
            switch (fail.PropertyName)
            {
                case "Address.Street": StreetError = fail.ErrorMessage;
                    break;
                case nameof(_establishment.Name): NameError = fail.ErrorMessage;
                    break;
                case nameof(_establishment.Tags): AddTagError = fail.ErrorMessage;
                    break;
            }
        }

        private void ResetValidationErrors()
        {
            NameError = "";
            StreetError = "";
            AddTagError = "";
        }

        public void RegisterEntrepreneur(Entrepreneur entrepreneur)
        {
            _entrepreneur = entrepreneur;
        }

        private JArray MapTagsToJArray(string[] tags)
        {
            JArray array = new JArray();
            foreach (string tag in tags)
            {
                JObject obj = new JObject();
                obj.Add(new JProperty("Value", tag));
                array.Add(obj);
            }
            return array;
        }

        private JArray MapServiceHoursToJArray()
        {
            if (_establishment.ServiceHours.Hours == null) return null;
            JArray array = new JArray();
            for (int i =0; i<_establishment.ServiceHours.Hours.Count(); i++)
            {
                JObject jsonTime = new JObject();
                TimeInterval t = _establishment.ServiceHours.Hours[i];
                jsonTime.Add(new JProperty("Index", i));
                if (t != null)
                {
                    jsonTime.Add(new JProperty("StartHour", t.Start.Hour));
                    jsonTime.Add(new JProperty("StartMinute", t.Start.Minute));
                    jsonTime.Add(new JProperty("EndHour", t.End.Hour));
                    jsonTime.Add(new JProperty("EndMinute", t.End.Minute));
                }
                array.Add(jsonTime);
            }
            return array;
        }
        #endregion
    }
}
