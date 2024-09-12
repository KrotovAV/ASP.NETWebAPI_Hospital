using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public class DoctorViewModel : IViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public short Cabinet { get; set; }
        public string Specialization { get; set; }
        public short? District { get; set; }
    }
}
