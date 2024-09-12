
namespace HospitalPresentationLayer.Models.Api.ListViewModels.Options
{
    public class DoctorFilter : FilterOptions
    {
        public string? FullName { get; set; }
        public int? CabinetId { get; set; }
        public int? SpecializationId { get; set; }
        public int? DistrictId { get; set; }
    }
}
