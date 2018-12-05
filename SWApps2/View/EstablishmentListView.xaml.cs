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
using SWApps2.ViewModel;
using SWApps2.Model;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

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
        private NavigationPage _navigator;
        private MapControl _map;
        public EstablishmentListView()
        {
            InitializeComponent();
            InitializeSearchBox();
            _map = (MapControl)this.FindName("map");
            this.DataContextChanged += (s, e) =>
            {
                EstablishmentList = DataContext as EstablishmentListViewModel;
                GeneratePointsOfInterest();
            };
            _map.ZoomLevel = 14.5;
        }

        async private void GeneratePointsOfInterest()
        {
            List<MapElement> mapLocations = new List<MapElement>();
            Geopoint referencePoint = new Geopoint(new BasicGeoposition() { Latitude = 51.0543, Longitude = 3.7174 });
            foreach (EstablishmentViewModel est in EstablishmentList.Establishments)
            {
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(est.Address.ToString(), referencePoint);

                if (result.Status == MapLocationFinderStatus.Success)
                {
                    Geopoint position = new Geopoint(new BasicGeoposition
                    {
                        Longitude = result.Locations[0].Point.Position.Longitude,
                        Latitude = result.Locations[0].Point.Position.Latitude
                    });
                    MapIcon icon = new MapIcon
                    {
                        Location = position,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = est.Name
                    };
                    mapLocations.Add(icon);
                }
            }
            MapElementsLayer positionsLayer = new MapElementsLayer
            {
                MapElements = mapLocations
            };
            _map.Layers.Add(positionsLayer);
            _map.Center = referencePoint;
            _map.UpdateLayout();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic).Navigator;
            base.OnNavigatedTo(e);
        }

        public void ItemClickHandler(object sender, ItemClickEventArgs e)
        {
            Establishment selectedEstablishment = (e.ClickedItem as EstablishmentViewModel).Establishment;
            this._navigator.Navigate("Establishment", new { Navigator = this._navigator, Parameter = selectedEstablishment });
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
