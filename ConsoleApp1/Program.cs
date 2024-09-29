using HospitalBuissnesLayer;
using HospitalBuissnesLayer.Implementations;
using HospitalDataLayer;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HospitalPresentationLayer.Services.Interfaces;
using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;
using HospitalPresentationLayer.Models.Api.EditModels;

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

            Console.WriteLine("* DataManager ******************");
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
                Console.WriteLine("id: " + doc.Id + " * " + doc.FullName + " - Спец:" + doc.Specialization.Name + " - Кабинет:" + doc.Cabinet.Num + " - Участок:" + doc.District.Num);
            }
            //-----------------------------
            Console.WriteLine("* DoctorService ******************");

            //-  DoctorService ------------------------------


            ListEntityService<Doctor> listEntityService = new ListEntityService<Doctor>();
            DoctorService doctorService = new DoctorService(dataManager, listEntityService);
            //ListOptions<F> where F : FilterOptions {OrderOptions Order, PageOptions Page, F Filter }
            //OrderOptions { Имя столбца string Field = "Id", Направление по возрастанию/убыванию OrderDirection Direction = OrderDirection.None}

            //enum OrderDirection { Без сортировки None = 0, По возрастанию Asc, По убыванию Desc}
            //PageOptions {Количество позиций по результату запроса int Count, Количество позиций на 1 страницы int PageSize = 10,
            //Количество страниц int PageTotal, Номер текущей страницы int PageCurrent = 1}
            
            //DoctorFilter : FilterOptions {string? FullName, int? CabinetId, int? SpecializationId, int? DistrictId}
            
            ListOptions<DoctorFilter> options = new ListOptions<DoctorFilter> { 
                Order = new OrderOptions { Field = "FullName", Direction = OrderDirection.Asc}, 
                Page = new PageOptions { Count = 10, PageSize = 10, PageTotal = 10, PageCurrent = 1 }, 
                Filter = new DoctorFilter ()
            };
            
            var docsServ = doctorService.Get(options);
            //ListViewModel<I, F> where I : IViewModel where F : FilterOptions {List<DoctorViewModel> List, ListOptions<DoctorFilter> Options} , 
            foreach (var doc in docsServ.List)
            {
                Console.WriteLine("id: " + doc.Id + " * " + doc.FullName + " - Спец:" + doc.Specialization + " - Кабинет:" + doc.Cabinet + " - Участок:" + doc.District);
            }
            //-----------------------------

            //Console.WriteLine("post");
            //DoctorEditModel doctorEditModel = new DoctorEditModel { Id = 0, FullName = "Викторов Виктор Викторович", CabinetId = 7, SpecializationId = 1, DistrictId = 3 };
            //doctorService.Create(doctorEditModel);
            //Console.WriteLine("id = " + dataManager.Doctors.Items.FirstOrDefault(x => x.FullName == doctorEditModel.FullName).Id);

            Console.WriteLine("Districts: ");

            ListDistrictService listDistrictService = new ListDistrictService(dataManager);

            DistrictService districtService = new DistrictService(dataManager, listDistrictService);
            var distsServ = districtService.Get();
            //ListViewModel<I, F> where I : IViewModel where F : FilterOptions {List<DoctorViewModel> List, ListOptions<DoctorFilter> Options} , 
            foreach (var distr in distsServ)
            {
                Console.WriteLine();
                Console.Write("id: " + distr.Id + " * " + distr.Num + "; Doctors: ");
                foreach(var doc in distr.Doctors)
                {
                    Console.Write(doc + ", ");
                }
                Console.Write("; Patients: ");
                foreach (var pat in distr.Patients)
                {
                    Console.Write(pat + ", ");
                }
                Console.Write(". ");
            }



        }
    }
}
