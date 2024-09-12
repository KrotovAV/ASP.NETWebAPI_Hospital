using HospitalBuissnesLayer;
using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Services.Interfaces;
using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using HospitalBuissnesLayer.Interfaces;

namespace HospitalPresentationLayer.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDataManager _dataManager;
        private readonly IListEntityService<Doctor> _listDoctorService;

        public DoctorService(DataManager dataManager, IListEntityService<Doctor> listDoctorService)
        {
            _dataManager = dataManager;
            _listDoctorService = listDoctorService;
        }

        public int Create(DoctorEditModel model)
        {
            Doctor doctor = new Doctor();
            //doctor.Id = model.Id;
            doctor.FullName = model.FullName;
            doctor.CabinetId = model.CabinetId;
            doctor.DistrictId = model.DistrictId;
            doctor.SpecializationId = model.SpecializationId;

            _dataManager.Doctors.Add(doctor);
            
            return doctor.Id;
        }

        public void Delete(int id)
        {
            var doctor = _dataManager.Doctors.Get(id);
            if (doctor != null)
            {
                _dataManager.Doctors.Remove(id);
            }
        }

        public ListViewModel<DoctorViewModel, DoctorFilter> Get(ListOptions<DoctorFilter> options)
        {
            var orderOptions = options.Order ?? new OrderOptions();
            var ids = _listDoctorService.GetIds(
                _dataManager.Doctors.Items.AsNoTracking(),
                options.Filter ?? new DoctorFilter(),
                orderOptions,
                options.Page ?? new PageOptions()
            );

            var doctors =  _dataManager.Doctors.Items
                .Where(d => ids.Contains(d.Id))
                .Include(d => d.Cabinet)
                .Include(d => d.Specialization)
                .Include(d => d.District)
                .AsNoTracking();

            _listDoctorService.QueryOrder(ref doctors, orderOptions);

            var sortDoctors = doctors.ToList();

            List<DoctorViewModel> listModel = new List<DoctorViewModel>();
            foreach (var doctor in sortDoctors)
            {
                listModel.Add(
                    new DoctorViewModel
                    {
                        Id = doctor.Id,
                        FullName = doctor.FullName,
                        Cabinet = (short)doctor.Cabinet.Num,
                        Specialization = doctor.Specialization.Name,
                        District = (short)doctor.District.Num
                    }
                );
            }

            var model = new ListViewModel<DoctorViewModel, DoctorFilter>
            {
            List = listModel,
            Options = options
            };

            return model;
        }

        public DoctorEditModel Get(int id)
        {
            var doctor = _dataManager.Doctors.Get(id);

            DoctorEditModel doctorEditModel = new DoctorEditModel();
            doctorEditModel.Id = doctor.Id;
            doctorEditModel.FullName = doctor.FullName;
            doctorEditModel.DistrictId = doctor.DistrictId;
            doctorEditModel.SpecializationId = doctor.SpecializationId;
            doctorEditModel.CabinetId = doctor.CabinetId;

            return doctorEditModel;
        }

        public void Update(DoctorEditModel model)
        {
            Doctor doctor = new Doctor();
            doctor.Id = model.Id;
            doctor.FullName = model.FullName;
            doctor.CabinetId = model.CabinetId;
            doctor.DistrictId = model.DistrictId;
            doctor.SpecializationId = model.SpecializationId;

            _dataManager.Doctors.Update(doctor);
        }
    }
}
