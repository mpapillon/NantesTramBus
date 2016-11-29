using SQLite;
using System.Xml.Serialization;

namespace TransportsNantais.Models
{
    public class Stop : ObservableModel
    {
        private int _id;
        private string _tanId;
        private string _name;
        private double _latitude;
        private double _longitude;

        [XmlAttribute("Id")]
        [PrimaryKey]
        public int StopId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

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

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }
    }
}
