using SWApps2.Model;
using SWApps2.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class EstablishmentView : Page
    {
        public EstablishmentViewModel Establishment { get; set; }
        private NavigationPage _navigator;
        private MapControl _map;
        public EstablishmentView()
        {
            this.DataContextChanged += (s, e) =>
            {
                Establishment = DataContext as EstablishmentViewModel;
                GeneratePointOfInterest();
            };
            this.InitializeComponent();
            _map = (MapControl)this.FindName("map");
            _map.ZoomLevel = 14.5;
        }

        async private void GeneratePointOfInterest()
        {
            List<MapElement> mapLocations = new List<MapElement>();
            Geopoint referencePoint = new Geopoint(new BasicGeoposition() { Latitude = 51.0543, Longitude = 3.7174 });
            if (Establishment.Establishment == null) return;
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(Establishment.Address.ToString(), referencePoint);

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
                    Title = Establishment.Name
                };
                mapLocations.Add(icon);
            }
            MapElementsLayer positionsLayer = new MapElementsLayer
            {
                MapElements = mapLocations
            };
            _map.Layers.Add(positionsLayer);
            _map.Center = referencePoint;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._navigator = (e.Parameter as dynamic).Navigator;
            this.Establishment.Establishment = (e.Parameter as dynamic).Parameter;
        }
    }
}
