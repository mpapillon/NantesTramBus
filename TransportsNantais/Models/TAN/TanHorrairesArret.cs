using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsNantais.Models.TAN
{

    public class TanHorrairesArret
    {
        public TanArret Arret { get; set; }
        public TanLigne Ligne { get; set; }
        public string CodeCouleur { get; set; }
        public string PlageDeService { get; set; }
        public TanNote[] Notes { get; set; }
        public TanHorraire[] Horaires { get; set; }
        public TanHorraire[] ProchainsHoraires { get; set; }
    }

    public class TanNote
    {
        public string Code { get; set; }
        public string Libelle { get; set; }
    }

    public class TanHorraire
    {
        public string Heure { get; set; }
        public string[] Passages { get; set; }
    }

}
