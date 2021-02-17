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
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(ILogger<ExerciseController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(TrackerAppContext.Exercises.Select(b => b.ExerciseName));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (!TrackerAppContext.Exercises.Any(a => a.Id == id))
                return BadRequest("Exercise Does Not Exist");

            return Ok(TrackerAppContext.Exercises.Where(a => a.Id == id));
        }
    }
}
