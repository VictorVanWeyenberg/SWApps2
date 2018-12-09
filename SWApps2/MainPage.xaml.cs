﻿using System;
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
            (this as INavigation)?.Navigate("Establishments", new { Navigator = (this as INavigation) });
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
            }
            _navigation.IsBackEnabled = _pageWrapper.CanGoBack;
        }

        public void BackRequestedHandler(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (_pageWrapper.CanGoBack)
            {
                _pageWrapper.GoBack();
            }
        }
    }
}
