using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TransportsNantais.Bases;
using System.IO.IsolatedStorage;


namespace TransportsNantais.Views
{
    public partial class SettingsView : ApplicationPageBase
    {
        public SettingsView()
        {
            InitializeComponent();

            var hasLocationConsent = IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent");

            if (hasLocationConsent)
            {
                this.LocationConsent.IsChecked = (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
            }
        }

        private void LocationConsent_Checked(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
        }

        private void LocationConsent_Unchecked(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
        }
    }
}