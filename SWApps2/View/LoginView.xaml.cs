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
    public sealed partial class LoginView : Page
    {
        private INavigation _navigator;
        public LoginViewModel LoginViewModel { get; set; }

        public LoginView()
        {
            DataContextChanged +=(s, e) => LoginViewModel = DataContext as LoginViewModel;
            InitializeComponent();
            LoginViewModel.PropertyChanged += UpdateUI_PropertyChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator;
            base.OnNavigatedTo(e);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Validate();
            if (LoginViewModel.IsValid)
            {
                //push API request
                throw new NotImplementedException();
            }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginViewModel.Email = (sender as TextBox)?.Text;
            LoginViewModel.EmailError = "";
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Password = (sender as PasswordBox)?.Password;
            LoginViewModel.PasswordError = "";
        }

        private void UpdateUI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Show error
            switch (e.PropertyName)
            {
                case "EmailError": ShowError("EmailError", LoginViewModel.EmailError);
                    break;
                case "PasswordError": ShowError("PasswordError", LoginViewModel.PasswordError);
                    break;
            }
        }

        private void ShowError(string element, string value)
        {
            TextBlock target = FindName(element) as TextBlock;
            target.Text = value ?? "";
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            _navigator.Navigate("Register", new { Navigator = _navigator });
        }
    }
}
