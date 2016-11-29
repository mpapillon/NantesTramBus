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
    public partial class DataView : BasePage
    {
        public DataView()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Hidden", false);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Visible", true);
            this.FeedbackAni.Begin();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings.Add("firstStart", false);
            this.NavigationService.Navigate(new Uri("/Views/MainView.xaml", UriKind.Relative));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            VisualStateManager.GoToState(this, "Hidden", true);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            while (this.NavigationService.BackStack.Any())
            {
                this.NavigationService.RemoveBackEntry();
            }
        }
    }
}