using HospitalDataLayer;
using HospitalDataLayer.Entityes;
using Microsoft.EntityFrameworkCore;

namespace HospitalBuissnesLayer.Implementations
{
    public class CabinetRepository : DbRepository<Cabinet>
    {
        public override IQueryable<Cabinet> Items => base.Items
                                                        .Include(item => item.Doctors);
                                                       
        public CabinetRepository(HospitalContext db) : base(db) { }
    }
}
