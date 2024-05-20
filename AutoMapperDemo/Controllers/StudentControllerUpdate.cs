using AutoClassLibrary.Context;
using AutoClassLibrary.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperDemo.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentControllerUpdate : ControllerBase
    {

        private readonly AppDbContext _appDbContext;

        public StudentControllerUpdate(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _appDbContext.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            _appDbContext.Students.Add(student);
            await _appDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //[HttpDelete("{postId}")]
        //public async Task<IActionResult> Delete(int postId)
        //{
        //    var student = await _appDbContext.Students.FindAsync(postId);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    _appDbContext.Students.Remove(student);
        //    await _appDbContext.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool StudentExists(int id)
        {
            return _appDbContext.Students.Any(e => e.Id == id);
        }

    }
    }
