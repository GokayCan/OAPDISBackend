using Business.Repositories.TeacherArticleRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherArticlesController : ControllerBase
    {
        private readonly ITeacherArticleService _teacherArticleService;

        public TeacherArticlesController(ITeacherArticleService teacherArticleService)
        {
            _teacherArticleService = teacherArticleService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromForm] TeacherArticleDto teacherArticle)
        {
            var result = await _teacherArticleService.Add(teacherArticle);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromForm] TeacherArticleDto teacherArticle)
        {
            var result = await _teacherArticleService.Update(teacherArticle);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(TeacherArticle teacherArticle)
        {
            var result = await _teacherArticleService.Delete(teacherArticle);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _teacherArticleService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetListByUserId(int id)
        {
            var result = await _teacherArticleService.GetListByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teacherArticleService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}