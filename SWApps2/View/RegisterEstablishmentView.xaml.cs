using SWApps2.Model;
using SWApps2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SWApps2.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterEstablishmentView : Page
    {
        private RegisterEstablishmentViewModel RegisterEstablishmentViewModel;
        private INavigation _navigator;
        public RegisterEstablishmentView()
        {
            DataContextChanged += (s, e) => RegisterEstablishmentViewModel = DataContext as RegisterEstablishmentViewModel;
            InitializeComponent();
            RegisterEstablishmentViewModel.PropertyChanged += ViewOnPropertyChanged;
            InitErrorLabels();
            InitComboBox();
            InitDayPicker();
            PassEntrepreneurToViewModel();
        }

        private void PassEntrepreneurToViewModel()
        {
            App app = Application.Current as App;
            if (app.User is Entrepreneur)
            {
                RegisterEstablishmentViewModel.RegisterEntrepreneur(app.User as Entrepreneur);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator;
            base.OnNavigatedTo(e);
        }

        private void ViewOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
              switch (e.PropertyName)
            {
                case "NameError": NameError.Text = RegisterEstablishmentViewModel.NameError;
                    break;
                case "ServerError": ServerError.Text = RegisterEstablishmentViewModel.ServerError;
                    break;
                case "StreetError": StreetError.Text = RegisterEstablishmentViewModel.StreetError;
                    break;
                case "AddTagError": TagError.Text = RegisterEstablishmentViewModel.AddTagError;
                    break;
            }
        }

        private void InitErrorLabels()
        {
            TextBlock error = FindName("ServerError") as TextBlock;
            error.Text = "";
            TextBlock nameError = FindName("NameError") as TextBlock;
            nameError.Text = "";
            TextBlock streetError = FindName("StreetError") as TextBlock;
            streetError.Text = "";
            TextBlock tagError = FindName("TagError") as TextBlock;
            tagError.Text = "";
        }

        private void InitComboBox()
        {
            ComboBox combo = FindName("TypeSelection") as ComboBox;
            combo.ItemsSource = Enum.GetNames(typeof(EstablishmentType));
            combo.SelectedItem = combo.Items.First();
            RegisterEstablishmentViewModel.Type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), combo.SelectedValue as string);
        }

        private void InitDayPicker()
        {
            ComboBox dayPicker = FindName("AddHourDayPicker") as ComboBox;
            dayPicker.ItemsSource = Enum.GetNames(typeof(DayOfWeek));
            dayPicker.SelectedItem = dayPicker.Items.First();
            RegisterEstablishmentViewModel.DayForHourToAdd = dayPicker.SelectedValue as string;
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterEstablishmentViewModel.Name = (sender as TextBox).Text;
            RegisterEstablishmentViewModel.NameError = "";
        }

        private void NumberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox input = sender as TextBox;
            int value;
            //Try parsing the text
            if (int.TryParse(input.Text, out value))
            {
                //If  less than or equal to zero, reset
                if (value <= 0)
                {
                    input.Text = "";
                    return;
                }
                //pass value to VM
                RegisterEstablishmentViewModel.Number = value;
            }
            else {//Failed to parse, reset
                input.Text = "";
            }
        }

        private void TypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            RegisterEstablishmentViewModel.Type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), cb.SelectedValue as string);
        }

        private void StreetInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox input = sender as TextBox;
            RegisterEstablishmentViewModel.Street = input.Text;
            RegisterEstablishmentViewModel.StreetError = "";
        }

        private void AddHour_Click(object sender, RoutedEventArgs e)
        {
            RegisterEstablishmentViewModel.AddServiceHour();
        }

        private void AddTag_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox input = sender as TextBox;
            RegisterEstablishmentViewModel.AddTagError = "";
            RegisterEstablishmentViewModel.TagToAdd = input.Text;
        }

        private void AddTagBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterEstablishmentViewModel.AddTag();
        }

        private void AddHourDayPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox input = sender as ComboBox;
            RegisterEstablishmentViewModel.DayForHourToAdd = input.SelectedValue as string;
        }

        private void DelHourBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterEstablishmentViewModel.RemoveHourForSelectedDay();
        }

        private void RemoveTagBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterEstablishmentViewModel.RemoveTag();
        }

        private void TimePickerFrom_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            RegisterEstablishmentViewModel.StartHour = e.NewTime.Hours;
            RegisterEstablishmentViewModel.StartMinute = e.NewTime.Minutes;
        }

        private void TimePickerTo_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            RegisterEstablishmentViewModel.EndHour = e.NewTime.Hours;
            RegisterEstablishmentViewModel.EndMinute = e.NewTime.Minutes;
        }

        private void ServiceHoursOverviewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView input = sender as ListView;
            RegisterEstablishmentViewModel.SetServiceHourToRemove(input.SelectedIndex);
        }

        private void TagsOverviewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView input = sender as ListView;
            RegisterEstablishmentViewModel.SetTagToRemove(input.SelectedIndex);
        }

        private async void Submit_ClickAsync(object sender, RoutedEventArgs e)
        {
            RegisterEstablishmentViewModel.Validate();
            if (RegisterEstablishmentViewModel.IsValid && await RegisterEstablishmentViewModel.DoRegisterEstablishmentAPICall())
            {
                _navigator.Navigate("MyEstablishment", new { Navigator = _navigator});
            }
        }
    }
}
