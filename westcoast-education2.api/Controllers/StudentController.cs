
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;
using westcoast_education2.api.ViewModels;

namespace westcoast_education2.api.Controllers;

    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
    private readonly WestCoastEducationContext _context;
    public StudentController(WestCoastEducationContext context)
    {
        _context = context;
    }

        //http://localhost:5004/api/students
        [HttpGet()]
        public async Task<ActionResult> List(){
            var result = await _context.studentData
            .Include(c => c.courseName)
            .Select(v => new StudentsListViewModel{
                userId = v.userId,
                firstName = v.firstName,
                lastName = v.lastName,
                personNu = v.personNu,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTakenName = v.courseName.name ?? ""
            })
            .ToListAsync();
            return Ok(result);
        }

        //---------------------------------------------------

        //http://localhost:5004/api/students/3302
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetByUserId(int userId){
            var result = await _context.studentData
            .Include(c => c.courseName)
            .Select(v => new StudentsListViewModel{
                userId = v.userId,
                firstName = v.firstName,
                lastName = v.lastName,
                personNu = v.personNu,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTakenName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.userId == userId);
            
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("personNu/{personNu}")]
        public async Task<ActionResult> GetByPersonNu(string personNu){
            var result = await _context.studentData
            .Include(c => c.courseName)
            .Select(v => new StudentsListViewModel{
                userId = v.userId,
                firstName = v.firstName,
                lastName = v.lastName,
                personNu = v.personNu,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTakenName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.personNu! .ToUpper().Trim() == personNu.ToUpper().Trim());
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("email/{email}")]
        public  async Task<ActionResult> GetByEmail(string email){
            var result = await _context.studentData
            .Include(c => c.courseName)
            .Select(v => new StudentsListViewModel{
                userId = v.userId,
                firstName = v.firstName,
                lastName = v.lastName,
                personNu = v.personNu,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTakenName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.email! .ToUpper().Trim() == email.ToUpper().Trim());
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpPost("addstudent")]
        public async Task<ActionResult> AddStudent(StudentAddViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to store the student in the system");
            var exists = await _context.studentData.SingleOrDefaultAsync(c=> c.userId == model.userId);
            if (exists is not null) return BadRequest($"We have already registered a student with userId {model.userId}");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.coursesTakenName.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.coursesTakenName} in our system");

            var student = new StudentsModel{
                userId = model.userId,
                firstName = model.firstName,
                lastName = model.lastName,
                personNu = model.personNu,
                email = model.email,
                phoneNumber = model.phoneNumber,
                address = model.address,
                courseName = CourseName
            };
            await _context.studentData.AddAsync(student);
            if (await _context.SaveChangesAsync() > 0){
                return Created(nameof(GetByUserId), new {userId = student.userId});
            }
            return StatusCode(500, "Internet Server Error");
        }

        //---------------------------------------------------

        [HttpPut("update/{userId}")]
        public async Task<ActionResult> UpdateStudent(int userId, StudentUpdateViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to updater the student in the system");
            var student = await _context.studentData.FindAsync(userId);
            if (student is  null) return BadRequest($"We cannot find a student in the system with this userId ");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.coursesTakenName.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.coursesTakenName} in our system");

            student.firstName = model.firstName;
            student.lastName = model.lastName;
            student.personNu = model.personNu;
            student.email = model.email;
            student.phoneNumber = model.phoneNumber;
            student.address = model.address;
            student.courseName = CourseName;
            
            _context.studentData.Update(student);
            if (await _context.SaveChangesAsync() > 0){
                return NoContent();
            }
            return StatusCode(500, "Internet Server Error");
        }

        //---------------------------------------------------

        [HttpGet("coursesRegistered/{userId}")]
        public async Task<ActionResult> ListOfCoursesRegistered(int userId){
            var result = await _context.studentData
            .Include(c => c.courseName)
            .Select(v => new CoursesRegisteredListOfStudentViewModel{
                userId = v.userId,
                coursesTakenName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.userId == userId);
            return Ok(result);
        }

        //---------------------------------------------------
    }
