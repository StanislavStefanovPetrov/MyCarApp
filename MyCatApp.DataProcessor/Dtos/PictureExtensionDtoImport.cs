using Newtonsoft.Json;

namespace MyCatApp.DataProcessor.Dtos
{
    public class PictureExtensionDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Extension { get; set; }
    }
}
