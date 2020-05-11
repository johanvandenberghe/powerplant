namespace PowerPlantApi.Models
{
    public class PowerPlant
    {
        public string name { get; set; }
        public string type { get; set; }
        public decimal efficiency { get; set; }
        public int pmin { get; set; }
        public int pmax { get; set; }        
    }

}
