using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportsNantais.Models;
using TransportsNantais.Enums;

namespace TransportsNantais.Services.Design
{
    public class DataServiceDesign
    {
        public Task<IList<TransportsNantais.Models.Line>> GetLinesAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                IList<Line> list = new List<Line>()
                {
                    new Line() { LineId = 1, NumLigne = "1", LineType = LineType.Tramway, OneDirection = "", OppositeDirection = "", FontColor = "FFFEFF", BackColor = "007944" },
                    new Line() { LineId = 2, NumLigne = "11", LineType = LineType.Bus, OneDirection = "", OppositeDirection = "", FontColor = "E7C389", BackColor = "1A171B" }
                };

                return list;
            });
        }

        public Task<TransportsNantais.Models.Line> GetLineByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertLineAsync(TransportsNantais.Models.Line line)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLineAsync(TransportsNantais.Models.Line line)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TransportsNantais.Models.Stop>> GetStopsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransportsNantais.Models.Stop> GetStopByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertStopAsync(TransportsNantais.Models.Stop stop)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStopAsync(TransportsNantais.Models.Stop stop)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TransportsNantais.Models.Direction>> GetDirectionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransportsNantais.Models.Direction> GetDirectionByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertDirectionAsync(TransportsNantais.Models.Direction dir)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDirectionAsync(TransportsNantais.Models.Direction dir)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TransportsNantais.Models.LineStop>> GetLineStopsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransportsNantais.Models.LineStop> GetLineStopByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertLineStopAsync(TransportsNantais.Models.LineStop ls)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLineStopAsync(TransportsNantais.Models.LineStop ls)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TransportsNantais.Models.LastStop>> GetLastStopsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransportsNantais.Models.LastStop> GetLastStopByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertLastStopAsync(TransportsNantais.Models.LastStop ls)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLastStopAsync(TransportsNantais.Models.LastStop ls)
        {
            throw new NotImplementedException();
        }
    }
}
