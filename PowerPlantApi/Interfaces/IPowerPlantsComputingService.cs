using PowerPlantApi.Models;

namespace PowerPlantApi.Interfaces
{
    public interface IPowerPlantsComputingService
    {
        PowerPlantsResponse ComputeUnitCommitment(PowerPlantsPayload payload);
    }
}