using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.API.Hubs
{
    public class VisitorHub : Hub
    {
        public async Task GetVisitorList()
        {
            await Clients.All.SendAsync("CallVisitList", "deneme");
        }
    }
}