using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class VehicleModelDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}