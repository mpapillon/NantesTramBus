using SQLite;
using System.Xml.Serialization;

namespace TransportsNantais.Models
{
    public class LineStop : ObservableModel
    {
        private int _id;
        private int _directionId;
        private int _subStopId;

        private Direction _direction;
        private SubStop _subStop;

        [XmlAttribute("Id")]
        [PrimaryKey]
        public int LineStopId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public int DirectionId
        {
            get { return _directionId; }
            set
            {
                _directionId = value;
                NotifyPropertyChanged("DirectionId");
            }
        }

        public int SubStopId
        {
            get { return _subStopId; }
            set
            {
                _subStopId = value;
                NotifyPropertyChanged("SubStopId");
            }
        }

        [XmlIgnore]
        [Ignore]
        public Direction Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
                NotifyPropertyChanged("Direction");
            }
        }

        [XmlIgnore]
        [Ignore]
        public SubStop SubStop
        {
            get { return _subStop; }
            set
            {
                _subStop = value;
                NotifyPropertyChanged("SubStop");
            }
        }
    }
}
