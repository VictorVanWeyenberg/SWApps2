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
        public PromotionListView()
        {
            InitializeComponent();
            DataContextChanged += (s, e) => PromotionList = DataContext as PromotionListViewModel;
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

    }
}
