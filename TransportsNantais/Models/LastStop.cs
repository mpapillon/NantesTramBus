using SQLite;
using System.Xml.Serialization;

namespace TransportsNantais.Models
{
    public class LastStop : ObservableModel
    {
        private int _id;
        private string _name;

        [XmlAttribute("Id")]
        [PrimaryKey]
        public int LastStopId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
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
    }
}
