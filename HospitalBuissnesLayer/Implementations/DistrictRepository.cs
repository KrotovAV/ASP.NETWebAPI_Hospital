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
    public class DistrictRepository : DbRepository<District>
    {
        public override IQueryable<District> Items => base.Items
                                                        .Include(item => item.Doctors)
                                                        .Include(item => item.Patients);
        public DistrictRepository(HospitalContext db) : base(db) { }
    }
}
