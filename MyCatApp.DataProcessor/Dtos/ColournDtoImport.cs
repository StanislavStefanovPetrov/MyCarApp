using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class ColournDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
