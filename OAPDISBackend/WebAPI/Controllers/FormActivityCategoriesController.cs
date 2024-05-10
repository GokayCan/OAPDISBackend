using Business.Repositories.FormActivityCategoryRepository;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormActivityCategoriesController : ControllerBase
    {
        private readonly IFormActivityCategoryService _formActivityCategoryService;

        public FormActivityCategoriesController(IFormActivityCategoryService formActivityCategoryService)
        {
            _formActivityCategoryService = formActivityCategoryService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(FormActivityCategory formActivityCategory)
        {
            var result = await _formActivityCategoryService.Add(formActivityCategory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(FormActivityCategory formActivityCategory)
        {
            var result = await _formActivityCategoryService.Update(formActivityCategory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(FormActivityCategory formActivityCategory)
        {
            var result = await _formActivityCategoryService.Delete(formActivityCategory);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _formActivityCategoryService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _formActivityCategoryService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
