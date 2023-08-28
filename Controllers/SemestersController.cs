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
    public class SemestersController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemestersController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSemesters()
        {
            var response = await _semesterService.GetAllSemesters();
            if(!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);    
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSemesterById(int id)
        {
            var response = await _semesterService.GetSemesterById(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);    
            }
        }

        [HttpPost]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> AddSemester(SemesterRequestDto semester)
        {
            var response = await _semesterService.AddSemester(semester);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);    
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> UpdateSemester(int id, SemesterRequestDto semester)
        {
            var response = await _semesterService.UpdateSemester(semester, id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);    
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> CloseSemester(int id)
        {
            var response = await _semesterService.CloseSemester(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);    
            }
        }
    }
}