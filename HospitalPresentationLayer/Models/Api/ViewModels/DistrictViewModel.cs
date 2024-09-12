using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public class DistrictViewModel : IViewModel
    {
        public int Id { get; set; }
        public short Num { get; set; }
    }
}
