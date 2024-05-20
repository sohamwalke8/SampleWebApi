using AutoClassLibrary.DTO;
using AutoClassLibrary.Model;
using AutoClassLibrary.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AutoMapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRepoController : ControllerBase
    {
        private readonly IGenericRepository<Student> _repo;
        private readonly IMapper _mapper;

        public StudentRepoController(IGenericRepository<Student> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [Route("addstudent")]
        [HttpPost]
        public async Task<ActionResult> addStudent(StudentDto student)
        {
            var st = _mapper.Map<Student>(student);
            var studen = await _repo.Add1Async(st);//method name change 
            Log.Information("Students Info:{@studen}", studen);
            return Ok(studen);
        }

        [Route("readstudent")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> ReadStudent()
        {

            var students = await _repo.ListAllAsync();
            var ST = _mapper.Map<IEnumerable<StudentDto>>(students);
            Log.Information("Students Info:{@ST}", ST);
            return Ok(ST);
        }
        [Route("updatestudent")]
        [HttpPut]

        public async Task<ActionResult> updateStudent(StudentDto model)
        {
            var st = _mapper.Map<Student>(model);
            await _repo.Update1Async(st);



            return Ok("Student Updated SuccessFully");
        }

        [Route("deletestudent")]
        [HttpDelete]
        public async Task<ActionResult> deletestudent(int id)
        {
            var stud = await _repo.GetByIdsAsync(id);

            await _repo.Delete1Async(stud);
            return Ok("Student Deleted SuccessFully");
        }
    }
}
