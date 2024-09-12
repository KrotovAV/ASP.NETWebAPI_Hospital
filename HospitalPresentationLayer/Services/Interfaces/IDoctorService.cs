using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ViewModels;

namespace HospitalPresentationLayer.Services.Interfaces
{
    public interface IDoctorService : ICRUDService<DoctorEditModel>, IListViewModelService<DoctorViewModel, DoctorFilter>
    {
    }
}
