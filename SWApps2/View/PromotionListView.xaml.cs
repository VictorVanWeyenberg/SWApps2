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
    public sealed partial class PromotionListView : Page
    {
        public PromotionListViewModel PromotionList;
        private INavigation _navigator;
        public PromotionListView()
        {
            PromotionList = new PromotionListViewModel();
            DataContext = PromotionList;
            InitializeComponent();
            InitializeSearchBox();
        }

        /// <summary>
        /// Set up the search box at the top of this page
        /// </summary>
        private void InitializeSearchBox()
        {
            //get the resource loader to fetch localized strings
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            AutoSuggestBox search = (AutoSuggestBox)FindName("Search");
            search.PlaceholderText = resourceLoader.GetString("PromotionSearchPlaceholder");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic).Navigator as INavigation;
            base.OnNavigatedTo(e);
        }

        public void ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            Promotion selectedPromotion = (e.ClickedItem as PromotionViewModel)?.Promotion;
            Messenger.Default.Send<IDArgs>(new IDArgs(selectedPromotion.ID));
            _navigator.Navigate("Promotion", new { Navigator = _navigator, Parameter = selectedPromotion });
        }

        public void Filter(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            string lookupString = (sender as AutoSuggestBox).Text.ToLower();
            if (lookupString != string.Empty || lookupString != null)
            {
                this.PromotionList.LookupString = lookupString;
            }
            else
            {
                this.PromotionList.LookupString = null;
            }
            (FindName("items") as ListView).ItemsSource = this.PromotionList.FilteredPromotions;
        }

    }
}
