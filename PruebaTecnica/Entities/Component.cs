using System.Text.Json.Serialization;

namespace PruebaTecnica.Entities
{
      public class Component
    {
        public int Id { get; set; }
        public string Part { get; set; }
        public string ComponentType { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public int MachineId { get; set; }
        [JsonIgnore] 
        public Machine? Machine { get; set; }
    }
}
