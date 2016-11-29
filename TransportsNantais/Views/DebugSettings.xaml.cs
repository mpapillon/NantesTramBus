using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using System.IO.IsolatedStorage;

namespace TransportsNantais.Views
{
    public partial class DebugSettings : PhoneApplicationPage
    {
        public DebugSettings()
        {
            InitializeComponent();

            if (IsolatedStorageSettings.ApplicationSettings.Contains("DEBUG:FakeLocation") 
                && ((bool)IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLocation"] == true))
            {
                this.FakeGPSBtn.IsChecked = true;
                this.LatitudeTbx.Text = IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLat"].ToString();
                this.LongitudeTbx.Text = IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLng"].ToString();
                this.Map.Center = new GeoCoordinate 
                { 
                    Latitude = (double)IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLat"], 
                    Longitude = (double)IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLng"] 
                };
                this.Map.ZoomLevel = 15;
            }
        }

        private void FakeGPSBtn_Checked(object sender, RoutedEventArgs e)
        {
            this.LatitudeTbx.IsEnabled = true;
            this.LongitudeTbx.IsEnabled = true;
            IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLocation"] = true;
        }

        private void FakeGPSBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            this.LatitudeTbx.IsEnabled = false;
            this.LongitudeTbx.IsEnabled = false;
            IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLocation"] = false;
        }

        private void Map_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            GeoCoordinate geo = this.Map.ConvertViewportPointToGeoCoordinate(e.GetPosition(this.Map));

            if (this.FakeGPSBtn.IsChecked.Value)
            {
                this.LatitudeTbx.Text = geo.Latitude.ToString();
                IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLat"] = geo.Latitude;
                this.LongitudeTbx.Text = geo.Longitude.ToString();
                IsolatedStorageSettings.ApplicationSettings["DEBUG:FakeLng"] = geo.Longitude;
            }
        }
    }
}