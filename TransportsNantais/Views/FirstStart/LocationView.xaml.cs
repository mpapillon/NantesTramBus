using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace TransportsNantais.Views.FirstStart
{
    public partial class LocationView : PhoneApplicationPage
    {
        public LocationView()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Hidden", false);
            VisualStateManager.GoToState(this, "Oppenning", false);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            var isLocationEnabled = this.LocationCheckBox.IsChecked;

            if (IsolatedStorageSettings.ApplicationSettings.Contains("location_enabled"))
            {
                IsolatedStorageSettings.ApplicationSettings["location_enabled"] = isLocationEnabled;
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings.Add("location_enabled", isLocationEnabled);
            }

            this.NavigationService.Navigate(new Uri("/Views/FirstStart/DataView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Showing", true);
            VisualStateManager.GoToState(this, "Visible", true);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            VisualStateManager.GoToState(this, "Closing", true);
            VisualStateManager.GoToState(this, "Hidden", true);
        }
    }
}