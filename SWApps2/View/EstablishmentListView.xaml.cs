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
using SWApps2.Model;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using SWApps2.CustomControls;
using System.Threading.Tasks;
using SWApps2.Services;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SWApps2.View
{
    /// <summary>
    /// This view represents a list of establishments, which are the result of a search operation
    /// Defaults to showing all establishments
    /// </summary>
    public sealed partial class EstablishmentListView : Page
    {
        public EstablishmentListViewModel EstablishmentListViewModel { get; set; }
        private EstablishmentListViewItem _selectedItem;
        private INavigation _navigator;
        private MapControl _map;
        public EstablishmentListView()
        {
            DataContextChanged += (s, e) => EstablishmentListViewModel = DataContext as EstablishmentListViewModel;
            InitializeComponent();
            FillListWithItems();
            InitializeMap();
            InitializeSearchBox();
        }

        private void GeneratePOI(string address, string name)
        {
            List<MapElement> mapLocations = new List<MapElement>();
            //Create MapLocationFinderResult w/ address.ToString & Name
            TaskCompletionNotifier<MapLocationFinderResult> task =
                new TaskCompletionNotifier<MapLocationFinderResult>(GPSService.FindLocationForAddress(address));
            if (task.IsSuccessfullyCompleted)
            {
                MapLocationFinderResult result = task.Result;
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
                        Title = name
                    };
                    mapLocations.Add(icon);
                }
                MapElementsLayer positionsLayer = new MapElementsLayer
                {
                    MapElements = mapLocations
                };
                _map.Layers.Add(positionsLayer);
                _map.Center = GPSService.CenterOfMap;
                _map.UpdateLayout();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator;
            base.OnNavigatedTo(e);
        }

        private void InitializeSearchBox()
        {
            //get the resource loader to fetch localized strings
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            AutoSuggestBox search = (AutoSuggestBox)FindName("Search");
            search.PlaceholderText = resourceLoader.GetString("EstablishSearchPlaceholder");
            //TODO: eventHandlers
        }

        private void InitializeMap()
        {
            GPSMap mapControl = (GPSMap)FindName("MapControl");
            _map = (MapControl)mapControl.FindName("Map");
            _map.ZoomLevel = 14.5;
            _map.Center = GPSService.CenterOfMap;
            _map.UpdateLayout();
        }

        private void FillListWithItems()
        {
            ObservableCollection<EstablishmentListViewItem> collection = new ObservableCollection<EstablishmentListViewItem>();
            foreach (EstablishmentListViewItemViewModel itemVM in EstablishmentListViewModel.Items)
            {
                collection.Add(new EstablishmentListViewItem
                {
                    ViewModel = itemVM,
                    Navigator = _navigator
                });
            }
            ListView list = FindName("items") as ListView;
            list.ItemsSource = collection;
        }

        private void Items_ItemClick(object sender, ItemClickEventArgs e)
        {
            EstablishmentListViewItem item = e.ClickedItem as EstablishmentListViewItem;
            if (_selectedItem == null && item != null)//In the beginning no item is selected
            {
                item.OpenMenu();//ask the item to open its menu
                _selectedItem = item;//set selected item
                GeneratePOI(item.ViewModel.Address, item.ViewModel.Name);
                return;
            }
            if (_selectedItem != null && _selectedItem.Equals(item))//item selected, but is the same as previous selected
            {
                //Open or close menu
                OpenItemMenu();
            }
            else{//item selected and its a different one
                _selectedItem = item;
                GeneratePOI(_selectedItem.ViewModel.Address, _selectedItem.ViewModel.Name);
                OpenItemMenu();
            }
        }

        private void OpenItemMenu()
        {
            if (_selectedItem.IsMenuOpen) _selectedItem.CloseMenu();
            else _selectedItem.OpenMenu();
        }
    }
}
