using ChartApi.Hubs;
using ChartApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChartApi.Services
{
    public class PopulationService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<PopulationHub> _hubContext;
        public PopulationService(AppDbContext context, IHubContext<PopulationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Population> GetList()
        {
            return _context.Populations.AsQueryable();
        }

        public async Task SavePopulation(Population model)
        {
            await _context.Populations.AddAsync(model);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveList", GetChartList()); //her kayıtda hublara bilgi gider
        }

        public List<PopulationChart> GetChartList()
        {
            List<PopulationChart> populationCharts = new List<PopulationChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select ImmigrationDate, [1], [2], [3], [4], [5] " +
               "FROM (select [City], [Count], Cast([ImmigrationDate] as date) as ImmigrationDate from Populations) as PopulationT " +
               "PIVOT (SUM(Count) for City IN ([1], [2], [3], [4], [5])) AS PTable " +
               "order by ImmigrationDate asc";
                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PopulationChart cc = new PopulationChart();
                        cc.ImmigrationDate = reader.GetDateTime(0).ToShortDateString();  //GetDateTime(0) 0.sırada
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                cc.Counts.Add(0);
                            }
                            else
                            {
                                cc.Counts.Add(reader.GetInt32(x));
                            }
                        });
                        populationCharts.Add(cc);
                    }
                }
                _context.Database.CloseConnection();
                return populationCharts;
            }
        }
    }
}
