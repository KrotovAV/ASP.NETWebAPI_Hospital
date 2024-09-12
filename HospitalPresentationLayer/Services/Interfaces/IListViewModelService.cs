using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Services.Interfaces
{
    public interface IListViewModelService<E, F> where E : IViewModel where F : FilterOptions
    {
        ListViewModel<E, F> Get(ListOptions<F> options);
    }
}
