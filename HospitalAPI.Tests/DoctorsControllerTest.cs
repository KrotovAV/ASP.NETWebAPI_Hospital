using HospitalAPI.Controllers;
using HospitalBuissnesLayer.Implementations;
using HospitalBuissnesLayer;
using HospitalDataLayer;
using HospitalPresentationLayer.Services;
using HospitalPresentationLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using HospitalDataLayer.Entityes;
using Microsoft.AspNetCore.Mvc;
using HospitalPresentationLayer.Models.Api.EditModels;

namespace HospitalAPI.Tests
{
    public class DoctorsControllerTest
    {
        DoctorsController _controller;
        IDoctorService _service;

        public DoctorsControllerTest()
        {
            HospitalContext hospitalContext = new HospitalContext(new DbContextOptionsBuilder<HospitalContext>().UseSqlServer("Server=DESKTOP-IFVARIJ\\SQLEXPRESS;Database=HospitalDB;User Id=Admin;Password=MsSQLavk;TrustServerCertificate=True").Options);
            CabinetRepository cabsRepo = new CabinetRepository(hospitalContext);
            DistrictRepository distrsRepo = new DistrictRepository(hospitalContext);
            DoctorRepository docsRepo = new DoctorRepository(hospitalContext);
            PatientRepository patsRepo = new PatientRepository(hospitalContext);
            SpecializationRepository specsRepo = new SpecializationRepository(hospitalContext);
            DataManager dataManager = new DataManager(cabsRepo, distrsRepo, docsRepo, patsRepo, specsRepo);
            ListEntityService<Doctor> listEntityService = new ListEntityService<Doctor>();

            _service = new DoctorService(dataManager, listEntityService);
            _controller = new DoctorsController(_service);
        }
        [Fact]
        public void Test1()
        {

        }

        
        [Theory]
        [InlineData("1", "222")]
        public void GetDoctorByIdTest(string id1, string id2)
        {
            //Arrange
            var validId = int.Parse(id1);
            var invalidId = int.Parse(id2);

            //Act
            var notFoundResult = _controller.Get(invalidId);
            var okResult = _controller.Get(validId);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
            Assert.IsType<OkObjectResult>(okResult.Result);


            //Now we need to check the value of the result for the ok object result.
            var item = okResult.Result as OkObjectResult;

            //We Expect to return a single book
            Assert.IsType<Doctor>(item.Value);

            //Now, let us check the value itself.
            var doctorItem = item.Value as Doctor;
            Assert.Equal(validId, doctorItem.Id);
            Assert.Equal("Managing Oneself", doctorItem.FullName);
        }

        [Fact]
        public void AddDoctorTest()
        {
            //OK RESULT TEST START

            //Arrange
            var completeDoctor = new DoctorEditModel()
            {
                FullName = "Test Доктор",
                DistrictId = 1,
                SpecializationId = 1,
                CabinetId = 1
            };

            //Act
            var createdResponse = _controller.Post(completeDoctor);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            //value of the result
            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Doctor>(item.Value);

            //check value of this book
            var doctorItem = item.Value as Doctor;
            Assert.Equal(completeDoctor.FullName, doctorItem.FullName);
            Assert.Equal(completeDoctor.DistrictId, doctorItem.DistrictId);
            Assert.Equal(completeDoctor.CabinetId, doctorItem.CabinetId);
            Assert.Equal(completeDoctor.SpecializationId, doctorItem.SpecializationId);

            //OK RESULT TEST END

            //BADREQUEST AND MODELSTATE ERROR TEST START

            //Arrange
            var incompleteDoctor = new DoctorEditModel()
            {
                FullName = "Test Доктор",
                DistrictId = 1
            };

            //Act
            _controller.ModelState.AddModelError("Title", "Title is a requried filed");
            var badResponse = _controller.Post(incompleteDoctor);

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);



            //BADREQUEST AND MODELSTATE ERROR TEST END
        }
    }
}
