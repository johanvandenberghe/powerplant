using PowerPlantApi.Interfaces;
using PowerPlantApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlantApi.Services
{
    public class PowerPlantsComputingService : IPowerPlantsComputingService
    {
        public PowerPlantsResponse ComputeUnitCommitment(PowerPlantsPayload payload) 
        {
            var loadToProduce = payload.load;
            var response = new PowerPlantsResponse(payload);
            
            foreach (var powerPlantResponse in response.PowerPlants.OrderBy(p => p.meritOrder))
            {
                if (loadToProduce > 0)
                    loadToProduce = powerPlantResponse.Product(loadToProduce);
            }
            
            return response;
        }
    }
}
