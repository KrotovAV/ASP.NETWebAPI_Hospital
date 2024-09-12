using HospitalBuissnesLayer.Implementations;
using HospitalBuissnesLayer.Interfaces;
using HospitalDataLayer.Entityes;

namespace HospitalBuissnesLayer
{
    public class DataManager : IDataManager
    {
        private IRepository<Cabinet> _cabinetRepository;
        private IRepository<District> _districtRepository;
        private IRepository<Doctor> _doctorRepository;
        private IRepository<Patient> _patientRepository;
        private IRepository<Specialization> _specializationRepository;

        public DataManager(
                            IRepository<Cabinet> cabinetRepository,
                            IRepository<District> districtRepository,
                            IRepository<Doctor> doctorRepository,
                            IRepository<Patient> patientRepository,
                            IRepository<Specialization> specializationRepository
                            )
        {
            _cabinetRepository = cabinetRepository;
            _districtRepository = districtRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _specializationRepository = specializationRepository;
        }

        public IRepository<Cabinet> Cabinets { get { return _cabinetRepository; } }
        public IRepository<District> Districts { get { return _districtRepository; } }
        public IRepository<Doctor> Doctors { get { return _doctorRepository; } }
        public IRepository<Patient> Patients { get { return _patientRepository; } }
        public IRepository<Specialization> Specializations { get { return _specializationRepository; } }
    }
}
