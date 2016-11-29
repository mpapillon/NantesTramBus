using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Models.TAN
{
    public class TanLigne
    {
        public string NumLigne { get; set; }
        public int TypeLigne { get; set; }
        public string DirectionSens1 { get; set; }
        public string DirectionSens2 { get; set; }
        public bool Accessible { get; set; }
        public int EtatTrafic { get; set; }
        public string LibelleTrafic { get; set; }
    }
}
