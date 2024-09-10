using HospitalDataLayer.Entityes.Base;
using HospitalDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDataLayer.Entityes
{
    public partial class Doctor : Entity
    {
        //public int Id { get; set; }

        public string FullName { get; set; }

        public int CabinetId { get; set; }

        public int SpecializationId { get; set; }

        public int? DistrictId { get; set; }

        public virtual Cabinet Cabinet { get; set; } = null!;

        public virtual Specialization Specialization { get; set; } = null!;

        public virtual District District { get; set; } = null!;
    }
}
