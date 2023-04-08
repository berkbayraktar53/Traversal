using System.Linq;
using SignalR.API.Hubs;
using SignalR.API.Contexts;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace SignalR.API.Models
{
    public class VisitorService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorService(DatabaseContext databaseContext, IHubContext<VisitorHub> hubContext)
        {
            _databaseContext = databaseContext;
            _hubContext = hubContext;
        }

        public IQueryable<Visitor> GetList()
        {
            return _databaseContext.Visitors.AsQueryable();
        }

        public async Task SaveVisitor(Visitor visitor)
        {
            await _databaseContext.Visitors.AddAsync(visitor);
            await _databaseContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("CallVisitorList", "test");
        }

        public List<VisitorChart> GetVisitorChartList()
        {
            List<VisitorChart> visitorCharts = new();
            var command = _databaseContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = "select * from crosstab\r\n(\r\n'select \"VisitDate\",\"City\",\"VisitCount\" from \"Visitors\" order by 1,2'\r\n) as ct(\"VisitDate\" date, City1 int, City2 int, City3 int, City4 int, City5 int);";
            command.CommandType = System.Data.CommandType.Text;
            _databaseContext.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VisitorChart visitorChart = new()
                    {
                        VisitDate = reader.GetDateTime(0).ToShortDateString()
                    };
                    Enumerable.Range(1, 5).ToList().ForEach(p =>
                    {
                        visitorChart.Counts.Add(reader.GetInt32(p));
                    });
                    visitorCharts.Add(visitorChart);
                }
            }
            _databaseContext.Database.CloseConnection();
            return visitorCharts;
        }
    }
}