using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.API.Model;

namespace Tracker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(TrackerAppContext.Patients.Where(a => a.PatientId == id).Select(b => b));
        }

        [HttpGet]
        [Route("check/{id}")]
        public IActionResult get(int id)
        {
            return Ok(TrackerAppContext.Patients.Select(s => s.PatientId == id));
        }
    }
}
