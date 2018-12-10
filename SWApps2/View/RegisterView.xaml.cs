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
    public sealed partial class RegisterView : Page
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public RegisterView()
        {
            DataContextChanged += (s, e) => RegisterViewModel = DataContext as RegisterViewModel;
            InitializeComponent();
            RegisterViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
        }

        private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UserRadio_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordRepeat_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
