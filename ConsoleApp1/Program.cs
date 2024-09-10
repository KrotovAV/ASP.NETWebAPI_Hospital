using HospitalBuissnesLayer;
using HospitalBuissnesLayer.Implementations;
using HospitalDataLayer;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            const string connection = "Server=DESKTOP-IFVARIJ\\SQLEXPRESS;Database=HospitalDB;User Id=Admin;Password=MsSQLavk;TrustServerCertificate=True";
            
            using var db = new HospitalContext(new DbContextOptionsBuilder<HospitalContext>().UseSqlServer(connection).Options);

            //* Context ***********************************
            //var docs = db.Doctors.Take(5).ToList();
            //if (docs != null)
            //{
            //    Console.WriteLine($"есть доктора: {docs.Count()} шт");
            //    foreach (var doc in docs)
            //    {
            //        Console.WriteLine(doc.FullName + " - " + db.Specializations.FirstOrDefault(x => x.Id == doc.SpecializationId).Name);
            //    }
            //}
            //else Console.WriteLine($"нет докторов");
            //*************************************

            //- Repository ------------------------------
            //DoctorRepository docsRepo = new DoctorRepository(db);
            //var docsR = docsRepo.Items.ToList();

            //foreach (var doc in docsR)
            //{

            //    Console.WriteLine(doc.FullName + " - Спец:" + doc.Specialization.Name+ " - Кабинет:" + doc.Cabinet.Num + " - Участок:" + doc.District.Num);
            //}
            //-----------------------------


            //- DataManager ------------------------------
            CabinetRepository cabsRepo = new CabinetRepository(db);
            DistrictRepository distrsRepo = new DistrictRepository(db);
            DoctorRepository docsRepo = new DoctorRepository(db);
            PatientRepository patsRepo = new PatientRepository(db);
            SpecializationRepository specsRepo = new SpecializationRepository(db);

            DataManager dataManager = new DataManager(cabsRepo, distrsRepo, docsRepo, patsRepo, specsRepo);
            var docsR = dataManager.Doctors.Items.ToList();

            foreach (var doc in docsR)
            {

                Console.WriteLine(doc.FullName + " - Спец:" + doc.Specialization.Name + " - Кабинет:" + doc.Cabinet.Num + " - Участок:" + doc.District.Num);
            }
            //-----------------------------
        }
    }
}
