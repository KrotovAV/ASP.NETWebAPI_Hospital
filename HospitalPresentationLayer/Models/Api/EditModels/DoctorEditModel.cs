using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPresentationLayer.Models.Api.EditModels
{
    public class DoctorEditModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int CabinetId { get; set; }
        public int SpecializationId { get; set; }
        public int? DistrictId { get; set; }
    }
}
