using Business.Repositories.TeacherProjectRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherProjectsController : ControllerBase
    {
        private readonly ITeacherProjectService _teacherProjectService;

        public TeacherProjectsController(ITeacherProjectService teacherProjectService)
        {
            _teacherProjectService = teacherProjectService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(TeacherProjectDto teacherProject)
        {
            var result = await _teacherProjectService.Add(teacherProject);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(TeacherProjectDto teacherProject)
        {
            var result = await _teacherProjectService.Update(teacherProject);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(TeacherProject teacherProject)
        {
            var result = await _teacherProjectService.Delete(teacherProject);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _teacherProjectService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetListByUserId(int id)
        {
            var result = await _teacherProjectService.GetListByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teacherProjectService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}