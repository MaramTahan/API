
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;
using westcoast_education2.api.ViewModels;

namespace westcoast_education2.api.Controllers;

    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
    private readonly WestCoastEducationContext _context;
    public TeacherController(WestCoastEducationContext context)
    {
        _context = context;
    }

        //http://localhost:5004/api/teachers
        [HttpGet()]
        public async Task<ActionResult> List(){
            var result = await _context.teacherData
            .Include(c => c.courseName)
            .Select(v => new TeachersListViewModel{
                TUserId = v.TUserId,
                firstName = v.firstName,
                lastName = v.lastName,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTaughtName = v.courseName.name ?? ""
            })
            .ToListAsync();
            return Ok(result);
        }

        //---------------------------------------------------

        //http://localhost:5004/api/teachers/330
        [HttpGet("teacher/{TUserId}")]
        public async Task<ActionResult> GetByUserId(int TUserId){
            var result = await _context.teacherData
            .Include(c => c.courseName)
            .Select(v => new TeachersListViewModel{
                TUserId = v.TUserId,
                firstName = v.firstName,
                lastName = v.lastName,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTaughtName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.TUserId == TUserId);
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("teacher/{email}")]
        public async Task<ActionResult> GetByEmail(string email){
            var result = await _context.teacherData
            .Include(c => c.courseName)
            .Select(v => new TeachersListViewModel{
                TUserId = v.TUserId,
                firstName = v.firstName,
                lastName = v.lastName,
                email = v.email,
                phoneNumber = v.phoneNumber,
                address = v.address,
                coursesTaughtName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.email! .ToUpper().Trim() == email.ToUpper().Trim());
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpPost("addTeacher")]
        public async Task<ActionResult> AddTeacher(TeacherAddViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to store the teacher in the system");
            var exists = await _context.teacherData.SingleOrDefaultAsync(c=> c.TUserId == model.TUserId);
            if (exists is not null) return BadRequest($"We have already registered a teacher with userId {model.TUserId}");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.coursesTaughtName.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.coursesTaughtName} in our system");

            var teacher = new TeachersModel{
                TUserId = model.TUserId,
                firstName = model.firstName,
                lastName = model.lastName,
                email = model.email,
                phoneNumber = model.phoneNumber,
                address = model.address,
                courseName = CourseName
            };
            await _context.teacherData.AddAsync(teacher);
            if (await _context.SaveChangesAsync() > 0){
                return Created(nameof(GetByUserId), new {TUserId = teacher.TUserId});
            }
            return StatusCode(500, "Internet Server Error");
        }

        //---------------------------------------------------

        [HttpPut("updateteacher/{TUserId}")]
        public async Task<ActionResult> UpdateTeacher(int TUserId, TeacherUpdateViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to updater the teacher in the system");

            var teacher = await _context.teacherData.FindAsync(TUserId);
            if (teacher is  null) return BadRequest($"We cannot find a teacher in the system with this CourseId ");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.coursesTaughtName.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.coursesTaughtName} in our system");

            teacher.firstName = model.firstName;
            teacher.lastName = model.lastName;
            teacher.email = model.email;
            teacher.phoneNumber = model.phoneNumber;
            teacher.address = model.address;
            teacher.courseName = CourseName;

                _context.teacherData.Update(teacher);
            if (await _context.SaveChangesAsync() > 0){
                return NoContent();
            }
            return StatusCode(500, "Internet Server Error");
        }

        //---------------------------------------------------

        [HttpGet("coursesTaught/{TUserId}")]
        public async Task<ActionResult> ListOfCoursesTaught(int TUserId){
            var result = await _context.teacherData
            .Include(c => c.courseName)
            .Select(v => new CoursesTaughtListOfTeacherViewModel{
                TUserId = v.TUserId,
                coursesTaughtName = v.courseName.name ?? ""
            })
            .SingleOrDefaultAsync(v => v.TUserId == TUserId);
            return Ok(result);
        }
        //---------------------------------------------------
    }
