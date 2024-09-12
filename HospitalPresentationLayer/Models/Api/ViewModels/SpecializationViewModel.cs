using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public class SpecializationViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
