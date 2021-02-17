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
    public class GoalController : ControllerBase
    {
        private readonly ILogger<GoalController> _logger;

        public GoalController(ILogger<GoalController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        [Route("{id}/{assigned}")]
        public IActionResult Get(int id, bool assigned)
        {
            return Ok(TrackerAppContext.Goals.Where(a => a.PatientId == id).Where(b => b.Done != true).Where(c => c.Assigned == assigned));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id)
        {
            var temp = TrackerAppContext.Goals.Find(id);

            temp.Done = true;

            TrackerAppContext.Goals.Update(temp);

            TrackerAppContext.SaveChanges();

            return Ok("Goal Updated");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Goal goal)
        {
            TrackerAppContext.Goals.Add(goal);

            TrackerAppContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var goal = TrackerAppContext.Goals.Find(id);

            TrackerAppContext.Goals.Remove(goal);

            TrackerAppContext.SaveChanges();

            return Ok();
        }
    }
}
