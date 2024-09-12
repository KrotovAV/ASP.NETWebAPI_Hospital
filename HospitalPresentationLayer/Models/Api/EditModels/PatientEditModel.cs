using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPresentationLayer.Models.Api.EditModels
{
    public class PatientEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime BirthdayDate { get; set; }
        public bool Gender { get; set; }
        public int DistrictId { get; set; }
    }
}
