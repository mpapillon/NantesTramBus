using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Models
{
    public class SubStop : ObservableModel
    {
        private int _id;

        [PrimaryKey]
        public int SubStopId
        {
            get { return _id; }
            set 
            { 
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string _tanId;

        [MaxLength(5), Indexed]
        public string TanId
        {
            get { return _tanId; }
            set 
            { 
                _tanId = value; 
                NotifyPropertyChanged("TanId"); 
            }
        }

        private string _name;

        [MaxLength(150)]
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                NotifyPropertyChanged("Name"); 
            }
        }

        private int _parentId;

        public int ParentId
        {
            get { return _parentId; }
            set 
            {
                _parentId = value;
                NotifyPropertyChanged("ParentId"); 
            }
        }

        private double _latitude;

        public double Latitude
        {
            get { return _latitude; }
            set 
            {
                _latitude = value;
                NotifyPropertyChanged("Latitude"); 
            }
        }

        private double _logitude;

        public double Longitude
        {
            get { return _logitude; }
            set 
            {
                _logitude = value;
                NotifyPropertyChanged("Longitude"); 
            }
        }

    }
}
