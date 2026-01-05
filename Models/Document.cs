namespace SUPREA_LOGISTICS.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string VehicleId { get; set; }

        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public DateTime Updated { get; set; }

        public string FilePath { get; set; }
    }
}
