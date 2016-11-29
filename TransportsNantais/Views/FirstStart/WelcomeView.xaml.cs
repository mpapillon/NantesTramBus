using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TransportsNantais.Views.FirstStart
{
    public partial class WelcomeView : PhoneApplicationPage
    {
        public WelcomeView()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Hidden", false);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var contains = IsolatedStorageSettings.ApplicationSettings.Contains("firstStart");

            if (contains)
            {
                var value = (bool)IsolatedStorageSettings.ApplicationSettings["firstStart"];

                if (value == false)
                {
                    this.NavigationService.Navigate(new Uri("/Views/MainView.xaml", UriKind.Relative));
                }
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/FirstStart/LocationView.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Visible", true);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            VisualStateManager.GoToState(this, "Hidden", true);
        }
    }
}