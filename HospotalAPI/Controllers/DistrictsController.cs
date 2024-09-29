using HospitalPresentationLayer.Models.Api.EditModels;
using HospitalPresentationLayer.Models.Api.ListViewModels.Options;
using HospitalPresentationLayer.Models.Api.ListViewModels;
using HospitalPresentationLayer.Models.Api.ViewModels;
using HospitalPresentationLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HospitalPresentationLayer.Services;
using HospitalDataLayer.Entityes;
using System.Collections.Generic;

namespace HospitalAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : Controller
    {

        private readonly IDistrictService _districtService;

        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet("{id}")]
        public ActionResult<DistrictEditModel> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = _districtService.Get(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            return Ok(district);
        }

        [HttpGet]
        public ActionResult<List<DistrictViewModel>> GetAll()
        {
            var districts = _districtService.Get();
            return Ok(districts);
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] DistrictEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _districtService.Create(model);
            return Ok(id);
        }

        [HttpPut]
        public ActionResult Put([FromBody] DistrictEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _districtService.Update(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var district = _districtService.Get(id.Value);
            if (district == null)
            {
                return NotFound();
            }
            _districtService.Delete(id.Value);
            return Ok();
        }
    }
}
