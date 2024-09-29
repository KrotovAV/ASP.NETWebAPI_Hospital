using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public class DistrictViewModel : IViewModel
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public List<string> Doctors { get; set; } = new List<string>();

        public List<string> Patients { get; set; } = new List<string>();
    }
}
