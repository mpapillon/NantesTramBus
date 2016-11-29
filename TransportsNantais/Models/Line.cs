using TransportsNantais.Enums;
using System.Xml.Serialization;
using SQLite;

namespace TransportsNantais.Models
{
    public class Line : ObservableModel
    {
        private int _id;
        private string _numLigne;
        private string _backColor;
        private string _fontColor;
        private LineType _lineType;
        private string _oneDirection;
        private string _oppositeDirection;

        [XmlAttribute("Id")]
        [PrimaryKey]
        public int LineId
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        [MaxLength(4), Indexed]
        public string NumLigne
        {
            get { return _numLigne; }
            set 
            { 
                _numLigne = value;
                NotifyPropertyChanged("NumLigne");
            }
        }

        [MaxLength(6)]
        public string BackColor
        {
            get { return _backColor; }
            set 
            {
                _backColor = value;
                NotifyPropertyChanged("BackColor");
            }
        }

        [MaxLength(6)]
        public string FontColor
        {
            get { return _fontColor; }
            set 
            {
                _fontColor = value;
                NotifyPropertyChanged("FontColor");
            }
        }

        public LineType LineType
        {
            get { return _lineType; }
            set 
            {
                _lineType = value;
                NotifyPropertyChanged("LineType");
            }
        }

        public string OneDirection
        {
            get { return _oneDirection; }
            set 
            {
                _oneDirection = value;
                NotifyPropertyChanged("OneDirection");
            }
        }

        public string OppositeDirection
        {
            get { return _oppositeDirection; }
            set 
            {
                _oppositeDirection = value;
                NotifyPropertyChanged("OppositeDirection");
            }
        }
    }
}
