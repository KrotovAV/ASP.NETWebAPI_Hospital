using HospitalBuissnesLayer.Implementations;
using HospitalDataLayer.Entityes;

namespace HospitalBuissnesLayer.Interfaces
{
    public interface IDataManager
    {
        public IRepository<Cabinet> Cabinets { get; }
        public IRepository<District> Districts { get; }
        public IRepository<Doctor> Doctors { get; }
        public IRepository<Patient> Patients { get; }
        public IRepository<Specialization> Specializations { get; }
    }
}