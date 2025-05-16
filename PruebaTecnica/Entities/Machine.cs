namespace PruebaTecnica.Entities
{
    public class Machine
    {
         public int Id { get; set; }
        public string TechnicalLocation { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string MachineTypeName { get; set; }
        public string BrandName { get; set; }
        public string Criticality { get; set; }
        public string Sector { get; set; }
        public ICollection<Component> Components { get; set; } = new List<Component>();

    }
}
