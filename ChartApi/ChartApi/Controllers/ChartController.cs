﻿using ChartApi.Models;
using ChartApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        private readonly PopulationService _service;
        public ChartController(PopulationService service)
        {
            _service= service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
           // await _service.SavePopulation(population);
            IQueryable<Population> populationList = _service.GetList();
            return Ok(_service.GetChartList());
        }

        [HttpPost]
        public async Task<IActionResult> SavePopulation(Population population)
        {
            await _service.SavePopulation(population);
            IQueryable<Population> populationList = _service.GetList();
            return Ok(_service.GetChartList());
        }

        //[HttpGet]
        //public  IActionResult RandomData()
        //{
        //    Random rnd=new Random();
        //    Enumerable.Range(1,10).ToList().ForEach(x=> {
                
        //        foreach (ECity item in Enum.GetValues(typeof(ECity)))
        //        {
        //            var newItem = new Population { 
        //                                            City=item,
        //                                            Count=rnd.Next(100,1000),
        //                                            ImmigrationDate=DateTime.Now.AddYears(-x)
        //                                         };
        //            _service.SavePopulation(newItem).Wait();
        //            System.Threading.Thread.Sleep(1000);
        //        }
        //    });
        //    return Ok("Random Data Eklendi");
        //}
    }
}
