
namespace HospitalPresentationLayer.Models.Api.ListViewModels.Options
{
    public class PatientFilter : FilterOptions
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Patronymic { get; set; }

        public string? Address { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public bool? Gender { get; set; }

        public int? DistrictId { get; set; }
    }
}
