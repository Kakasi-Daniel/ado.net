using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        ReportsService ReportsService { get; }
        public ReportsController(ReportsService reportsService)
        {
            ReportsService = reportsService;
        }

        [HttpGet]
        public string Get()
        {
            return ReportsService.GetReport();
        }
    }
}
