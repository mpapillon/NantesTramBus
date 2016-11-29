using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportsNantais.Models.TAN;
using TransportsNantais.Enums;

namespace TransportsNantais.Services.Design
{
    public class TanRestServiceDesign : ITanRestService
    {
        public Task<TanArret[]> GetStopFromGeoAsync(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }

        public Task<TanDepart[]> GetWaitTimeForStopAsync(string stopId)
        {
            throw new NotImplementedException();
        }


        public TanArret[] GetStopFromGeo(double latitude, double longitude)
        {
            return new TanArret[] 
            {
                new TanArret() { 
                    Ligne = new TanLigne[] { new TanLigne() { NumLigne = "11"}, new TanLigne() { NumLigne = "70"} }, 
                    Accessible = true, 
                    CodeArret = "CVTI", 
                    Distance = "83m", 
                    Libelle = "Convention"
                },
                new TanArret() 
                { 
                    Ligne = new TanLigne[] { new TanLigne() { NumLigne = "1"}, new TanLigne() { NumLigne = "11"}, new TanLigne() { NumLigne = "70"} },
                    Accessible = true, 
                    CodeArret = "EGLI", 
                    Distance = "84m", 
                    Libelle = "Egalité"
                }
            };
        }

        public TanDepart[] GetWaitTimeForStop(string stopId)
        {
            return new TanDepart[] 
            {
                new TanDepart() 
                { 
                    Arret = new TanArret() { 
                        Ligne = new TanLigne[] { new TanLigne() { NumLigne = "11"}, new TanLigne() { NumLigne = "70"} }, 
                        Accessible = true, 
                        CodeArret = "EGLI", 
                        Distance = "83m", 
                        Libelle = "Egalité"
                    },
                    Ligne = new TanLigne() { NumLigne = "1"},
                    Sens = 1,
                    Terminus = "François Mitterand",
                    Temps = "Proche"
                },
                new TanDepart() 
                { 
                    Arret = new TanArret() { 
                        Ligne = new TanLigne[] { new TanLigne() { NumLigne = "11"}, new TanLigne() { NumLigne = "70"} }, 
                        Accessible = true, 
                        CodeArret = "EGLI", 
                        Distance = "83m", 
                        Libelle = "Egalité"
                    },
                    Ligne = new TanLigne() { NumLigne = "11"},
                    Sens = 2,
                    Terminus = "Tertre",
                    Temps = "5 mins"
                }
            };
        }
    }
}
