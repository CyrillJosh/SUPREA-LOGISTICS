using System.Text.Json.Serialization;

namespace SUPREA_LOGISTICS.Models
{
    public class Vehicle
    {
        [JsonPropertyName("VECHICLE ID")]
        public string VehicleId { get; set; }

        [JsonPropertyName("UNIT TYPE")]
        public string UnitType { get; set; }

        [JsonPropertyName("UNIT MODEL/SERIES")]
        public string UnitModelSeries { get; set; }

        [JsonPropertyName("BRAND/MAKE")]
        public string BrandMake { get; set; }

        [JsonPropertyName("YEAR MODEL")]
        public string YearModel { get; set; }

        [JsonPropertyName("ENGINE NO.")]
        public string EngineNo { get; set; }

        [JsonPropertyName("CHASIS NO.")]
        public string ChasisNo { get; set; }

        [JsonPropertyName("PLATE NO")]
        public string PlateNo { get; set; }

        [JsonPropertyName("OR/CR")]
        public string OrCr { get; set; }

        [JsonPropertyName("EXPIRATION")]
        public string Expiration { get; set; }

        [JsonPropertyName("INSURANCE ")]
        public string Insurance { get; set; }

        [JsonPropertyName("INSURANCE COVERAGE")]
        public string InsuranceCoverage { get; set; }

        [JsonPropertyName("INSURANCE PROVIDER")]
        public string InsuranceProvider { get; set; }

        [JsonPropertyName("DATE ACQUIRED")]
        public string DateAcquired { get; set; }

        [JsonPropertyName("SUPPLIER")]
        public string Supplier { get; set; }

        [JsonPropertyName("PROJECT ID")]
        public string ProjectId { get; set; }

        [JsonPropertyName("SITE LOCATION")]
        public string SiteLocation { get; set; }

        [JsonPropertyName("VEHICLE STATUS")]
        public string VehicleStatus { get; set; }
    }

}
