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
    public class StepController : ControllerBase
    {
        private readonly ILogger<StepController> _logger;

        public StepController(ILogger<StepController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (!TrackerAppContext.Steps.Any(a => a.ExerciseID == id))
                return BadRequest("Exercise Does Not Exist");

            return Ok(TrackerAppContext.Steps.Where(a => a.ExerciseID == id));
        }
    }
}
