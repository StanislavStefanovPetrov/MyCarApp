namespace MyCarApp.Models.Vehicles
{
    public class EngineFuelType
    {
        public int EngineId { get; set; }
        public Engine Engine { get; set; }

        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
    }
}
