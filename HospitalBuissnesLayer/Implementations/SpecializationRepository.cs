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
    public class SpecializationRepository : DbRepository<Specialization>
    {
        public override IQueryable<Specialization> Items => base.Items
                                                        .Include(item => item.Doctors);
        public SpecializationRepository(HospitalContext db) : base(db) { }
    }
}
