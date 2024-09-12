using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospitalPresentationLayer.Models.Api.ListViewModels
{
    public class ListViewModel<I, F> where I : IViewModel where F : FilterOptions
    {
        public List<I> List { get; set; }
        public ListOptions<F> Options { get; set; }
    }
}
