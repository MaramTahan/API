
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;

namespace westcoast_education2.api.Controllers
{
    [ApiController]
    [Route("api/c1/courseName")]
    public class NameOfCourseController : ControllerBase, IBaseApiControllers
    {
        private readonly WestCoastEducationContext _context;
        public NameOfCourseController(WestCoastEducationContext context){
            _context = context;
        }

        [HttpGet("listall")]
        public async Task<ActionResult> ListAll(){
            var result = await _context.coursesNameData
            .Select(c => new{
                Id = c.Id,
                name = c.name
            })
            .ToListAsync();
            
            return Ok(result);
        }
        //--------------------------------------------------

        [HttpGet("getbyId/{id}")]
        public async Task<ActionResult> GetById(int id){
            var result = await _context.coursesNameData
            .Select(c => new{
                Id = c.Id,
                name = c.name
            })
            .SingleOrDefaultAsync(c => c.Id == id);
            return Ok(result);
        }
        //--------------------------------------------------

        [HttpGet("getbyname/{name}/courses")]
        public async Task<ActionResult> List(string name){
            var result = await _context.coursesNameData
            .Select(c => new {
                Id = c.Id,
                name = c.name,

                Courses = c.Courses.Select(v => new{
                    Id = v.Id,
                    name = $"{v.courseName.name} {v.teacher} {v.placeStudy} {v.startDate} {v.endDate}"
                }).ToList(),


                StudentsCourses = c.StudentsCourses.Select(v => 
                new{
                    userId = v.userId,
                    name = $"{v.firstName} {v.lastName} {v.personNu} {v.courseName.name}"

                }).ToList(),


                TeachersCourses = c.TeachersCourses.Select(v => 
                new{
                    TUserId = v.TUserId,
                    name = $"{v.firstName} {v.lastName}  {v.courseName.name}"

                }).ToList()
            })
            .SingleOrDefaultAsync(n => n.name.ToUpper().Trim() == name.ToUpper().Trim());
            return Ok (result);
        }
        //--------------------------------------------------

        [HttpPost("addNameOfCourse")]
        public async Task<ActionResult> Add(string name){
            if(await _context.coursesNameData.SingleOrDefaultAsync(
                n => n.name.ToLower().Trim() == name.ToLower().Trim()) is not null)
                {
                    return BadRequest ($"We have already registered a course with name {name} ");
                }
                var courseName = new CourseNameModel{ name = name.Trim()};
                await _context.coursesNameData.AddAsync(courseName);
                if(await _context.SaveChangesAsync() > 0)
                {
                    return CreatedAtAction(nameof(GetById),
                    new {Id = courseName.Id},
                    new {Id = courseName.Id, name = courseName.name}
                    );
                }
                return StatusCode (500, "Internet Server Error");
        }
        //--------------------------------------------------

        [HttpPut("updateNameOfCourse/{Id}")]
        public async Task<ActionResult> Update(int Id, string name){
            var courseName = await _context.coursesNameData.FindAsync(Id);
            if (courseName is  null) return NotFound($"We cannot find a course in the system with this name ");

            courseName.name = name;
            _context.coursesNameData.Update(courseName);
            if (await _context.SaveChangesAsync() > 0){
                return NoContent();
            }
            return StatusCode(500, "Internet Server Error");
        }
        //--------------------------------------------------

        [HttpDelete("deleteNameOfCourse/{Id}")]
        public async Task<ActionResult> Delete(int Id){
            var courseName = await _context.coursesNameData.FindAsync(Id);
            if (courseName is null) return NotFound($"We can't find any course with this Id {Id}");

            _context.coursesNameData.Remove(courseName);
            if (await _context.SaveChangesAsync() > 0)
        {
            return NoContent();
        }
        return StatusCode(500, "Internal Server Error");
        }
        //---------------------------------------------------

  Task<IActionResult> IBaseApiControllers.Add(string name)
  {
   throw new NotImplementedException();
  }

  Task<IActionResult> IBaseApiControllers.Delete(int Id)
  {
   throw new NotImplementedException();
  }

  Task<IActionResult> IBaseApiControllers.GetById(int Id)
  {
   throw new NotImplementedException();
  }

  Task<IActionResult> IBaseApiControllers.ListAll()
  {
   throw new NotImplementedException();
  }

  Task<IActionResult> IBaseApiControllers.List(string name)
  {
   throw new NotImplementedException();
  }

  Task<IActionResult> IBaseApiControllers.Update(int Id, string name)
  {
   throw new NotImplementedException();
  }
  //---------------------------------------------------

 }
}