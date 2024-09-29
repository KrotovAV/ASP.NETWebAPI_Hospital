using HospitalBuissnesLayer.Interfaces;
using HospitalBuissnesLayer;
using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Models.Api.ViewModels;
using HospitalPresentationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalDataLayer.Interfaces;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using System.Linq.Expressions;

namespace HospitalPresentationLayer.Services
{
    public class ListDistrictService : IListDistrictService
    {
        private readonly IDataManager _dataManager;
        
        public ListDistrictService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<DistrictViewModel> Get()
        {
            List<DistrictViewModel> listModel = new List<DistrictViewModel>();
            var districts = _dataManager.Districts.Items;

            foreach (var district in districts)
            {
                List<string> districtsDoctors = new List<string>();
                //var doctors = _dataManager.Doctors.Items.Where(x => x.DistrictId == district.Id);
                //foreach (var doc in doctors)
                foreach (var doc in district.Doctors)
                {
                    districtsDoctors.Add(doc.FullName);
                }

                List<string> districtsPatients = new List<string>();
                //var patients = _dataManager.Patients.Items.Where(x => x.DistrictId == district.Id);
                //foreach (var patient in patients)
                foreach (var patient in district.Patients)
                {
                    districtsPatients.Add(patient.Name + " " + patient.Surname + " " + patient.Patronymic);
                }
                listModel.Add(
                    new DistrictViewModel
                    {
                        Id = district.Id,
                        Num = district.Num,
                        Doctors = districtsDoctors,
                        Patients = districtsPatients
                    }
                );
            }

            return listModel;
        }
    }
}
