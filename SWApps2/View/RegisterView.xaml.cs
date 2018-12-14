using SWApps2.Model;
using SWApps2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class RegisterView : Page
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        private INavigation _navigator;
        public RegisterView()
        {
            DataContextChanged += (s, e) => RegisterViewModel = DataContext as RegisterViewModel;
            InitializeComponent();
            RegisterViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
            SetupRadios();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator;
            base.OnNavigatedTo(e);
        }

        private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FirstNameError": ShowError("FirstNameError", RegisterViewModel.FirstNameError);
                    break;
                case "EmailError": ShowError("EmailError", RegisterViewModel.EmailError);
                    break;
                case "PasswordError": ShowError("PasswordError", RegisterViewModel.PasswordError);
                    break;
                case "LastNameError": ShowError("LastNameError", RegisterViewModel.LastNameError);
                    break;
                case "PasswordRepeatError": ShowError("PasswordRepeatError", RegisterViewModel.PasswordRepeatError);
                    break;
                case "RadioButtonError": ShowError("RadioError", RegisterViewModel.RadioButtonError);
                    break;
                case "GlobalError": ShowError("GlobalError", RegisterViewModel.GlobalError);
                    break;
            }
        }

        private void UserRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            RegisterViewModel.SetUser(button.Content as string);
            RegisterViewModel.RadioButtonError = "";
        }

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterViewModel.FirstName = (sender as TextBox)?.Text;
            RegisterViewModel.FirstNameError = "";
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterViewModel.LastName = (sender as TextBox)?.Text;
            RegisterViewModel.LastNameError = "";
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegisterViewModel.Email = (sender as TextBox)?.Text;
            RegisterViewModel.EmailError = "";
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterViewModel.Password = (sender as PasswordBox)?.Password;
            RegisterViewModel.PasswordError = "";
        }

        private void PasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegisterViewModel.PasswordRepeat = (sender as PasswordBox)?.Password;
            RegisterViewModel.PasswordRepeatError = "";
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterViewModel.Validate();
            if (RegisterViewModel.IsValid)
            {
                bool succeeded = await RegisterViewModel.DoRegisterAPICall();
                if (succeeded)
                {
                    bool isEntrepreneur = RegisterViewModel.IsEntrepreneur();
                    if (isEntrepreneur)
                    {
                        _navigator.Navigate("MyEstablishment", new { Navigator = _navigator });
                    }
                    else {
                        _navigator.Navigate("Establishments", new { Navigator = _navigator });
                    }
                }
            }
        }

        private void SetupRadios()
        {
            RadioButton user = FindName("UserRadio") as RadioButton;
            RadioButton entrepreneur = FindName("EntrepreneurRadio") as RadioButton;
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            user.Content = resourceLoader.GetString("User");
            entrepreneur.Content = resourceLoader.GetString("Entrepreneur");
            TextBlock radioErr = FindName("RadioError") as TextBlock;
            radioErr.Text = "";
        }

        private void ShowError(string element, string value)
        {
            TextBlock target = FindName(element) as TextBlock;
            target.Text = value ?? "";
        }
    }
}
