using GalaSoft.MvvmLight.Messaging;
using SWApps2.Model;
using SWApps2.ViewModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SWApps2.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventListView : Page
    {
        public EstablishmentEventListViewModel EventList { get; set; }

        private INavigation _navigator;
        public EventListView()
        {
            DataContextChanged += (s, e) =>
            {
                EventList = DataContext as EstablishmentEventListViewModel;
            };
            InitializeComponent();
            InitializeSearchBox();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator;
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Set up the search box at the top of this page
        /// </summary>
        private void InitializeSearchBox()
        {
            //get the resource loader to fetch localized strings
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            AutoSuggestBox search = (AutoSuggestBox)FindName("Search");
            search.PlaceholderText = resourceLoader.GetString("EventSearchPlaceholder");
        }

        public void ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            EstablishmentEvent selectedEvent = (e.ClickedItem as EstablishmentEventViewModel)?.Event;
            Messenger.Default.Send(new IDArgs(selectedEvent.ID));
            _navigator.Navigate("Event", new { Navigator = _navigator, Parameter = selectedEvent });
        }

        public void Filter(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            string lookupString = (sender as AutoSuggestBox).Text.ToLower();
            if (lookupString != string.Empty || lookupString != null)
            {
                this.EventList.LookupString = lookupString;
            }
            else
            {
                this.EventList.LookupString = null;
            }
            (FindName("items") as ListView).ItemsSource = this.EventList.FilteredEvents;
        }
    }
}
