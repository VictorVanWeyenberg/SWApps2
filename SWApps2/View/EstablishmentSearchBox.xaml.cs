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



namespace SWApps2.View
{
    /// <summary>
    /// This User Control serves as a search box to look up establishments
    /// </summary>
    public sealed partial class EstablishmentSearchBox : UserControl
    {
        public EstablishmentSearchBox()
        {
            InitializeComponent();
            
            ///Get the resourceLoader to grab strings from
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            //Set placeholder text from resource
            inputBox.PlaceholderText = resourceLoader.GetString("EstablishSearchPlaceholder");
        }
    }
}
