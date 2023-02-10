

using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Models;

namespace westcoast_education2.api.Data;

public class WestCoastEducationContext : DbContext
{
    public DbSet<CoursesModel> coursesData => Set<CoursesModel>();
    public DbSet<CourseNameModel> coursesNameData => Set<CourseNameModel>();
    public DbSet<StudentsModel> studentData => Set<StudentsModel>();
    public DbSet<TeachersModel> teacherData => Set<TeachersModel>();
 public WestCoastEducationContext(DbContextOptions options) : base(options)
 {
 }
}
