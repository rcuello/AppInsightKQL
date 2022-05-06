using HandsOnTest.AppInsight.Domain.DealerTrack;
using HandsOnTest.AppInsight.Domain.ViewModel;
using HandsOnTest.ETracker.Services.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnTest.ETracker.Services.WebApi.Controllers
{
    /// <summary>
    /// Parse a csv file and returns list of sales
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DealerTrackController : ControllerBase
    {
        protected Dictionary<string, object> Logvalues { get; }
        private ILogger<DealerTrackController> _logger;

        private readonly IDealerTrackDomain _dealerTrackDomain;
        public DealerTrackController(IDealerTrackDomain dealerTrackDomain, ILogger<DealerTrackController> logger)
        {
            _dealerTrackDomain = dealerTrackDomain;
            _logger = logger;
            Logvalues = new Dictionary<string, object>();

        }
        /// <summary>
        /// Parse a csv file and returns list of sales
        /// </summary>
        /// <param name="file">A csv file where each row represents a sale of a vehicle</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /api/DealerTrack/Parse
        ///
        /// </remarks>
        /// <returns>list of sales to retrieve</returns>
        /// <response code="200">If csv file is valid return list of sales
        /// </response>
        [HttpPost("Parse")]
        public async Task<IActionResult> Parse([FromForm] DealerTrackViewModel model)
        {
            var result = await _dealerTrackDomain.ProcessFile(model.File);
            return Ok(result);
        }

        [HttpPost("Log")]
        public async Task<IActionResult> Log()
        {
            var requestId = Guid.NewGuid().ToString();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Logvalues.Add("process-name", "UnEjemploProc");
            Logvalues.Add("trace-id", requestId);

            using (_logger.BeginScope(Logvalues))
            {
                Logvalues.Add("execution-time", watch.ElapsedMilliseconds);
                _logger.LogWarning($"Starting process SampleLoggin");
            }


            watch.Stop();
            /*
             traces
                |extend traceId = tostring(customDimensions["trace-id"])
                | where traceId =='d135eee3-4f61-493b-b31a-ca37efc5a5d2'
             */
            return Ok(requestId);
        }

        [HttpPost("LogError")]
        public async Task<IActionResult> LogError()
        {
            var requestId = Guid.NewGuid().ToString();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Logvalues.Add("process-name", "UnEjemploProc");
            Logvalues.Add("trace-id", requestId);

            try
            {
                throw new ArgumentException("Esto es un error de ejemplo");

            }catch (Exception ex)
            {
                using (_logger.BeginScope(Logvalues))
                {
                    Logvalues.Add("execution-time", watch.ElapsedMilliseconds);
                    _logger.LogError(ex,ex.Message);
                }
            }
            /*
             exceptions
            |extend traceId = tostring(customDimensions["trace-id"])
            | where traceId =='c0f88ec3-9940-4ee1-9d60-cfc7165073d0'
             */

            return Ok(requestId);
        }
    }

}
