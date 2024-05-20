using ApplicationLayer.Commands;
using ApplicationLayer.Queries;
using AutoClassLibrary.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatStudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediatStudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreateStudent(CreateStudentCommand command)
        {
            var studentId = await _mediator.Send(command);
            return Ok(studentId);
        }

        // Dispatch Query
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var query = new GetStudentByIdQuery { Id = id };
            var student = await _mediator.Send(query);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
    }
}
