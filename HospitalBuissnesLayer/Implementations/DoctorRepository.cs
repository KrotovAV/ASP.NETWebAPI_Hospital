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
    public class DoctorRepository : DbRepository<Doctor>
    {
        public override IQueryable<Doctor> Items => base.Items
                                                        .Include(item => item.Cabinet)
                                                        .Include(item => item.Specialization)
                                                        .Include(item => item.District);
        public DoctorRepository(HospitalContext db) : base(db) { }
    }
}
