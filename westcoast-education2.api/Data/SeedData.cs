
using System.Text.Json;
using westcoast_education2.api.Models;

namespace westcoast_education2.api.Data;

    public static class SeedData
    {
        public static async Task LoadCoursesData(WestCoastEducationContext context){
        var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };


       // Only want to load data if the database table is empty...
    if (context.coursesData.Any()) return;


       // Loading the json data...
    var json = System.IO.File.ReadAllText("Data/json/courses.json");


       // Convert the json objects to a list of Vehicle objects...
    var coursesList = JsonSerializer.Deserialize<List<CoursesModel>>
        (json, options);


    if (coursesList is not null && coursesList.Count > 0)
    {
        await context.coursesData.AddRangeAsync(coursesList);
        await context.SaveChangesAsync();
    }
    }
//------------------------------------------------------
public static async Task LoadCoursesNameData(WestCoastEducationContext context){
        var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };


    // Only want to load data if the database table is empty...
    if (context.coursesNameData.Any()) return;


    // Loading the json data...
    var json = System.IO.File.ReadAllText("Data/json/courseName.json");


    // Convert the json objects to a list of Vehicle objects...
    var coursesNameList = JsonSerializer.Deserialize<List<CourseNameModel>>
        (json, options);


    if (coursesNameList is not null && coursesNameList.Count > 0)
    {
        await context.coursesNameData.AddRangeAsync(coursesNameList);
        await context.SaveChangesAsync();
    }
    }
//------------------------------------------------------
 public static async Task LoadStudentData(WestCoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (context.studentData.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/students.json");

        var studentList = JsonSerializer.Deserialize<List<StudentsModel>>
        (json, options);


        if (studentList is not null && studentList.Count > 0)
        {
            await context.studentData.AddRangeAsync(studentList);
            await context.SaveChangesAsync();
        }
    }
   // //-----------------------------------------------------


    public static async Task LoadTeacherData(WestCoastEducationContext context)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        if (context.teacherData.Any()) return;


        var json = System.IO.File.ReadAllText("Data/json/teachers.json");

        var TeacherList = JsonSerializer.Deserialize<List<TeachersModel>>
        (json, options);


        if (TeacherList is not null && TeacherList.Count > 0)
        {
            await context.teacherData.AddRangeAsync(TeacherList);
            await context.SaveChangesAsync();
        }
    }


    }
