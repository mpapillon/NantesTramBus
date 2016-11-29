using GalaSoft.MvvmLight;
using TransportsNantais.Services;
using TransportsNantais.Models.TAN;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Windows.Devices.Geolocation;
using System;
using TransportsNantais.Messages;
using System.Collections.Generic;
using TransportsNantais.Bases;
using System.IO.IsolatedStorage;
using GalaSoft.MvvmLight.Threading;

namespace TransportsNantais.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class NearStopsViewModel : ApplicationViewModelBase
    {
        private ITanRestService _restService;
        private INavigationService _navigationService;

        private Geolocator _geolocator = null;
        private bool _tracking = false;
        private bool _hasData = false;

        private double _latitude;
        private double _longitude;

        public double ActualLatitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisePropertyChanged("ActualLatitude");
            }
        }

        public double ActualLongitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisePropertyChanged("ActualLongitude");
            }
        }

        private bool _hasMessage;
        private bool _isBusy;
        private ObservableCollection<TanArret> _nearStops;
        private TanArret _selectedStop;
        private string _msg;
        private string _busyMsg;

        public string Message
        {
            get { return _msg; }
            set 
            { 
                _msg = value;
                RaisePropertyChanged("Message");
            }
        }

        public string BusyMessage
        {
            get { return _busyMsg; }
            set
            {
                _busyMsg = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        public TanArret SelectedStop
        {
            get { return _selectedStop; }
            set 
            { 
                _selectedStop = value;
                RaisePropertyChanged("SelectedStop"); 
            }
        }

        public ObservableCollection<TanArret> NearStops
        {
            get { return _nearStops; }
            set 
            { 
                _nearStops = value;
                RaisePropertyChanged("NearStops"); 
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set 
            { 
                _isBusy = value; 
                RaisePropertyChanged("IsBusy"); 
            }
        }

        public bool HasMessage
        {
            get { return _hasMessage; }
            set
            {
                _hasMessage = value;
                RaisePropertyChanged("HasMessage");
            }
        }

        #region RelayCommands

        public RelayCommand SelectionChangedCommand { get; private set; }
        public RelayCommand ForceTrackPositionCommand { get; private set; }

        #endregion


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public NearStopsViewModel(ITanRestService restService, INavigationService navigationService)
        {
            _restService = restService;
            _navigationService = navigationService;

            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.NearStops = new ObservableCollection<TanArret>(restService.GetStopFromGeo(0,0));
            }

            this.SelectionChangedCommand = new RelayCommand(SelectionChanged);
            this.ForceTrackPositionCommand = new RelayCommand(TraskPosition);
        }

        public override void OnPageNavigatedTo(IDictionary<string, string> parameters)
        {
            this.TraskPosition();
        }

        public override void OnNavigatingFrom()
        {
            _geolocator = null;
            _tracking = false;
        }

        public void SelectionChanged()
        {
            if (this.SelectedStop == null)
                return;

            IDictionary<string, string> p = new Dictionary<string, string>();
            p.Add("TanId", _selectedStop.CodeLieu);
            p.Add("StopName", _selectedStop.Libelle);

            // Clean the selection
            this.SelectedStop = null;

            _navigationService.NavigateTo("/Views/StopDetailsView.xaml", p);
        }

        private void TraskPosition()
        {
            var hasConsent = IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent");

            if (!hasConsent)
                return;

            var locationConsent = (bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"];

            if (locationConsent)
            {
                this.HasMessage = false;
                if (!_tracking)
                {
                    _geolocator = new Geolocator();
                    _geolocator.DesiredAccuracy = PositionAccuracy.High;
                    _geolocator.MovementThreshold = 300; // The units are meters.

                    _geolocator.StatusChanged += geolocator_StatusChanged;
                    _geolocator.PositionChanged += geolocator_PositionChanged;

                    _tracking = true;
                }
            }
            else
            {
                _geolocator = null;
                _tracking = false;

                this.HasMessage = true;
                this.Message = "Activez la localisation dans les paramètres de l'application pour utiliser cette fonction.";
            }
        }

        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => { 
                switch (args.Status)
                {
                    case PositionStatus.Disabled:
                        // the application does not have the right capability or the location master switch is off
                        this.HasMessage = true;
                        this.Message = "Activez la localisation dans les paramètres du téléphone pour utiliser cette fonction.";
                        break;
                    case PositionStatus.Initializing:
                        // the geolocator started the tracking operation
                        if (!_hasData)
                        {
                            this.IsBusy = true;
                            this.BusyMessage = "Chargement...";
                        }
                        break;
                    case PositionStatus.NoData:
                        // the location service was not able to acquire the location
                        break;
                    case PositionStatus.Ready:
                        // the location service is generating geopositions as specified by the tracking parameters
                        break;
                    case PositionStatus.NotInitialized:
                        // the initial state of the geolocator, once the tracking operation is stopped by the user the geolocator moves back to this state
                        break;
                }
            });
        }

        private async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (!_hasData)
                {
                    this.IsBusy = true;
                    this.BusyMessage = "Chargement...";
                }
            });

            this.ActualLatitude = args.Position.Coordinate.Latitude;
            this.ActualLongitude = args.Position.Coordinate.Longitude;

            this.SendLocationChangedMessage();

            System.Diagnostics.Debug.WriteLine("Latitude : " + args.Position.Coordinate.Latitude);
            System.Diagnostics.Debug.WriteLine("Longitude : " + args.Position.Coordinate.Longitude);

            var geo = args.Position.Coordinate.ToGeoCoordinate();

            TanArret[] nearStops = await _restService.GetStopFromGeoAsync(geo.Latitude, geo.Longitude);

            DispatcherHelper.CheckBeginInvokeOnUI(() => 
            { 
                this.NearStops = new ObservableCollection<TanArret>(nearStops);
            });

            _hasData = true;
            SendNearStopsLoadedMessage();

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                this.IsBusy = false;
            });
        }

        /// <summary>
        /// Send the new location to others ViewModels.
        /// </summary>
        private void SendLocationChangedMessage()
        {
            MessengerInstance.Send<LocationChangedMessage>(new LocationChangedMessage() 
            {
                Latitude = _latitude,
                Longitude = _longitude
            });
        }

        private void SendNearStopsLoadedMessage()
        {
            MessengerInstance.Send<NearStopsLoadedMessage>(new NearStopsLoadedMessage()
            {
                IsLoaded = true
            });
        }
    }
}