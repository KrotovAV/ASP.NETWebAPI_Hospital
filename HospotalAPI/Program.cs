
using HospitalBuissnesLayer;
using HospitalBuissnesLayer.Implementations;
using HospitalBuissnesLayer.Interfaces;
using HospitalDataLayer;
using HospitalDataLayer.Entityes;
using HospitalPresentationLayer.Services.Interfaces;
using HospitalPresentationLayer.Services;
using Microsoft.EntityFrameworkCore;
using HospitalPresentationLayer.Models.Api.ViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels.Interfaces;

namespace HospotalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //************
            builder.Configuration.GetConnectionString("Connection");
            var confBuilder = new ConfigurationBuilder();
            confBuilder.SetBasePath(Directory.GetCurrentDirectory());
            confBuilder.AddJsonFile("appsettings.json");
            var cfg = confBuilder.Build();
            //************

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //***************** 
            builder.Services.AddSingleton<HospitalContext>(); 
            
            builder.Services.AddTransient<IRepository<Cabinet>, CabinetRepository>();
            builder.Services.AddTransient<IRepository<District>, DistrictRepository>();
            builder.Services.AddTransient<IRepository<Doctor>, DoctorRepository>();
            builder.Services.AddTransient<IRepository<Patient>, PatientRepository>();
            builder.Services.AddTransient<IRepository<Specialization>, SpecializationRepository>();
            builder.Services.AddScoped<DataManager>();
            builder.Services.AddTransient<IDataManager, DataManager>();
            
            builder.Services.AddTransient<IListEntityService<Doctor>, ListEntityService<Doctor>>();
            builder.Services.AddTransient<IDoctorService, DoctorService>();

            builder.Services.AddTransient<IListEntityService<Patient>, ListEntityService<Patient>>();
            builder.Services.AddTransient<IPatientService, PatientService>();

            builder.Services.AddTransient<IListDistrictService, ListDistrictService>();
            builder.Services.AddTransient<IDistrictService, DistrictService>();
            //*****************
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
