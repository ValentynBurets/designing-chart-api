using Business.Contract.Model;
using Business.Contract.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace designing_chart_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticService _statisticsService;

        public StatisticsController(IStatisticService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET: StatisticsController
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetAll(string userName=null, DateTime? startDate = null, DateTime? endDate = null, string category = null, string sort = null)
        {
            IEnumerable<UserStatisticReport> statisics = default;
            try
            {

                if (User.IsInRole("Admin"))
                {
                    statisics = await _statisticsService.GetStatistics(userName, startDate, endDate, category, sort);
                }
                else if (User.IsInRole("Student"))
                {
                    statisics = await _statisticsService.GetStatistics(GetUserId(), startDate, endDate, category, sort);
                }
                return Ok(statisics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET: StatisticsController/Details/5
        /*
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetById(string title)
        {
            StatisticReportModel statisic = default;
            try
            {
                
                return Ok(statisic);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        */
    }
}
