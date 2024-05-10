using Business.Repositories.FormActivitiyRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormActivitiiesController : ControllerBase
    {
        private readonly IFormActivitiyService _formActivitiyService;

        public FormActivitiiesController(IFormActivitiyService formActivitiyService)
        {
            _formActivitiyService = formActivitiyService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(FormActivitiy formActivitiy)
        {
            var result = await _formActivitiyService.Add(formActivitiy);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(FormActivitiy formActivitiy)
        {
            var result = await _formActivitiyService.Update(formActivitiy);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(FormActivitiy formActivitiy)
        {
            var result = await _formActivitiyService.Delete(formActivitiy);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _formActivitiyService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _formActivitiyService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
