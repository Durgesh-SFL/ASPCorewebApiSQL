using ASPCorewebApiSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCorewebApiSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly AppDbContext contex;

        public StudentApiController(AppDbContext contex)
        {
            this.contex = contex;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
            var data = await contex.Students.ToListAsync();
            return Ok(data);
            //ok is cntrollr base class method and gives status code of our response
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> getStudenyById(int id)
        {
            var student = await contex.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
                //controller base class method and give status 404NotFound
            }
            return student;

        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent(Student std)
        {
            await contex.Students.AddAsync(std);
            await contex.SaveChangesAsync();
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            //contex.Entry(std).State = EntityState.Modified;
            if (await contex.Students.FirstOrDefaultAsync(x => x.Id == id) is Student existingStudent)
            {
                contex.Entry(existingStudent).CurrentValues.SetValues(std);
                await contex.SaveChangesAsync();
                return Ok(std);
            }
            return NotFound();
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
           var std =await contex.Students.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
             contex.Students.Remove(std);
            await contex.SaveChangesAsync();
            return Ok();
        }
    }


}
