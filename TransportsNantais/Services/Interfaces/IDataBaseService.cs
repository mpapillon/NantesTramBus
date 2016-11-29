using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportsNantais.Models;

namespace TransportsNantais.Services
{
    public interface IDataBaseService
    {
        Task<IList<Line>> GetLinesAsync();
        Task<IList<Line>> GetLinesForStopAsync(int stopId);
        Task<Line> GetLineAsync(int id);
        Task InsertLineAsync(Line line);
        Task UpdateLineAsync(Line line);
        Task DeleteLineAsync(Line line);

        Task<IList<Stop>> GetStopsAsync();
        Task<Stop> GetStopAsync(int id);
        Task<Stop> GetStopByTanIdAsync(string id);
        Task<IList<Stop>> GetStopsByNameAsync(string name);
        Task<IList<Stop>> GetStopsForDirectionAsync(int directionId);
        Task<IList<Stop>> GetStopsForLineAsync(int line);
        Task InsertStopAsync(Stop stop);
        Task UpdateStopAsync(Stop stop);
        Task DeleteStopAsync(Stop stop);

        Task<IList<Direction>> GetDirectionsAsync(bool relations = true);
        Task<Direction> GetDirectionAsync(int id, bool relations = true);
        Task InsertDirectionAsync(Direction dir);
        Task UpdateDirectionAsync(Direction dir);
        Task DeleteDirectionAsync(Direction direction);
        Task<IList<Direction>> GetDirectionsForLineAsync(int lineid);

        Task<IList<LineStop>> GetLineStopsAsync(bool relations = true);
        Task<LineStop> GetLineStopAsync(int id);
        Task InsertLineStopAsync(LineStop ls);
        Task UpdateLineStopAsync(LineStop ls);
        Task DeleteLineStopAsync(LineStop ls);
        Task<IList<LineStop>> GetLineStopsForLineAsync(int lineId);
        Task<IList<LineStop>> GetLineStopsForDirectionAsync(int directionId);

        Task<IList<LastStop>> GetLastStopsAsync();
        Task<LastStop> GetLastStopAsync(int id);
        Task InsertLastStopAsync(LastStop ls);
        Task UpdateLastStopAsync(LastStop ls);
        Task DeleteLastStopAsync(LastStop ls);

        Task InsertAllAsync(IEnumerable data);
        Task UpdateAllAsync(IEnumerable data);
    }
}
