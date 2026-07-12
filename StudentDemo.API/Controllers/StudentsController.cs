using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentDemo.API.Model;
using StudentDemo.API.Repository;

namespace StudentDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            var id = await _repository.CreateAsync(student);

            student.Id = id;

            return CreatedAtAction(nameof(Get), new { id }, student) ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            await _repository.UpdateAsync(student);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
