using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPresentationLayer.Services.Interfaces
{
    public interface IPatientService : ICRUDService<PatientEditModel>, IListViewModelService<PatientViewModel, PatientFilter>
    {
    }
}
