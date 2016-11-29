using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportsNantais.Models;
using System.Collections;
#if WINDOWS_WPF
using TransportsNantais.DataManager;
#endif

namespace TransportsNantais.Services
{
    public class SQLiteService : IDataBaseService
    {
        #region Lignes

        public async Task<IList<Line>> GetLinesAsync()
        {
            var db = await App.GetDatabaseAsync();
            var d = await db.Table<Line>().ToListAsync();

            return d;
        }

        public async Task<IList<Line>> GetLinesForStopAsync(int stopId)
        {
            var db = await App.GetDatabaseAsync();

            string query = @"select Line.*
                            from Line, Direction
                            where Line.LineId = Direction.LineId
                            and Direction.DirectionId in ( 
                                                  select LineStop.DirectionId 
                                                  from LineStop, SubStop, Stop
                                                  where LineStop.SubStopId = SubStop.SubStopId
                                                  and   SubStop.ParentId = Stop.StopId
                                                  and   Stop.StopId = ?
                                                )
                            group by Direction.LineId;";

            var d = await db.QueryAsync<Line>(query, stopId);

            return d;
        }

        public async Task<Line> GetLineAsync(int id)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Line>();
            var l = await table.Where(t => t.LineId == id).FirstAsync();

            return l;
        }

        public async Task InsertLineAsync(Line line)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAsync(line);
        }

        public async Task UpdateLineAsync(Line line)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAsync(line);
        }

        public async Task DeleteLineAsync(Line line)
        {
            var db = await App.GetDatabaseAsync();

            var dirs = await this.GetDirectionsForLineAsync(line.LineId);
            var ls = await this.GetLineStopsForLineAsync(line.LineId);

            foreach (var d in dirs)
            {
                await db.DeleteAsync(d);
            }

            foreach (var d in ls)
            {
                await db.DeleteAsync(d);
            }

            await db.DeleteAsync(line);
        }

        #endregion

        #region Stops

        public async Task<IList<Stop>> GetStopsAsync()
        {
            var db = await App.GetDatabaseAsync();
            return await db.Table<Stop>().ToListAsync();
        }

        public async Task<Stop> GetStopAsync(int id)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Stop>();
            var d = await table.Where(t => t.StopId == id).FirstAsync();

            return d;
        }

        public async Task<Stop> GetStopByTanIdAsync(string id)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Stop>();
            var d = await table.Where(t => t.TanId == id).FirstAsync();

            return d;
        }

        public async Task<IList<Stop>> GetStopsByNameAsync(string name)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Stop>();
            var d = await table.Where(t => t.Name.Contains(name)).ToListAsync();

            return d;
        }

        public async Task<IList<Stop>> GetStopsForDirectionAsync(int directionId)
        {
            var db = await App.GetDatabaseAsync();
            string query = @"select distinct Stop.StopId, Stop.TanId, Stop.Name, Stop.Latitude, Stop.Longitude
                            from Stop
                            inner join LineStop, SubStop
                            on Stop.StopId = SubStop.ParentId
                            and SubStop.SubStopId = LineStop.SubStopId
                            and LineStop.DirectionId = ?;";

            var d = await db.QueryAsync<Stop>(query, directionId);

            return d;
        }

        public async Task<IList<Stop>> GetStopsForLineAsync(int lineId)
        {
            var db = await App.GetDatabaseAsync();
            string query = @"select distinct Stop.StopId, Stop.TanId, Stop.Name, Stop.Latitude, Stop.Longitude
                            from Stop
                            inner join LineStop, SubStop, Direction
                            on Stop.StopId = SubStop.ParentId
                            and SubStop.SubStopId = LineStop.SubStopId
                            and LineStop.DirectionId = Direction.DirectionId
                            and Direction.LineId = ?
                            order by Stop.Name;";

            var d = await db.QueryAsync<Stop>(query, lineId);

            return d;
        }

        public async Task InsertStopAsync(Stop stop)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAsync(stop);
        }

        public async Task UpdateStopAsync(Stop stop)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAsync(stop);
        }

        public async Task DeleteStopAsync(Stop d)
        {
            var db = await App.GetDatabaseAsync();
            await db.DeleteAsync(d);
        }

        #endregion

        #region Directions

        public async Task<IList<Direction>> GetDirectionsAsync(bool relations = true)
        {
            var db = await App.GetDatabaseAsync();
            var d = await db.Table<Direction>().ToListAsync();

            if (relations)
            {
                foreach (var dir in d)
                {
                    dir.Line = await this.GetLineAsync(dir.LineId);
                    dir.LastStop = await this.GetLastStopAsync(dir.LastStopId);
                }
            }

            return d;
        }

        public async Task<Direction> GetDirectionAsync(int id, bool relations = true)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Direction>();
            var d = await table.Where(t => t.DirectionId == id).FirstAsync();

            if (relations)
            {
                d.Line = await this.GetLineAsync(d.LineId);
                d.LastStop = await this.GetLastStopAsync(d.LastStopId);
            }

            return d;
        }

        public async Task InsertDirectionAsync(Direction dir)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAsync(dir);
        }

        public async Task UpdateDirectionAsync(Direction dir)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAsync(dir);
        }

        public async Task<IList<Direction>> GetDirectionsForLineAsync(int l)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<Direction>();
            var d = await table.Where(t => t.LineId == l).ToListAsync();

            foreach (var dir in d)
            {
                dir.Line = await this.GetLineAsync(dir.LineId);
                dir.LastStop = await this.GetLastStopAsync(dir.LastStopId);
            }

            return d;
        }

        public async Task DeleteDirectionAsync(Direction d)
        {
            var db = await App.GetDatabaseAsync();
            await db.DeleteAsync(d);
        }

        #endregion

        #region LineStops

        public async Task<IList<LineStop>> GetLineStopsAsync(bool relations = true)
        {
            var db = await App.GetDatabaseAsync();
            var d = await db.Table<LineStop>().ToListAsync();

            if (relations)
            {
                foreach (var ls in d)
                {
                    ls.Direction = await this.GetDirectionAsync(ls.DirectionId);
                }
            }

            return d;
        }

        public async Task<LineStop> GetLineStopAsync(int id)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<LineStop>();
            var ls = await table.Where(t => t.LineStopId == id).FirstAsync();

            ls.Direction = await this.GetDirectionAsync(ls.DirectionId);

            return ls;
        }

        public async Task InsertLineStopAsync(LineStop ls)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAsync(ls);
        }

        public async Task UpdateLineStopAsync(LineStop ls)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAsync(ls);
        }

        public async Task DeleteLineStopAsync(LineStop d)
        {
            var db = await App.GetDatabaseAsync();
            await db.DeleteAsync(d);
        }

        public async Task<IList<LineStop>> GetLineStopsForLineAsync(int lineId)
        {
            var db = await App.GetDatabaseAsync();
            //var table = db.Table<LineStop>();
            //var d = await table.Where(t => t.LineId == lineId).ToListAsync();

            //foreach (var ls in d)
            //{
            //    ls.Direction = await this.GetDirectionAsync(ls.DirectionId);
            //    ls.Stop = await this.GetStopAsync(ls.StopId);
            //    ls.Line = await this.GetLineAsync(ls.LineId);
            //}

            string query = @"select LineStop.LineStopId, LineStop.DirectionId, LineStop.SubStopId
                            from LineStop, Direction
                            where LineStop.DirectionId = Direction.DirectionId
                            and Direction.LineId = 1";

            var d = await db.QueryAsync<LineStop>(query, lineId);

            return d;
        }

        public async Task<IList<LineStop>> GetLineStopsForDirectionAsync(int directionId)
        {
            var db = await App.GetDatabaseAsync();
            //var table = db.Table<LineStop>();
            //var d = await table.Where(t => (t.LineId == lineId) && (t.DirectionId == directionId)).ToListAsync();

            //foreach (var ls in d)
            //{
            //    ls.Direction = await this.GetDirectionAsync(ls.DirectionId, false);
            //    ls.Stop = await this.GetStopAsync(ls.StopId);
            //    ls.Line = await this.GetLineAsync(ls.LineId);
            //}

            string query = @"select LineStop.LineStopId, LineStop.DirectionId, LineStop.SubStopId,
                                    SubStop.TanId, SubStop.Name, SubStop.ParentId,
                                    SubStop.Latitude, SubStop.Longitude
                            from LineStop
                            left join SubStop
                            on SubStop.SubStopId = LineStop.SubStopId
                            where LineStop.DirectionId = ?";

            List<LineStop> lineStops = new List<LineStop>(await db.QueryAsync<LineStop>(query, directionId));
            List<SubStop> subStops = new List<SubStop>(await db.QueryAsync<SubStop>(query, directionId));

            var d = (from ls in lineStops
                     join ss in subStops on ls.SubStopId equals ss.SubStopId
                     select new LineStop()
                     {
                         LineStopId = ls.LineStopId,
                         DirectionId = ls.DirectionId,
                         SubStopId = ls.SubStopId,
                         SubStop = new SubStop()
                         {
                             SubStopId = ss.SubStopId,
                             Name = ss.Name,
                             Latitude = ss.Latitude,
                             Longitude = ss.Longitude,
                             ParentId = ss.ParentId,
                             TanId = ss.TanId
                         }
                     }).ToList();

            return d;
        }

        #endregion

        #region LastStops

        public async Task<IList<LastStop>> GetLastStopsAsync()
        {
            var db = await App.GetDatabaseAsync();
            return await db.Table<LastStop>().ToListAsync();
        }

        public async Task<LastStop> GetLastStopAsync(int id)
        {
            var db = await App.GetDatabaseAsync();
            var table = db.Table<LastStop>();
            var d = await table.Where(t => t.LastStopId == id).FirstAsync();

            return d;
        }

        public async Task InsertLastStopAsync(LastStop ls)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAsync(ls);
        }

        public async Task UpdateLastStopAsync(LastStop ls)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAsync(ls);
        }

        public async Task DeleteLastStopAsync(LastStop ls)
        {
            var db = await App.GetDatabaseAsync();
            await db.DeleteAsync(ls);
        }

        #endregion

        public async Task InsertAllAsync(IEnumerable data)
        {
            var db = await App.GetDatabaseAsync();
            await db.InsertAllAsync(data);
        }

        public async Task UpdateAllAsync(IEnumerable data)
        {
            var db = await App.GetDatabaseAsync();
            await db.UpdateAllAsync(data);
        }
    }
}
