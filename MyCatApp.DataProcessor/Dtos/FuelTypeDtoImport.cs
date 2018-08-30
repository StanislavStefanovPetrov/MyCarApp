using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class FuelTypeDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Fuel { get; set; }
    }
}
