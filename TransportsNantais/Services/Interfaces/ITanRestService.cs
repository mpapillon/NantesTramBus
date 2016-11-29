using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportsNantais.Models.TAN;

namespace TransportsNantais.Services
{
    public interface ITanRestService
    {
        Task<TanArret[]> GetStopFromGeoAsync(double latitude, double longitude);
        TanArret[] GetStopFromGeo(double latitude, double longitude);

        Task<TanDepart[]> GetWaitTimeForStopAsync(string stopId);
        TanDepart[] GetWaitTimeForStop(string stopId);
    }
}
