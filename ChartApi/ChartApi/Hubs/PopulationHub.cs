using ChartApi.Models;
using ChartApi.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChartApi.Hubs
{
    public class PopulationHub : Hub
    {
        private readonly PopulationService _service;

        public PopulationHub(PopulationService service)
        {
            _service= service;
        }
        public async Task GetPopulationList()
        {
            await Clients.All.SendAsync("ReceiveList",_service.GetChartList());
        }

        public async Task BroadcastChartData(List<ChartModel> data) =>
          await Clients.All.SendAsync("broadcastchartdata", data);
    }
}
