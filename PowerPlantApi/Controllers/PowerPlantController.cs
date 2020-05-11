using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PowerPlantApi.Interfaces;
using PowerPlantApi.Models;

namespace PowerPlantApi.Controllers
{
    [ApiController]
    public class PowerPlantsController : ControllerBase
    {
        private readonly ILogger<PowerPlantsController> _logger;
        private readonly IPowerPlantsComputingService _powerPlantsComputingService;


        public PowerPlantsController(ILogger<PowerPlantsController> logger, IPowerPlantsComputingService powerPlantsComputingService)
        {
            _logger = logger;
            _powerPlantsComputingService = powerPlantsComputingService;
        }

        [HttpPost("Production")]
        public ActionResult<IEnumerable<PowerPlantsResponse>> PowerPlantsProduction([FromBody]PowerPlantsPayload payload)
        {
            if (payload == null)
            {
                _logger.LogError("missing payload");
                return NotFound();
            }
                
             return Ok(_powerPlantsComputingService.ComputeUnitCommitment(payload));
        }
    }
}
