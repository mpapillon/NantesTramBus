using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Models.TAN
{
    public class TanDepart
    {
        public int Sens { get; set; }
        public string Terminus { get; set; }
        public bool Infotrafic { get; set; }
        public string Temps { get; set; }
        public TanLigne Ligne { get; set; }
        public TanArret Arret { get; set; }
    }
}
