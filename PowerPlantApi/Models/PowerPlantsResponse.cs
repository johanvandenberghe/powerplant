using PowerPlantApi.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PowerPlantApi.Models
{
    public class PowerPlantsResponse
    {
        private readonly PowerPlantsPayload _payload;
        private List<PowerPlantResponse> _powerPlants;

        public PowerPlantsResponse(PowerPlantsPayload payload)
        {
            _payload = payload;
        }
                
        public List<PowerPlantResponse> PowerPlants
        {
            get
            {
                if (_powerPlants == null)
                {
                    _powerPlants = new List<PowerPlantResponse>();
                    foreach (var powerPlant in _payload.powerplants)
                    {
                        var porwerPlan = new PowerPlantResponse(_payload.fuels, powerPlant);
                        _powerPlants.Add(porwerPlan);
                    }
                }
                return _powerPlants;
            }
        }
    }

    public class PowerPlantResponse
    {
        private Fuels _fuels;
        private PowerPlant _powerPlant;

        public PowerPlantResponse(Fuels fuels, PowerPlant powerPlant)
        {
            _fuels = fuels;
            _powerPlant = powerPlant;
        }

        public string name => _powerPlant.name;
        public int p { get; set; } = 0;

        [JsonIgnore]
        public decimal meritOrder
        {
            get
            {
                switch (_powerPlant.type)
                {
                    case "gasfired":
                        return (1 / _powerPlant.efficiency) * _fuels.gaseuroMWh;

                    case "turbojet":
                        return _fuels.kerosineeuroMWh;

                    default:
                        return 0;
                }
            }
        }
        

        public int Product(int load)
        {
            if (load < _powerPlant.pmin)
            {
                p = _powerPlant.pmin;
                return 0;
            }

            if (load >= _powerPlant.pmax)
            {
                p = _powerPlant.pmax;
                return load - _powerPlant.pmax;
            }

            p = load;
            return 0;
        }

    }
}