using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using TransportsNantais.Enums;
using TransportsNantais.Messages;
using TransportsNantais.Models;
using TransportsNantais.Services;
using System.Collections.Generic;
using TransportsNantais.Bases;

namespace TransportsNantais.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SearchViewModel : ApplicationViewModelBase
    {
        private IDataBaseService _db;
        private INavigationService _navigationService;

        private bool _isBusy;
        private string _message;
        private ObservableCollection<Stop> _stops;
        private Stop _selecteditem;

        public Stop SelectedItem
        {
            get { return _selecteditem; }
            set 
            { 
                _selecteditem = value;
                RaisePropertyChanged("SelectedItem");
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


        public RelayCommand<string> SearchCommand { get; private set; }
        public RelayCommand SelectionChangedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the SearchViewModel class.
        /// </summary>
        public SearchViewModel(IDataBaseService dbService, INavigationService navigationService)
        {
            _db = dbService;
            _navigationService = navigationService;

            this.SearchCommand = new RelayCommand<string>(StartSearch);
            this.SelectionChangedCommand = new RelayCommand(SelectionChanged);
        }

        private async void StartSearch(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return;

            this.IsBusy = true;
            this.Message = "Recherche...";

            var s = await _db.GetStopsByNameAsync(value);
            s.OrderBy(v => v.Name);
            this.Stops = new ObservableCollection<Stop>(s);

            this.IsBusy = false;
            this.Message = "";
        }

        public void SelectionChanged()
        {
            if (this.SelectedItem == null)
                return;            

            IDictionary<string, string> p = new Dictionary<string, string>();
            p.Add("TanId", SelectedItem.TanId);
            p.Add("StopName", SelectedItem.Name);

            // Clean the selection
            this.SelectedItem = null;

            _navigationService.NavigateTo("/Views/StopDetailsView.xaml", p);
        }
    }
}