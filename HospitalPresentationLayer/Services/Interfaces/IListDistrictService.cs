using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalDataLayer.Interfaces;
using HospitalPresentationLayer.Models.Api.ViewModels;

namespace HospitalPresentationLayer.Services.Interfaces
{
    public interface IListDistrictService
    {
        List<DistrictViewModel> Get();
    }
}
