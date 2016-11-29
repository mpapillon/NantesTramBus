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
using GalaSoft.MvvmLight.Messaging;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;

namespace TransportsNantais.Views
{
    public partial class StopDetailsView : ApplicationPageBase
    {
        private ApplicationBarIconButton _pinAppBtn;
        private MapLayer pushPinLayer;

        public StopDetailsView()
        {
            InitializeComponent();
            pushPinLayer = new MapLayer();
            this.MainMap.Layers.Add(pushPinLayer);

            Messenger.Default.Register<GenericMessage<GeoCoordinate>>(this, (msg) =>
            {
                if (msg.Sender == this.DataContext)
                {
                    pushPinLayer.Clear();

                    this.MainMap.Center = msg.Content;
                    this.MainMap.ZoomLevel = 15;

                    Pushpin stop = new Pushpin();
                    stop.GeoCoordinate = msg.Content;

                    var itemsColl = MapExtensions.GetChildren(MainMap).OfType<MapItemsControl>().First();
                    itemsColl.Items.Clear();
                    itemsColl.Items.Add(stop);
                }
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            CreatePinBtn();
        }

        private void CreatePinBtn()
        {
            _pinAppBtn = new ApplicationBarIconButton();

            var dc = (TransportsNantais.ViewModels.StopDetailsViewModel)this.DataContext;
            string tileName = "TanId=" + dc.TanId;

            // check if secondary tile is already made and pinned
            ShellTile Tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(tileName));

            if (Tile == null)
            {
                _pinAppBtn.IconUri = new Uri("/Assets/AppBar/appbar.pin.png", UriKind.Relative);
                _pinAppBtn.Text = "épingler";
            }
            else
            {
                _pinAppBtn.IconUri = new Uri("/Assets/AppBar/appbar.pin.remove.png", UriKind.Relative);
                _pinAppBtn.Text = "supprimer";
            }

            this.ApplicationBar.Buttons.Add(_pinAppBtn);
            _pinAppBtn.Click += new EventHandler(PinAppBtn_Click);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PinAppBtn_Click(object sender, EventArgs e)
        {
            var dc = (TransportsNantais.ViewModels.StopDetailsViewModel)this.DataContext;
            string tileName = "TanId=" + dc.TanId;

            // check if secondary tile is already made and pinned
            ShellTile Tile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(tileName));

            if (Tile == null)
            {
                // create a new secondary tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.Title = dc.StopName;
                data.BackgroundImage = new Uri("/Assets/info_trafic.png", UriKind.Relative);
                // create a new tile for this Second Page
                string tilePath = String.Format("/Views/StopDetailsView.xaml?TanId={0}&StopName={1}", 
                    dc.TanId, 
                    Uri.EscapeUriString(dc.StopName));

                ShellTile.Create(new Uri(tilePath, UriKind.Relative), data);
            }
            else
            {
                Tile.Delete();
            }

            this.ChangePinAppBtn();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangePinAppBtn()
        {
            if (_pinAppBtn.Text == "épingler")
            {
                _pinAppBtn.IconUri = new Uri("/Assets/AppBar/appbar.pin.remove.png", UriKind.Relative);
                _pinAppBtn.Text = "supprimer";
            }
            else
            {
                _pinAppBtn.IconUri = new Uri("/Assets/AppBar/appbar.pin.png", UriKind.Relative);
                _pinAppBtn.Text = "épingler";
            }
        }
    }
}