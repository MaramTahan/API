
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;
using westcoast_education2.api.ViewModels;

namespace westcoast_education2.api.Controllers;

    [ApiController]
    [Route("api/c1/courses")]
    public class CoursesController : ControllerBase
    {
  private readonly WestCoastEducationContext _context;
 public CoursesController(WestCoastEducationContext context)
 {
   _context = context;
 }

 //http://localhost:5004/api/c1/courses
 [HttpGet()]
        public async Task<ActionResult> List(){
            var result = await _context.coursesData
            .Include(c => c.courseName)
            .Select(v =>new CoursesListViewModel{
                Id = v.Id,
                courseNumber = v.courseNumber,
                nameOfCourse = v.courseName.name ?? "",
                startDate = v.startDate,
                endDate = v.endDate,
                teacher = v.teacher,
                placeStudy = v.placeStudy
            })
            .ToListAsync();
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("{id}")]
        //http://localhost:5004/api/c1/courses/5
        public async Task<ActionResult> GetById(int id){
            var result = await _context.coursesData
            .Include(c => c.courseName)
            .Select(v => new CoursesListViewModel{
                Id = v.Id,
                courseNumber = v.courseNumber,
                nameOfCourse = v.courseName.name ?? "",
                startDate = v.startDate,
                endDate = v.endDate,
                teacher = v.teacher,
                placeStudy = v.placeStudy
            })
            .SingleOrDefaultAsync(v => v.Id == id);
            return Ok(result);
        
        }

        //---------------------------------------------------

        [HttpGet("courseno/{courseNumber}")]
        public async Task<ActionResult> GetBycourseNumber(string courseNumber){
            var result = await _context.coursesData
            .Include(c => c.courseName)
            .Select(v => new CoursesListViewModel{
                Id = v.Id,
                courseNumber = v.courseNumber,
                nameOfCourse = v.courseName.name ?? "",
                startDate = v.startDate,
                endDate = v.endDate,
                teacher = v.teacher,
                placeStudy = v.placeStudy
            })
            .SingleOrDefaultAsync(v => v.courseNumber! .ToUpper().Trim() == courseNumber.ToUpper().Trim());
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("name/{nameId}")]
        public async Task<ActionResult> GetByName(string nameId){
            var result = await _context.coursesData
            .Include(c => c.courseName)
            .Where(w => w.courseName.name!.ToUpper().Trim()== nameId.ToUpper().Trim())
            .Select(v => new CoursesListViewModel{
                Id = v.Id,
                courseNumber = v.courseNumber,
                nameOfCourse = v.courseName.name ?? "",
                startDate = v.startDate,
                endDate = v.endDate,
                teacher = v.teacher,
                placeStudy = v.placeStudy
            })
            .ToListAsync();
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpGet("startdate/{startDate}")]
        public async Task<ActionResult> GetByStartDate(string startDate){
            var result = await _context.coursesData
            .Include(c => c.courseName)
            .Where(w => w.startDate!.ToUpper().Trim()== startDate.ToUpper().Trim())
            .Select(v => new CoursesListViewModel{
                Id = v.Id,
                courseNumber = v.courseNumber,
                nameOfCourse = v.courseName.name ?? "",
                startDate = v.startDate,
                endDate = v.endDate,
                teacher = v.teacher,
                placeStudy = v.placeStudy
            })
            .ToListAsync();
            return Ok(result);
        }

        //---------------------------------------------------

        [HttpPost("addCourse")]
        public async Task<ActionResult>  AddCourse(CourseAddViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to store the course in the system");
            var exists = await _context.coursesData.SingleOrDefaultAsync(c=> c.Id == model.Id);
            if (exists is not null) return BadRequest($"We have already registered a course with courseId {model.Id}");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.nameOfCourse.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.nameOfCourse} in our system");

            var course = new CoursesModel{
                Id = model.Id,
                courseNumber = model.courseNumber,
                courseName = CourseName,
                startDate = model.startDate,
                endDate = model.endDate,
                teacher = model.teacher,
                placeStudy = model.placeStudy
            };
            await _context.coursesData.AddAsync(course);
            if (await _context.SaveChangesAsync() > 0){
                return Created(nameof(GetById), new {Id = course.Id});
            }
            return StatusCode(500, "Internet Server Error");
        }

        //---------------------------------------------------

        [HttpPut("update/{Id}")]
        public async Task<ActionResult> UpdateCourse(int Id, CourseUpdateViewModel model){
            if (!ModelState.IsValid) return BadRequest("Information is missing to be able to updater the course in the system");

            var course = await _context.coursesData.FindAsync(Id);
            if (course is  null) return BadRequest($"We cannot find a course in the system with this CourseId ");

            var CourseName = await _context.coursesNameData.SingleOrDefaultAsync(c => c.name!.ToUpper().Trim() == model.nameOfCourse.ToUpper().Trim());
            if(CourseName is null) return NotFound ($"We could not find any NameOfCourse with the name {model.nameOfCourse} in our system");

                course.courseNumber = model.courseNumber;
                course.courseName = CourseName;
                course.startDate = model.startDate;
                course.endDate = model.endDate;
                course.teacher = model.teacher;
                course.placeStudy = model.placeStudy;

                _context.coursesData.Update(course);
            if (await _context.SaveChangesAsync() > 0){
                return NoContent();
            }
            return StatusCode(500, "Internet Server Error");
        }

        //-----------------------------------------------------

        [HttpPatch("fullybooked/{Id}")]
        //go to database for mark course as fully booked
        public async Task<ActionResult> MarkAsFullyBooked(int Id){
            var course = await _context.coursesData.FindAsync(Id);
            if (course is null) return NotFound($"We can't find any course with courseID: {Id}");
            course.status = CourseStatusEnum.fullybooked;
            _context.coursesData.Update(course);
            if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }
        return StatusCode(500, "Internal Server Error");
            
        }

        //------------------------------------------------------
        
        [HttpPatch("available/{Id}")]
        public async Task<ActionResult> MarkAsAvailable(int Id){
            var course = await _context.coursesData.FindAsync(Id);
            if (course is null) return NotFound($"We can't find any course with courseID: {Id}");
            course.status = CourseStatusEnum.Available;
            _context.coursesData.Update(course);
            if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }
        return StatusCode(500, "Internal Server Error");
        }
        //---------------------------------------------------
        }