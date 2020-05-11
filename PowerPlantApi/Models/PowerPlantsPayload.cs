using System.Collections.Generic;

namespace PowerPlantApi.Models
{
    public class PowerPlantsPayload
    {
            public int load { get; set; }
            public Fuels fuels { get; set; }
            public IEnumerable<PowerPlant> powerplants { get; set; }
    }
}