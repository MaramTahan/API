using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using westcoast_education2.api.Models;
using westcoast_education2.api.ViewModels.Account;

namespace westcoast_education2.api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    public AccountController(UserManager<UserModel> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model){
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized();
            }
            return Ok(user);
        }
}
