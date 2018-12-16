using System;
using SWApps2.Model;
using SWApps2.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SWApps2.CustomControls
{
    public sealed partial class EstablishmentListViewItem : UserControl
    {
        public INavigation Navigator { get;  set; }
        public EstablishmentListViewItemViewModel ViewModel { get; set; }

        public bool IsMenuOpen { get { return ViewModel.IsMenuOpen; } }
        public EstablishmentListViewItem()
        {
            InitializeComponent();
        }

        private void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Establishment == null) return;
            var app = Application.Current as App;
            var user = app.User;
            if (user == null || user is User)
            {
                app.EstablishmentFromList = ViewModel.Establishment;
                Navigator.Navigate("Establishment", new { Navigator = Navigator});
            }
        }

        private void SubscribeBtn_Click(object sender, RoutedEventArgs e)
        {
            //Subscribe
        }

        public void CloseMenu()
        {
            StackPanel sp = FindName("Menu") as StackPanel;
            if (ViewModel.IsMenuOpen)
            {
                sp.Visibility = Visibility.Collapsed;
                ViewModel.IsMenuOpen = false;
            }
        }

        public void OpenMenu()
        {
            if (!ViewModel.CanShowMenu) return;
            StackPanel sp = FindName("Menu") as StackPanel;
            if (!ViewModel.IsMenuOpen)
            {
                sp.Visibility = Visibility.Visible;
                ViewModel.IsMenuOpen = true;
            }
        }
    }
}
