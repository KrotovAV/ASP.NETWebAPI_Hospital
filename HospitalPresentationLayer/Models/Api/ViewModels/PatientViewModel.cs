using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public partial class PatientViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; }
        public string Address { get; set; } = null!;
        public DateTime BirthdayDate { get; set; }
        public bool Gender { get; set; }
        public short District { get; set; }
    }
}
