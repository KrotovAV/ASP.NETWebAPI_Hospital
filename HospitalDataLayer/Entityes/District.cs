using HospitalDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDataLayer.Entityes
{
    public partial class District : IEntity
    {
        public int Id { get; set; }
        public int Num { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

        public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
