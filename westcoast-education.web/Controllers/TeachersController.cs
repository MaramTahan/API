using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using westcoast_education.web.Models;
using westcoast_education.web.ViewModels.Teachers;
using static System.Net.Mime.MediaTypeNames;

namespace westcoast_education.web.Controllers;

    [Route("teachers")]
    public class TeachersController : Controller
    {
  private readonly IConfiguration _config;
 private readonly string _baseUrl;
 private readonly JsonSerializerOptions _options;
 private readonly IHttpClientFactory _httpClient;
 public TeachersController(IConfiguration config, IHttpClientFactory httpClient)
 {
   _httpClient = httpClient;
   _config = config;
   _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
   _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
 }

 public async Task<IActionResult> Index()
        {
          using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/teachers");

            if (!response.IsSuccessStatusCode) return Content("Oops something wrong!");
            var json = await response.Content.ReadAsStringAsync();
            var teacher = JsonSerializer.Deserialize<IList<TeacherListViewModel>>(json, _options);
            return View("Index", teacher);
        }
        //---------------------------------------------------

        [HttpGet("create")]
        public async Task<IActionResult> Create(){
          var courseNameList = new List<SelectListItem>();

          using var client = _httpClient.CreateClient();

          var response = await client.GetAsync($"{_baseUrl}/courseName/listall");
          if (!response.IsSuccessStatusCode) return Content("Oops something wrong!!!");
            var json = await response.Content.ReadAsStringAsync();
            var courseName = JsonSerializer.Deserialize<List<TeachersSettings>>(json, _options);

              foreach (var make in courseName){
              courseNameList.Add(new SelectListItem { Value = make.name, Text = make.name });
              }

              var teacher = new TeacherPostViewModel();
              teacher.coursesTaughtName = courseNameList;
              return View ("Create", teacher);
        }
        //---------------------------------------------------

        [HttpPost("create")]
        public async Task<IActionResult> Create(TeacherPostViewModel teacher){
          if (!ModelState.IsValid) return View("Create", teacher);
          var model = new {
            firstName = teacher.firstName,
            lastName = teacher.lastName,
            email = teacher.email,
            phoneNumber = teacher.phoneNumber,
            address = teacher.address,
            coursesTaughtName = teacher.coursesTaughtName
          };

          using var client = _httpClient.CreateClient();
          var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Application.Json);
          var response = await client.PostAsync($"{_baseUrl}/teachers", content);

          if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return Content("Done!");
        }
        //---------------------------------------------------

    }
