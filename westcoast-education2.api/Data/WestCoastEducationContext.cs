

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Models;

namespace westcoast_education2.api.Data;

public class WestCoastEducationContext : IdentityDbContext<UserModel>
{
    public DbSet<CoursesModel> coursesData { get; set; }
    public DbSet<CourseNameModel> coursesNameData => Set<CourseNameModel>();
    public DbSet<StudentsModel> studentData { get; set; }
    public DbSet<TeachersModel> teacherData { get; set; }
    public WestCoastEducationContext(DbContextOptions options) : base(options)
    {
    }
}
