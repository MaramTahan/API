
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using westcoast_education.web.Models;
using westcoast_education.web.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace westcoast_education.web.Controllers;

    [Route("courses")]
    public class CoursesController : Controller
    {
  private readonly IConfiguration _config;
 private readonly string _baseUrl;
 private readonly JsonSerializerOptions _options;
 private readonly IHttpClientFactory _httpClient;
 public CoursesController(IConfiguration config, IHttpClientFactory httpClient)
 {
   _httpClient = httpClient;
   _config = config;
   _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
   _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
 }

 public async Task<IActionResult> Index()
        {
          using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses/listall");

            if (!response.IsSuccessStatusCode) return Content("Oops something wrong!");
            var json = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<IList<CoursesListViewModel>>(json, _options);
            return View("Index", courses);
        }
        //---------------------------------------------------

        [HttpGet("create")]
        public async Task<IActionResult> Create(){
          var courseNameList = new List<SelectListItem>();

          using var client = _httpClient.CreateClient();

          var response = await client.GetAsync($"{_baseUrl}/courseName/listall");
          if (!response.IsSuccessStatusCode) return Content("Oops something wrong!!!");
            var json = await response.Content.ReadAsStringAsync();
            var courseName = JsonSerializer.Deserialize<List<CoursesSettings>>(json, _options);

              foreach (var make in courseName){
              courseNameList.Add(new SelectListItem { Value = make.name, Text = make.name });
              }

              var course = new CoursesPostViewModel();
              course.nameOfCourses= courseNameList;
              return View ("Create", course);
        }
        //---------------------------------------------------

        [HttpPost("create")]
        public async Task<IActionResult> Create(CoursesPostViewModel course){
          if (!ModelState.IsValid) return View("Create", course);
          var model = new {
            courseNumber = course.courseNumber,
            nameOfCourse = course.nameOfCourse,
            startDate = course.startDate,
            endDate = course.endDate,
            teacher = course.teacher,
            placeStudy = course.placeStudy
          };

          using var client = _httpClient.CreateClient();
          var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Application.Json);
          var response = await client.PostAsync($"{_baseUrl}/courses/addCourse", content);

          if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return Content("Done!");

        }

    }
