
using Microsoft.AspNetCore.Mvc;
using StudentRosterApi.Models;

namespace StudentRosterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Dela Cruz",
                Course = "BSCS",
                Email = "juan@gmail.com",
                YearLevel = 3
            },
            new Student
            {
                Id = 2,
                FirstName = "Maria",
                LastName = "Clara",
                Course = "BSIT",
                Email = "maria@gmail.com",
                YearLevel = 2
            }
        };

        // GET: api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            return Ok(student);
        }

        // GET: api/students/course/BSCS
        [HttpGet("course/{courseName}")]
        public IActionResult GetStudentsByCourse(string courseName)
        {
            var filtered = students
                .Where(s => s.Course.Equals(
                    courseName,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filtered);
        }

        // POST: api/students
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student newStudent)
        {
            if (string.IsNullOrWhiteSpace(newStudent.FirstName) ||
                string.IsNullOrWhiteSpace(newStudent.LastName))
            {
                return BadRequest(new
                {
                    message = "First name and last name are required."
                });
            }

            newStudent.Id = students.Any()
                ? students.Max(s => s.Id) + 1
                : 1;

            students.Add(newStudent);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = newStudent.Id },
                newStudent);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(
            int id,
            [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id)
            {
                return BadRequest(new
                {
                    message = "ID mismatch."
                });
            }

            var existingStudent = students.FirstOrDefault(
                s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.Course = updatedStudent.Course;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.YearLevel = updatedStudent.YearLevel;

            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            students.Remove(student);

            return NoContent();
        }
    }
}