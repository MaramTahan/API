
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using westcoast_education.web.Models;
using westcoast_education.web.ViewModels.Students;
using static System.Net.Mime.MediaTypeNames;

namespace westcoast_education.web.Controllers;

    [Route("students")]
    public class StudentsController : Controller
    {
  private readonly IConfiguration _config;
 private readonly string _baseUrl;
 private readonly JsonSerializerOptions _options;
 private readonly IHttpClientFactory _httpClient;
 public StudentsController(IConfiguration config, IHttpClientFactory httpClient)
 {
   _httpClient = httpClient;
   _config = config;
   _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
   _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
 }

 public async Task<IActionResult> Index()
        {
          using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/students/listall");

            if (!response.IsSuccessStatusCode) return Content("Oops something wrong!");
            var json = await response.Content.ReadAsStringAsync();
            var student = JsonSerializer.Deserialize<IList<StudentListViewModel>>(json, _options);
            return View("Index", student);
        }
        //---------------------------------------------------

        [HttpGet("create")]
        public async Task<IActionResult> Create(){
          var courseNameList = new List<SelectListItem>();

          using var client = _httpClient.CreateClient();

          var response = await client.GetAsync($"{_baseUrl}/courseName/listall");
          if (!response.IsSuccessStatusCode) return Content("Oops something wrong!!!");
            var json = await response.Content.ReadAsStringAsync();
            var courseName = JsonSerializer.Deserialize<List<StudentsSettings>>(json, _options);

              foreach (var make in courseName){
              courseNameList.Add(new SelectListItem { Value = make.name, Text = make.name });
              }

              var student = new StudentPostViewModel();
              student.coursesTakenNames = courseNameList;
              return View ("Create", student);
        }
        //---------------------------------------------------

        [HttpPost("create")]
        public async Task<IActionResult> Create(StudentPostViewModel student){
          if (!ModelState.IsValid) return View("Create", student);
          var model = new {
            firstName = student.firstName,
            lastName = student.lastName,
            personNu = student.personNu,
            email = student.email,
            phoneNumber = student.phoneNumber,
            address = student.address,
            coursesTakenName = student.coursesTakenName
          };

          using var client = _httpClient.CreateClient();
          var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Application.Json);
          var response = await client.PostAsync($"{_baseUrl}/students/addstudent", content);

          if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return Content("Done!");
        }
        //---------------------------------------------------
    }
