using ChartApi.Hubs;
using ChartApi.Models;
using ChartApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        private readonly PopulationService _service;

        private readonly IHubContext<PopulationHub> _hub;
        private readonly TimerManager _timer;
        public ChartController(PopulationService service, IHubContext<PopulationHub> hub, TimerManager timer)
        {
            _service= service;
            _hub = hub;
            _timer = timer;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if (!_timer.IsTimerStarted)
            {
                _timer.PrepareTimer(() => _hub.Clients.All.SendAsync("TransferChartData", DataManager.GetData()));
            }

            return Ok(new { Message = "Request Completed" });
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    IQueryable<Population> populationList = _service.GetList();
        //    return Ok(_service.GetChartList());
        //}

        //[HttpPost]
        //public async Task<IActionResult> SavePopulation(Population population)
        //{
        //    await _service.SavePopulation(population);
        //    IQueryable<Population> populationList = _service.GetList();
        //    return Ok(_service.GetChartList());
        //}
        //[HttpGet]
        public void RandomData()
        {
            Random rnd = new Random();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {

                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newItem = new Population
                    {
                        City = item,
                        Count = rnd.Next(100, 1000),
                        ImmigrationDate = DateTime.Now.AddYears(-x)
                    };
                    _service.SavePopulation(newItem).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });
        }
    }
}
