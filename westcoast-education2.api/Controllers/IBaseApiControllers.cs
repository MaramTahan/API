using Microsoft.AspNetCore.Mvc;

namespace westcoast_education2.api.Controllers;

    public interface IBaseApiControllers
    {
        Task<IActionResult> Add(string name);
        Task<IActionResult> Delete(int Id);
        Task<IActionResult> GetById(int Id);
        Task<IActionResult> ListAll();
        Task<IActionResult> List(string name);
        Task<IActionResult> Update(int Id, string name);
    }
