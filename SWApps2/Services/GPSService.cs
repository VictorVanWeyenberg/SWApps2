using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace SWApps2.Services
{
    public static class GPSService
    {
        public static readonly Geopoint CenterOfMap = new Geopoint(new BasicGeoposition() { Latitude = 51.0543, Longitude = 3.7174 });

        public static async Task<MapLocationFinderResult> FindLocationForAddress(string address)
        {
            return await MapLocationFinder.FindLocationsAsync(address, CenterOfMap);
        }

    }
}
