using HospitalDataLayer;
using HospitalDataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalBuissnesLayer.Implementations
{
    public class PatientRepository : DbRepository<Patient>
    {
        public override IQueryable<Patient> Items => base.Items
                                                        .Include(item => item.District);
        public PatientRepository(HospitalContext db) : base(db) { }
    }
}
