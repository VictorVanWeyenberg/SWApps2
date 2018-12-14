using System;
using System.Collections.Generic;
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
using SWApps2.Data;
using SWApps2.Model;
using SWApps2.View;
using Windows.Foundation.Metadata;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SWApps2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INavigation
    {
        public User User {
            get {
                var user = (Application.Current as App).User;
                return user is User ? user as User : null;
            }
        }

        public Entrepreneur Entrepreneur {
            get {
                var user = (Application.Current as App).User;
                return user is Entrepreneur ? user as Entrepreneur : null;
            }
        }
        private Frame _pageWrapper;
        private NavigationView _navigation;
        public MainPage()
        {
            InitializeComponent();
            _pageWrapper = FindName("PageWrapper") as Frame;
            _navigation = FindName("Nav") as NavigationView;
            _navigation.IsBackEnabled = _pageWrapper.CanGoBack;
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6))
            {
                _navigation.BackRequested += BackRequestedHandler;
            }
            (this as INavigation)?.Navigate("Establishments", new { Navigator = this as INavigation });
        }

        private void Change_Page(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem selectedViewItem = args.SelectedItem as NavigationViewItem;
            (this as INavigation)?.Navigate(selectedViewItem.Name, new { Navigator = this as INavigation });
        }

        void INavigation.Navigate(string pageName, object Parameters)
        {
            switch (pageName)
            {
                case "Establishments":
                    _pageWrapper.Navigate(typeof(EstablishmentListView), Parameters);
                    break;
                case "Promotions":
                    _pageWrapper.Navigate(typeof(PromotionListView), Parameters);
                    break;
                case "Establishment":
                    _pageWrapper.Navigate(typeof(EstablishmentView), Parameters);
                    break;
                case "Login":
                    _pageWrapper.Navigate(typeof(LoginView), Parameters);
                    break;
                case "Register":
                    _pageWrapper.Navigate(typeof(RegisterView), Parameters);
                    break;
                case "RegisterEstablishment": _pageWrapper.Navigate(typeof(RegisterEstablishmentView), Parameters);
                    break;
                case "LogOut": Logout(); break;
                case "Subscriptions": break;    //TODO
                case "MyEstablishment": NavigateToMyEstablishmentIfPresent(); break;
            }
            _navigation.IsBackEnabled = _pageWrapper.CanGoBack;
            UpdateView();
        }

        public void BackRequestedHandler(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            //Check go back in certain cases (eg after login or register)!!!
            if (_pageWrapper.CanGoBack)
            {
                _pageWrapper.GoBack();
            }
        }

        private void UpdateView()
        {
            //Fetch app user
            AbstractUser currentUser = (Application.Current as App).User;
            if (currentUser == null)
            {
                ApplyAnonymousUserNavigation();
                return;
            }
            if (currentUser is Entrepreneur)
            {
                ApplyEntrepreneurNavigation(currentUser as Entrepreneur);
                return;
            }
            if (currentUser is User)
            {
                ApplyUserNavigation();
            }
        }

        private void ApplyAnonymousUserNavigation()
        {
            //Visible
            (FindName("Login") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Establishments") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Promotions") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Events") as NavigationViewItem).Visibility = Visibility.Visible;
            //Invisible
            (FindName("MyEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("LogOut") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("RegisterEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("Subscriptions") as NavigationViewItem).Visibility = Visibility.Collapsed;
        }

        private void ApplyEntrepreneurNavigation(Entrepreneur entrepreneur)
        {
            //Visible
            (FindName("LogOut") as NavigationViewItem).Visibility = Visibility.Visible;
            if (entrepreneur.Establishment != null)
            {
                (FindName("Promotions") as NavigationViewItem).Visibility = Visibility.Visible;
                (FindName("Events") as NavigationViewItem).Visibility = Visibility.Visible;
                (FindName("MyEstablishment") as NavigationViewItem).Visibility = Visibility.Visible;
                (FindName("RegisterEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
            }
            else {
                (FindName("Promotions") as NavigationViewItem).Visibility = Visibility.Collapsed;
                (FindName("Events") as NavigationViewItem).Visibility = Visibility.Collapsed;
                (FindName("MyEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
                (FindName("RegisterEstablishment") as NavigationViewItem).Visibility = Visibility.Visible;
            }
            //Invisible
            (FindName("Subscriptions") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("Login") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("Establishments") as NavigationViewItem).Visibility = Visibility.Collapsed;
        }

        private void ApplyUserNavigation()
        {
            //Visible
            (FindName("Subscriptions") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Establishments") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Promotions") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("Events") as NavigationViewItem).Visibility = Visibility.Visible;
            (FindName("LogOut") as NavigationViewItem).Visibility = Visibility.Visible;
            //Invisible
            (FindName("Login") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("MyEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
            (FindName("RegisterEstablishment") as NavigationViewItem).Visibility = Visibility.Collapsed;
        }

        private void NavigateToMyEstablishmentIfPresent()
        {
            AbstractUser user = (Application.Current as App).User;
            if (user is Entrepreneur)
            {
                if ((user as Entrepreneur).Establishment != null)
                {
                    //Navigate to establishment detail page
                }
                else {
                    _pageWrapper.Navigate(typeof(NoEstablishmentView), new { Navigator = this as INavigation });
                }
            }
            else {
                //Should not happen anyway
                _pageWrapper.Navigate(typeof(EstablishmentListView), new { Navigator = this as INavigation });
            }
        }

        private void Logout()
        {
            var app = Application.Current as App;
            app.User = null;
            (this as INavigation)?.Navigate("Establishments", new { Navigator = this as INavigation });
        }
    }
}
