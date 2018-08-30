using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyCatApp.DataProcessor.Dtos
{
    public class VehicleMakeDtoImport
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ICollection<VehicleModelDtoImport> Models { get; set; } = new List<VehicleModelDtoImport>();
    }
}
