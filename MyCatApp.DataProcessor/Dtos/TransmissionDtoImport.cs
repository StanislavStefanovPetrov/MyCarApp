using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class TransmissionDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }
    }
}
