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
    public class AchievementController : ControllerBase
    {
        private readonly ILogger<AchievementController> _logger;

        public AchievementController(ILogger<AchievementController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(TrackerAppContext.Achievements.Where(b => b.PatientId == id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Achievement achievement)
        {
            TrackerAppContext.Achievements.Add(achievement);

            TrackerAppContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var achievement = TrackerAppContext.Achievements.Find(id);

            if (achievement == null)
                return BadRequest("Achievement Does Not Exist");

            TrackerAppContext.Achievements.Remove(achievement);

            TrackerAppContext.SaveChanges();

            return Ok();
        }
    }
}
