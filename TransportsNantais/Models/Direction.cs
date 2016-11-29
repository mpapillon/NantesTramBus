using TransportsNantais.Enums;
using System.Xml.Serialization;
using SQLite;

namespace TransportsNantais.Models
{
    public class Direction : ObservableModel
    {
        private int _id;
        private DirectionType _directionType;
        private int _lineId;
        private int _lastStopId;

        private Line _line;
        private LastStop _lastStop;

        [XmlAttribute("Id")]
        [PrimaryKey]
        public int DirectionId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public DirectionType DirectionType
        {
            get { return _directionType; }
            set 
            {
                _directionType = value; 
                NotifyPropertyChanged("DirectionType"); 
            }
        }

        public int LineId
        {
            get { return _lineId; }
            set
            {
                _lineId = value;
                NotifyPropertyChanged("LineId");
            }
        }

        public int LastStopId
        {
            get { return _lastStopId; }
            set
            {
                _lastStopId = value;
                NotifyPropertyChanged("LastStopId");
            }
        }

        [XmlIgnore]
        [Ignore]
        public Line Line
        {
            get { return _line; }
            set
            {
                _line = value;
                NotifyPropertyChanged("Line");
            }
        }

        [XmlIgnore]
        [Ignore]
        public LastStop LastStop
        {
            get { return _lastStop; }
            set
            {
                _lastStop = value;
                NotifyPropertyChanged("LastStop");
            }
        }

    }
}
