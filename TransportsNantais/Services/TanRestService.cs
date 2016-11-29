using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TransportsNantais.Models.TAN;
using TransportsNantais.Resources;

namespace TransportsNantais.Services
{
    public class TanRestService : ITanRestService
    {
        public async Task<TanArret[]> GetStopFromGeoAsync(double latitude, double longitude)
        {
            string latitudeStr = latitude.ToString().Replace('.', ',');
            string longitudeStr = longitude.ToString().Replace('.', ',');

            string url = String.Format(TanAdresses.ArretsByGeo, latitudeStr, longitudeStr);

            string a = await this.GetDataAsync(url);
            TanArret[] arrets = JsonConvert.DeserializeObject<TanArret[]>(a);

            return arrets;
        }

        public TanArret[] GetStopFromGeo(double latitude, double longitude)
        {
            string latitudeStr = latitude.ToString().Replace('.', ',');
            string longitudeStr = longitude.ToString().Replace('.', ',');

            string url = String.Format(TanAdresses.ArretsByGeo, latitudeStr, longitudeStr);

            var t = this.GetDataAsync(url);
            t.Wait();

            TanArret[] arrets = JsonConvert.DeserializeObject<TanArret[]>(t.Result);

            return arrets;
        }

        public async  Task<TanDepart[]> GetWaitTimeForStopAsync(string stopId)
        {
            string url = String.Format(TanAdresses.WaitTime, stopId);

            string a = await this.GetDataAsync(url);
            TanDepart[] waiTime = JsonConvert.DeserializeObject<TanDepart[]>(a);

            return waiTime;
        }

        public TanDepart[] GetWaitTimeForStop(string stopId)
        {
            string url = String.Format(TanAdresses.WaitTime, stopId);

            var t = this.GetDataAsync(url);
            t.Wait();

            TanDepart[] waiTime = JsonConvert.DeserializeObject<TanDepart[]>(t.Result);

            return waiTime;
        }

        private Task<string> GetDataAsync(string url)
        {
            HttpWebRequest hwr = WebRequest.Create(new Uri(url)) as HttpWebRequest;

            hwr.Accept = "application/json";
            hwr.Headers["Accept-language"] = "fr_FR";

            Task<WebResponse> task = Task.Factory.FromAsync(
                hwr.BeginGetResponse,
                asyncResult => hwr.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    //Need to return this response 
                    string strContent = sr.ReadToEnd();
                    return strContent;
                }
            }
        }
    }
}
