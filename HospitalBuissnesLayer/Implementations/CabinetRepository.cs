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
    public class CabinetRepository : DbRepository<Cabinet>
    {
        public override IQueryable<Cabinet> Items => base.Items
                                                        .Include(item => item.Doctors);
                                                       
        public CabinetRepository(HospitalContext db) : base(db) { }
    }
}
