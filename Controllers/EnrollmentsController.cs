using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CPApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var response = await _enrollmentService.GetAllEnrollments();
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollmentById(int id)
        {
            var response = await _enrollmentService.GetEnrollmentById(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "StudentOnly")]
        public async Task<IActionResult> AddEnrollment(EnrollmentRequestDto enrollment)
        {
            var response = await _enrollmentService.AddEnrollment(enrollment);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> CloseEnrollment(int id)
        {
            var response = await _enrollmentService.CloseEnrollment(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("~/api/subjects/{id}/[controller]")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> GetEnrollmentsBySubjectId(int id)
        {
            var response = await _enrollmentService.GetEnrollmentsBySubjectId(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}