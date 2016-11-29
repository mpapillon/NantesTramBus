using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Models.TAN
{
    public class TanArret
    {
        public string CodeArret { get; set; }
        public string CodeLieu { get; set; }
        public string Libelle { get; set; }
        public string Distance { get; set; }
        public bool Accessible { get; set; }
        public TanLigne[] Ligne { get; set; }
    }
}
