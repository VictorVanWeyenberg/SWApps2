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
    public sealed partial class EstablishmentView : Page
    {
        public EstablishmentViewModel Establishment { get; set; }
        private NavigationPage _navigator;
        public EstablishmentView()
        {
            this.DataContextChanged += (s, e) =>
            {
                Establishment = DataContext as EstablishmentViewModel;
            };
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._navigator = (e.Parameter as dynamic).Navigator;
            this.Establishment.Establishment = (e.Parameter as dynamic).Parameter;
        }
    }
}
