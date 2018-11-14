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
using SWApps2.Data;
using SWApps2.Model;
using SWApps2.View;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SWApps2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Frame _pageWrapper;
        public MainPage()
        {
            this.InitializeComponent();
            this._pageWrapper = this.FindName("PageWrapper") as Frame;

            FakeDataService fakenews = new FakeDataService();
            List<Establishment> establishments = fakenews.Establishments;
        }

        private void Establisments_Page(object sender, RoutedEventArgs e)
        {
            this._pageWrapper.Navigate(typeof(EstablishmentListView));
        }
    }
}
