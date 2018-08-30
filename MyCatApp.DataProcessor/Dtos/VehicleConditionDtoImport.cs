using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class VehicleConditionDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Condition { get; set; }
    }
}
