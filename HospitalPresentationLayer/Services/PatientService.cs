using AutoMapper;
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
using HospitalBuissnesLayer;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using HospitalBuissnesLayer.Interfaces;

namespace HospitalPresentationLayer.Services
{
    public class PatientService : IPatientService
    {
        private readonly IDataManager _dataManager;
        private readonly IListEntityService<Patient> _listPatientService;

        public PatientService(DataManager dataManager, IListEntityService<Patient> listPatientService)
        {
            _dataManager = dataManager;
            _listPatientService = listPatientService;
        }

        public int Create(PatientEditModel model)
        {
            Patient patient = new Patient();
            //patient.Id = model.Id;
            patient.Name = model.Name;
            patient.Surname = model.Surname;
            patient.Patronymic = model.Patronymic;
            patient.Address = model.Address;
            patient.BirthdayDate = model.BirthdayDate;
            patient.Gender = model.Gender;
            patient.DistrictId = model.DistrictId;

            _dataManager.Patients.Add(patient);

            return patient.Id;
        }

        public void Delete(int id)
        {
            var patient = _dataManager.Patients.Get(id);
            if (patient != null)
            {
                _dataManager.Patients.Remove(id);
            }
        }

        public ListViewModel<PatientViewModel, PatientFilter> Get(ListOptions<PatientFilter> options)
        {
            var orderOptions = options.Order ?? new OrderOptions();
            var ids = _listPatientService.GetIds(
                _dataManager.Patients.Items.AsNoTracking(),
                options.Filter ?? new PatientFilter(),
                orderOptions,
                options.Page ?? new PageOptions());

            var patients = _dataManager.Patients.Items
                .Where(d => ids.Contains(d.Id))
                .Include(d => d.District)
                .AsNoTracking();

            _listPatientService.QueryOrder(ref patients, orderOptions);

            var sortPatients = patients.ToList();
            List<PatientViewModel> listModel = new List<PatientViewModel>();
            foreach (var patient in sortPatients) 
            {
                listModel.Add(
                   new PatientViewModel
                   {
                       Id = patient.Id,
                       Name = patient.Name,
                       Surname = patient.Surname,
                       Patronymic = patient.Patronymic,
                       Address = patient.Address,
                       BirthdayDate = patient.BirthdayDate,
                       Gender = patient.Gender,
                       District = (short)patient.District.Num

                   }
                );
            }

            var model = new ListViewModel<PatientViewModel, PatientFilter>
            {
                List = listModel,
                Options = options
            };

            return model;
        }

        public PatientEditModel Get(int id)
        {
            var patient = _dataManager.Patients.Get(id);

            PatientEditModel patientEditModel = new PatientEditModel();
            patientEditModel.Name = patient.Name;
            patientEditModel.Surname = patient.Surname;
            patientEditModel.Patronymic = patient.Patronymic;
            patientEditModel.Address = patient.Address;
            patientEditModel.BirthdayDate = patient.BirthdayDate;
            patientEditModel.Gender = patient.Gender;
            patientEditModel.DistrictId = patient.DistrictId;

            return patientEditModel;
        }

        public void Update(PatientEditModel model)
        {
            Patient patient = new Patient();
            patient.Id = model.Id;
            patient.Name = model.Name;
            patient.Surname = model.Surname;
            patient.Patronymic = model.Patronymic;
            patient.Address = model.Address;
            patient.BirthdayDate = model.BirthdayDate;
            patient.Gender = model.Gender;
            patient.DistrictId = model.DistrictId;

            _dataManager.Patients.Update(patient);
        }
    }
}
