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
    public class MarksController : ControllerBase
    {
        private readonly IMarkService _markService;

        public MarksController(IMarkService markService)
        {
            _markService = markService;
        }

        [HttpGet]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult> GetAllMarks()
        {
            var response = await _markService.GetAllMarks();
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "StudentOnly")]
        public async Task<ActionResult> GetMarkById(int id)
        {
            var response = await _markService.GetMarkById(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<ActionResult> AddMark(MarkRequestDto mark)
        {
            var response = await _markService.AddMark(mark);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<ActionResult> UpdateMark(int id, MarkRequestDto mark)
        {
            var response = await _markService.UpdateMark(mark ,id);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "TeacherOnly")]
        public async Task<ActionResult> DeleteMark(int id)
        {
            var response = await _markService.DeleteMark(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpGet("~/api/enrollments/{id}/[controller]")]
        public async Task<ActionResult> GetMarksByEnrollmentId(int id)
        {
            var response = await _markService.GetMarksByEnrollmentId(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("~/api/subject/{id}/[controller]")]
        public async Task<ActionResult> GetMarksBySubjectId(int id)
        {
            var response = await _markService.GetMarksBySubjectId(id);
            if(!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}