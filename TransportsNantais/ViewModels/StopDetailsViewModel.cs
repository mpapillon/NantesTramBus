using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using TransportsNantais.Models;
using TransportsNantais.Messages;
using TransportsNantais.Services;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using TransportsNantais.Models.TAN;
using System.Collections.Generic;
using TransportsNantais.Bases;
using System.Device.Location;
using GalaSoft.MvvmLight.Messaging;

namespace TransportsNantais.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StopDetailsViewModel : ApplicationViewModelBase
    {
        private ITanRestService _restService;
        private IDataBaseService _dbService;
        private INavigationService _navigationService;

        private string _stopName;
        private string _tanId;
        private bool _isBusy;
        private string _message;
        private ObservableCollection<Line> _lines;
        private ObservableCollection<TanDepart> _nextDepartures;
        private Line _selectedLine;
        private Stop _stop;
        private GeoCoordinate _geoCoordinate = new GeoCoordinate();

        public GeoCoordinate GeoCoordinate
        {
            get { return _geoCoordinate; }
            set
            {
                _geoCoordinate = value;
                RaisePropertyChanged("GeoCoordinate");
            }
        }

        public Stop Stop
        {
            get { return _stop; }
            set
            {
                _stop = value;
                RaisePropertyChanged("Stop");
            }
        }

        /// <summary>
        /// Get or set the next departures
        /// </summary>
        public ObservableCollection<TanDepart> NextDepartures
        {
            get { return _nextDepartures; }
            set
            {
                _nextDepartures = value;
                RaisePropertyChanged("NextDepartures");
            }
        }

        public Line SelectedLine
        {
            get { return _selectedLine; }
            set
            {
                _selectedLine = value;
                RaisePropertyChanged("SelectedLine");
            }
        }

        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set 
            { 
                _lines = value;
                RaisePropertyChanged("Lines");
            }
        }

        public string StopName
        {
            get { return _stopName; }
            set 
            { 
                _stopName = value;
                RaisePropertyChanged("StopName");
            }
        }

        public string TanId
        {
            get { return _tanId; }
            set
            {
                _tanId = value;
                RaisePropertyChanged("TanId");
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

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }


        public RelayCommand<Line> LineSelectionChangedCommand { get; private set; }
        public RelayCommand GetNextDeparturesCommand { get; private set; }


        /// <summary>
        /// Initializes a new instance of the StopDetailsViewModel class.
        /// </summary>
        public StopDetailsViewModel(IDataBaseService dbService, ITanRestService restService, 
            INavigationService navigationService)
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                StopName = "Egalité";
                TanId = "EGLI";
            }

            _dbService = dbService;
            _restService = restService;
            _navigationService = navigationService;

            this.LineSelectionChangedCommand = new RelayCommand<Line>(e => SelectionChanged(e));
            this.GetNextDeparturesCommand = new RelayCommand(GetNextDepartures);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SelectionChanged(Line line)
        {
            if (line == null)
                return;

            IDictionary<string, string> p = new Dictionary<string, string>();
            p.Add("LineId", line.LineId.ToString());

            // Clean the selection
            this.SelectedLine = null;

            _navigationService.NavigateTo("/Views/LineDetailsView.xaml", p);
        }

        public override void OnPageNavigatedTo(IDictionary<string, string> parameters)
        {
            this.Lines = new ObservableCollection<Line>();
            this.NextDepartures = new ObservableCollection<TanDepart>();

            if (parameters.ContainsKey("TanId"))
            {
                TanId = parameters["TanId"];

                this.GetLines();
                this.GetNextDepartures();
            }

            if (parameters.ContainsKey("StopName"))
            {
                StopName = parameters["StopName"];
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        private async void GetLines()
        {
            this.Stop = await _dbService.GetStopByTanIdAsync(_tanId);
            var lines = await _dbService.GetLinesForStopAsync(Stop.StopId);

            this.GeoCoordinate = new GeoCoordinate(Stop.Latitude, Stop.Longitude);
            MessengerInstance.Send<GenericMessage<GeoCoordinate>>(new GenericMessage<GeoCoordinate>(this, GeoCoordinate));

            this.Lines = new ObservableCollection<Line>(lines.OrderBy(l => l.LineId).ToList());
        }

        /// <summary>
        /// Get next departures from the WebService
        /// </summary>
        private async void GetNextDepartures()
        {
            this.IsBusy = true;
            this.Message = "Chargement...";

            var departures = await _restService.GetWaitTimeForStopAsync(TanId);
            this.NextDepartures = new ObservableCollection<TanDepart>(departures);

            this.IsBusy = false;
        }
    }
}