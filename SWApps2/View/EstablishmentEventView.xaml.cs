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
    public sealed partial class EstablishmentEventView : Page
    {
        public EstablishmentEventViewModel Event { get; set; }
        private INavigation _navigator;
        public EstablishmentEventView()
        {
            Event = new EstablishmentEventViewModel();
            DataContext = Event;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigator = (e.Parameter as dynamic)?.Navigator as INavigation;
            Event.Event = (e.Parameter as dynamic)?.Parameter as EstablishmentEvent;
            base.OnNavigatedTo(e);
        }

        public void GoToEstablishment(object sender, RoutedEventArgs e)
        {
            this._navigator.Navigate("Establishment", new { Navigator = this._navigator, Parameter = this.Event.Event.Establishment });
        }
    }
}
