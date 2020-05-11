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
                        switch (powerPlant.type)
                        {
                            case "gasfired":
                                _powerPlants.Add(new GasfiredPowerPlantResponse(_payload.fuels, powerPlant));
                                break;

                            case "turbojet":
                                _powerPlants.Add(new TurbojetPowerPlantResponse(_payload.fuels, powerPlant));
                                break;

                            case "windturbine":
                                _powerPlants.Add(new WindturbinePowerPlantResponse(_payload.fuels, powerPlant));
                                break;
                        }
                    }
                }
                return _powerPlants;
            }
        }
    }

    public abstract class PowerPlantResponse
    {
        protected Fuels _fuels;
        protected PowerPlant _powerPlant;

        public PowerPlantResponse(Fuels fuels, PowerPlant powerPlant)
        {
            _fuels = fuels;
            _powerPlant = powerPlant;
        }

        public string name => _powerPlant.name;
        public int p { get; set; } = 0;

        [JsonIgnore]
        public abstract decimal meritOrder { get; }

        public virtual int Product(int load)
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


    public class GasfiredPowerPlantResponse : PowerPlantResponse
    {
        public GasfiredPowerPlantResponse(Fuels fuels, PowerPlant powerPlant) : base(fuels, powerPlant)
        {
        }

        [JsonIgnore]
        public override decimal meritOrder
        {
            get
            {
                return (1 / _powerPlant.efficiency) * _fuels.gaseuroMWh;
            }
        }
    }

    public class TurbojetPowerPlantResponse : PowerPlantResponse
    {
        public TurbojetPowerPlantResponse(Fuels fuels, PowerPlant powerPlant) : base(fuels, powerPlant)
        {
        }

        [JsonIgnore]
        public override decimal meritOrder
        {
            get
            {
                return _fuels.kerosineeuroMWh;
            }
        }
    }

    public class WindturbinePowerPlantResponse : PowerPlantResponse
    {
        public WindturbinePowerPlantResponse(Fuels fuels, PowerPlant powerPlant) : base(fuels, powerPlant)
        {
        }

        [JsonIgnore]
        public override decimal meritOrder
        {
            get
            {
                return 0;
            }
        }

        public override int Product(int load)
        {
            if (load < _powerPlant.pmin)
            {
                p = _powerPlant.pmin;
                return 0;
            }

            var realPmax = (_powerPlant.pmax * _fuels.wind) / 100;

            if (load >= realPmax)
            {
                p = realPmax;
                return load - realPmax;
            }

            p = load;
            return 0;
        }
    }
}