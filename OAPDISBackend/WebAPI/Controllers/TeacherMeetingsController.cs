using Business.Repositories.TeacherMeetingRepository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherMeetingsController : ControllerBase
    {
        private readonly ITeacherMeetingService _teacherMeetingService;

        public TeacherMeetingsController(ITeacherMeetingService teacherMeetingService)
        {
            _teacherMeetingService = teacherMeetingService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(TeacherMeetingDto teacherMeeting)
        {
            var result = await _teacherMeetingService.Add(teacherMeeting);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(TeacherMeetingDto teacherMeeting)
        {
            var result = await _teacherMeetingService.Update(teacherMeeting);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(TeacherMeeting teacherMeeting)
        {
            var result = await _teacherMeetingService.Delete(teacherMeeting);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _teacherMeetingService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetListByUserId(int id)
        {
            var result = await _teacherMeetingService.GetListByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _teacherMeetingService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}