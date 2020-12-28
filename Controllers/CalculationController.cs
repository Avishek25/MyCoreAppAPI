using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StatusApi.Models;

namespace StatusApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private static readonly Dictionary<Guid, CalculationOutcome> Statuses = new Dictionary<Guid, CalculationOutcome>();

        private readonly ILogger<CalculationController> _logger;
        private readonly IConfiguration _config;

        public CalculationController(ILogger<CalculationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Route("/calculate/{first}/{second}")]
        public Guid StartCalculation(int first, int second)
        {
            var guid = Guid.NewGuid();
            var time = int.Parse(_config.GetSection("TimeTakenToCalculateInMS").Value);

            short status = 0;
            Statuses.Add(guid, new CalculationOutcome { Progress = status, Outcome = null });
            while (status < 100)
            {
                Thread.Sleep(time / 20);
                status += 5;
                Statuses[guid].Progress = status;
            }

            Statuses[guid].Outcome = (first * second).ToString();

            return guid;
        }

        [HttpGet]
        [Route("/status/{id}")]
        public StatusObject GetStatus(Guid id)
        {
            var progress = Statuses.ContainsKey(id) ? Statuses[id] : null;
            if (progress == null)
            {
                throw new Exception($"Id: {id} does not exist");
            }

            return new StatusObject
            {
                Status = progress.Progress == 100 ? Status.Completed : Status.Running,
                Progress = progress.Progress + "%",
                Outcome = progress.Outcome
            };
        }
    }
}
