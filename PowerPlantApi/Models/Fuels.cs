using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerPlantApi.Models
{
    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public decimal gaseuroMWh { get; set; }

        [JsonPropertyName("kerosine(euro/MWh)")]
        public decimal kerosineeuroMWh { get; set; }

        [JsonPropertyName("co2(euro/ton)")]
        public int co2euroton { get; set; }

        [JsonPropertyName("wind(%)")]
        public int wind { get; set; }

    }
}
