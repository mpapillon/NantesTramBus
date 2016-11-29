using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TransportsNantais.Models;
using TransportsNantais.Models.TAN;
using TransportsNantais.Enums;
using TransportsNantais.Messages;
using TransportsNantais.Services;
using TransportsNantais.Bases;

namespace TransportsNantais.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LinesViewModel : ApplicationViewModelBase
    {
        private IDataBaseService _dbService;
        private INavigationService _navigationService;

        private bool _isDataLoaded;

        private ObservableCollection<Line> _lines;
        private Line _selectedItem;
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

        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set 
            { 
                _lines = value;
                RaisePropertyChanged("Lines");
            }
        }

        public Line SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        #region RelayCommand

        public RelayCommand LoadLinesCommand { get; private set; }
        public RelayCommand SelectionChangedCommand { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the LinesViewModel class.
        /// </summary>
        public LinesViewModel(IDataBaseService dbService, INavigationService navigationService)
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                this.Lines = new ObservableCollection<Line>(new List<Line>()
                {
                    new Line() 
                    { 
                        LineId = 1, 
                        NumLigne = "1", 
                        LineType = LineType.Tramway, 
                        OneDirection = "François Mitterand / Jamet",
                        OppositeDirection = "Beaujoire / Ranzay",
                        FontColor = "FFFEFF", 
                        BackColor = "007944" 
                    },
                    new Line() 
                    { 
                        LineId = 2, 
                        NumLigne = "11", 
                        LineType = LineType.Bus, 
                        OneDirection = "Tertre",
                        OppositeDirection = "Perray",
                        FontColor = "E7C389", 
                        BackColor = "1A171B" 
                    }
                });
            }

            _dbService = dbService;
            _navigationService = navigationService;

            this.LoadLinesCommand = new RelayCommand(LoadLines);
            this.SelectionChangedCommand = new RelayCommand(SelectionChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void LoadLines()
        {
            if (_isDataLoaded)
                return;

            this.IsBusy = true;
            this.Message = "Chargement...";

            var lines = await _dbService.GetLinesAsync();
            this.Lines = new ObservableCollection<Line>(lines);

            this.IsBusy = false;
            this.Message = "";
            _isDataLoaded = true;
        }

        private void SelectionChanged()
        {
            if (this.SelectedItem == null)
                return;

            IDictionary<string, string> p = new Dictionary<string, string>();
            p.Add("LineId", _selectedItem.LineId.ToString());

            // Clean the selection
            this.SelectedItem = null;

            _navigationService.NavigateTo("/Views/LineDetailsView.xaml", p);
        }
    }
}