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
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var response = await _subjectService.GetAllSubjects();
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var response = await _subjectService.GetSubjectById(id);
            if(!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> AddSubject(SubjectRequestDto subject)
        {
            var response = await _subjectService.AddSubject(subject);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectRequestDto subject)
        {
            var response = await _subjectService.UpdateSubject(subject, id);
            if(!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<IActionResult> CloseSubject(int id)
        {
            var response = await _subjectService.CloseSubject(id);
            if(!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

    }
}