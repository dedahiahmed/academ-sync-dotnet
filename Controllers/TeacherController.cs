using academ_sync_back.Models;
using academ_sync_back.requests;
using academ_sync_back.services;
using Microsoft.AspNetCore.Mvc;

namespace academ_sync_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return teacher;
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return teachers;
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherRequest teacher)
        {
            try
            {
                await _teacherService.AddTeacherAsync(teacher);
                return Ok();    
            }
            catch (ArgumentException ex)
            {
                // Handle the specific exception thrown by AddTeacherAsync
                if (ex.Message == "User with the specified userId not found")
                {
                    return BadRequest(new { message = ex.Message });
                }

                // Handle other exceptions
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest("Invalid teacher id");
            }

            try
            {
                await _teacherService.UpdateTeacherAsync(teacher);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherService.DeleteTeacherAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
