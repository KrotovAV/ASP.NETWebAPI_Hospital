using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ViewModels
{
    public class CabinetViewModel : IViewModel
    {
        public int Id { get; set; }
        public short Num { get; set; }
    }
}
