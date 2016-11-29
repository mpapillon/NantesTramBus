using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TransportsNantais.Bases;
using TransportsNantais.Enums;
using TransportsNantais.Messages;
using TransportsNantais.Models;
using TransportsNantais.Services;
using System.Linq;
using Microsoft.Phone.Controls;
using System;

namespace TransportsNantais.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LineDetailsViewModel : ApplicationViewModelBase
    {
        private IDataBaseService _db;
        private INavigationService _navigationService;

        private Line _line;
        private LastStop _selectedLastStop;
        private ObservableCollection<Stop> _stops;
        private bool _isBusy;
        private string _message;


        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                RaisePropertyChanged("Message");
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
        
        public Line Line
        {
            get { return _line; }
            set
            {
                _line = value;
                RaisePropertyChanged("Line");
            }
        }

        public LastStop SelectedLastStop
        {
            get { return _selectedLastStop; }
            set
            {
                _selectedLastStop = value;
                RaisePropertyChanged("SelectedLastStop");
            }
        }

        public ObservableCollection<Stop> Stops
        {
            get { return _stops; }
            set
            {
                _stops = value;
                RaisePropertyChanged("Stops");
            }
        }

        public RelayCommand<Stop> StopSelectionChangedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the LineDetailsViewModel class.
        /// </summary>
        public LineDetailsViewModel(IDataBaseService dbService, INavigationService navigationService, IApplicationBarService appBarServ)
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Line = new Line()
                {
                    LineId = 1, 
                    NumLigne = "1", 
                    LineType = LineType.Tramway, 
                    OneDirection = "François Mitterand / Jamet",
                    OppositeDirection =  "Beaujoire / Ranzay",
                    FontColor = "FFFEFF", 
                    BackColor = "007944"
                };

                SelectedLastStop = new LastStop() { Name = "François Mitterand / Jamet" };
            }

            _db = dbService;
            _navigationService = navigationService;

            this.StopSelectionChangedCommand = new RelayCommand<Stop>(e => SelectionChanged(e));
        }

        private void SelectionChanged(Stop e)
        {
            if (e == null)
                return;

            IDictionary<string, string> p = new Dictionary<string, string>();
            p.Add("TanId", e.TanId);
            p.Add("StopName", e.Name);

            _navigationService.NavigateTo("/Views/StopDetailsView.xaml", p);
        }

        public override void OnPageNavigatedTo(IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey("LineId"))
            {
                var lineId = int.Parse(parameters["LineId"]);
                this.LoadData(lineId);
            }
        }

        private async void LoadData(int lineId)
        {
            IsBusy = true;
            Message = "Chargement...";

            this.Line = await _db.GetLineAsync(lineId);

            var qStops = await _db.GetStopsForLineAsync(lineId);
            this.Stops = new ObservableCollection<Stop>(qStops);

            IsBusy = false;
        }

        public override void OnNavigatingFrom()
        {
            Line = null;
            SelectedLastStop = null;
            Stops = null;
        }
    }
}