using HospitalBuissnesLayer;
using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels;
using HospitalPresentationLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _doctorService;
        
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("{id}")]
        public ActionResult<DoctorEditModel> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctor = _doctorService.Get(id.Value);

            return Ok(doctor);
        }

        [HttpGet]
        public ActionResult<ListViewModel<DoctorViewModel, DoctorFilter>> GetAll([FromQuery] ListOptions<DoctorFilter> options)
        {
            var doctors = _doctorService.Get(options);
            return Ok(doctors);
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] DoctorEditModel model)
        {
            var id = _doctorService.Create(model);
            return Ok(id);
        }

        [HttpPut]
        public ActionResult Put([FromBody] DoctorEditModel model)
        {
            _doctorService.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _doctorService.Delete(id.Value);
            return Ok();
        }
    }
}
