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
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger, TrackerAppContext trackerAppContext)
        {
            _logger = logger;
            TrackerAppContext = trackerAppContext;
        }

        public TrackerAppContext TrackerAppContext { get; }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return Ok(TrackerAppContext.Images.Where(a => a.PatientID == null).Select(b => b));

            return Ok(TrackerAppContext.Images.Where(a => a.PatientID == id).Select(b => b));
        }

        [HttpGet]
        [Route("Exercise/{id}")]
        public IActionResult get(int id)
        {
            return Ok(TrackerAppContext.Images.Where(a => a.Id == id).Select(b => b));
        }
    }
}
