using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HospitalPresentationLayer.Services.Interfaces;
using HospitalPresentationLayer.Services;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public ActionResult<PatientEditModel> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patient = _patientService.Get(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpGet]
        public ActionResult<ListViewModel<PatientViewModel, PatientFilter>> GetAll([FromQuery] ListOptions<PatientFilter> options)
        {
            var patient = _patientService.Get(options);
            return Ok(patient);
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] PatientEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _patientService.Create(model);
            return Ok(id);
        }

        [HttpPut]
        public ActionResult Put([FromBody] PatientEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _patientService.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patient = _patientService.Get(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            _patientService.Delete(id.Value);
            return Ok();
        }
    }

}
