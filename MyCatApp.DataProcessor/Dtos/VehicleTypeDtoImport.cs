using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class VehicleTypeDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }
    }
}
