using HospitalBuissnesLayer.Interfaces;
using HospitalBuissnesLayer;
using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels;
using HospitalPresentationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HospitalPresentationLayer.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDataManager _dataManager;
        private readonly IListDistrictService _listDistrictService;

        public DistrictService(DataManager dataManager, IListDistrictService listDistrictService)
        {
            _dataManager = dataManager;
            _listDistrictService = listDistrictService;
        }

        public int Create(DistrictEditModel model)
        {
            District district = new District();
            //doctor.Id = model.Id;
            district.Num = model.Num;
           
            _dataManager.Districts.Add(district);

            return district.Id;
        }

        public void Delete(int id)
        {
            var district = _dataManager.Districts.Get(id);
            if (district != null)
            {
                _dataManager.Districts.Remove(id);
            }
        }

        public List<DistrictViewModel> Get()
        {
            return _listDistrictService.Get();
        }

        public DistrictEditModel Get(int id)
        {
            var district = _dataManager.Districts.Get(id);

            DistrictEditModel districtEditModel = new DistrictEditModel();
            districtEditModel.Id = district.Id;
            districtEditModel.Num = district.Num;

            return districtEditModel;
        }

        public void Update(DistrictEditModel model)
        {
            District district = new District();
            district.Id = model.Id;
            district.Num = model.Num;
            
            _dataManager.Districts.Update(district);
        }
    }
}
