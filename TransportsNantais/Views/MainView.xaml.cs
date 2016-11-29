using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TransportsNantais.Bases;
using GalaSoft.MvvmLight.Messaging;
using TransportsNantais.Messages;
using System.Device.Location;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Collections.ObjectModel;
using TransportsNantais.ViewModels;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using TransportsNantais.Resources;

namespace TransportsNantais.Views
{
    public partial class MainView : ApplicationPageBase
    {
        // Nantes GeoCoordinates
        private GeoCoordinate _defaultCenter = new GeoCoordinate(47.21986, -1.53458);
        MapLayer _myLocationLayer;

        public MainView()
        {
            InitializeComponent();

            this.MainMap.Center = _defaultCenter;
            this.MainMap.ZoomLevel = 10;

            _myLocationLayer = new MapLayer();
            // Add the MapLayer to the Map.
            this.MainMap.Layers.Add(_myLocationLayer);

            Messenger.Default.Register<LocationChangedMessage>(this, (msg) =>
            {
                ChageUserPosition(msg.Latitude, msg.Longitude);
            });

            Messenger.Default.Register<NearStopsLoadedMessage>(this, (msg) =>
            {
                if (msg.IsLoaded)
                {
                    ShowNearStops();
                }
            });
        }

        private void ChageUserPosition(double lat, double lng)
        {
            Dispatcher.BeginInvoke(() =>
            {
                var myGeoCoordinate = new GeoCoordinate(lat, lng);

                // Create a MapOverlay to contain the circle.
                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = CreatePostionCanvas();
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = myGeoCoordinate;

                _myLocationLayer.Clear();
                _myLocationLayer.Add(myLocationOverlay);

                this.MainMap.SetView(myGeoCoordinate, 15);
            });
        }

        /// <summary>
        /// Get the visual point to define the user position.
        /// </summary>
        /// <returns></returns>
        private Canvas CreatePostionCanvas()
        {
            var canvas = new Canvas();
            canvas.Height = 30;
            canvas.Width = 30;

            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            SolidColorBrush whiteBrush = new SolidColorBrush();
            whiteBrush.Color = Colors.White;

            SolidColorBrush accentBrush = new SolidColorBrush();
            accentBrush.Color = (Color)Application.Current.Resources["PhoneAccentColor"];

            var outEllipse = new Ellipse();
            outEllipse.Stroke = blackBrush;
            outEllipse.Fill = whiteBrush;
            outEllipse.StrokeThickness = 4;
            outEllipse.Width = 30;
            outEllipse.Height = 30;

            var inEllipse = new Ellipse();
            inEllipse.Fill = accentBrush;
            inEllipse.StrokeThickness = 4;
            inEllipse.Width = 14;
            inEllipse.Height = 14;
            inEllipse.Margin = new Thickness(8, 8, 0, 0);

            canvas.Children.Add(outEllipse);
            canvas.Children.Add(inEllipse);
            return canvas;
        }

        /// <summary>
        /// Open search view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchAppBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SearchView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Open about view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/AboutView.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Open settings view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SettingsView.xaml", UriKind.Relative));
        }

        private void ShowNearStops()
        {
            Dispatcher.BeginInvoke(async() =>
            {
                var vm = this.DataContext as NearStopsViewModel;
                var db = new TransportsNantais.Services.SQLiteService();

                ObservableCollection<MapItem> coll = new ObservableCollection<MapItem>(); ;

                foreach (var ns in vm.NearStops)
                {
                    var s = await db.GetStopByTanIdAsync(ns.CodeLieu);
                    coll.Add(new MapItem() { Name = s.Name, GeoCoordinate = new GeoCoordinate(s.Latitude, s.Longitude) });
                }

                var itemsColl = MapExtensions.GetChildren(MainMap).OfType<MapItemsControl>().First();

                if (itemsColl.Items.Count > 0)
                    itemsColl.Items.Clear();

                foreach (var c in coll)
                {
                    itemsColl.Items.Add(c);
                }
            });
        }

        private async void ApplicationPageBase_Loaded(object sender, RoutedEventArgs e)
        {
            var locationConsent = IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent");

            if (locationConsent == false)
            {
                await AuthorizePosition();

                var vm = this.DataContext as NearStopsViewModel;
                vm.ForceTrackPositionCommand.Execute(null);
            }
        }

        private static async System.Threading.Tasks.Task AuthorizePosition()
        {
            HyperlinkButton hyperlinkButton = new HyperlinkButton()
            {
                Content = "Déclaration de confidentialité",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 30),
                NavigateUri = new Uri("https://www.tan.fr/jsp/fiche_pagelibre.jsp?CODE=21852478&LANGUE=0&RH=ACCUEIL&RF=1373631635261", UriKind.Absolute)
            };

            TiltEffect.SetSuppressTilt(hyperlinkButton, true);

            CustomMessageBox messageBox = new CustomMessageBox()
            {
                Caption = "Autorisez-vous l'application à accèder à votre position ?",
                Message = "Nantes Tram & Bus utilise votre position géographique pour déterminer les arrêts à proximité de vous.\n\n"
                        + "Cependant, vos coordonnées géographique seront envoyées à la SEMITAN afin de faire fonctionner le service.",
                Content = hyperlinkButton,
                LeftButtonContent = "autoriser",
                RightButtonContent = "annuler",
                IsFullScreen = false
            };

            switch (await messageBox.ShowAsync())
            {
                case CustomMessageBoxResult.LeftButton:
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
                    break;

                case CustomMessageBoxResult.RightButton:
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                    break;

                case CustomMessageBoxResult.None:
                    IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                    break;
            }
        }

        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.ApplicationId = AppResources.ApplicationId;
            Microsoft.Phone.Maps.MapsSettings.ApplicationContext.AuthenticationToken = AppResources.AuthenticationToken;
        }
    }

    internal class MapItem
    {
        public GeoCoordinate GeoCoordinate { get; set; }
        public string Name { get; set; }
    }
}