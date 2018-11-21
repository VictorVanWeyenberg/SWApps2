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
using SWApps2.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SWApps2.View
{
    /// <summary>
    /// This view represents a list of establishments, which are the result of a search operation
    /// Defaults to showing all establishments
    /// </summary>
    public sealed partial class EstablishmentListView : Page
    {
        public EstablishmentListViewModel EstablishmentList { get; set; }
        public EstablishmentListView()
        {
            InitializeComponent();
            InitializeSearchBox();
            this.DataContextChanged += (s, e) =>
            {
                EstablishmentList = DataContext as EstablishmentListViewModel;
            };
        }

        /// <summary>
        /// Set up the search box at the top of this page
        /// </summary>
        private void InitializeSearchBox()
        {
            //get the resource loader to fetch localized strings
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            inputBox.PlaceholderText = resourceLoader.GetString("EstablishSearchPlaceholder");
        }

        private void inputBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void inputBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void inputBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //if user was typing
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //searchBox.ItemsSource = myFilteredData;
            }
        }
    }
}
