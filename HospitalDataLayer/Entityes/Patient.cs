using HospitalDataLayer.Entityes.Base;
using HospitalDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDataLayer.Entityes
{
    public partial class Patient : Entity
    {
        //public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Patronymic { get; set; }

        public string Address { get; set; } = null!;

        public DateTime BirthdayDate { get; set; }

        public bool Gender { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; } = null!;
    }
}
