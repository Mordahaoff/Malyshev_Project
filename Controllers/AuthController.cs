using Malyshev_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Malyshev_Project.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly PostgresContext _db;

    public AuthController(ILogger<AuthController> logger, PostgresContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(UserAuthModel model)
    {
        var user = _db.Users.FirstOrDefault(u => u.Login == model.Login);

        if (user == null)
        {
            string message = $"Пользователь с логином [{model.Login}] не найден!";
			_logger.LogWarning(message);
            ViewData["Message"] = message;
			return RedirectToAction("Login", "Auth");
        }

        if (user.Password != model.Password)
        {
			string message = "Пароль не совпадает!";
			_logger.LogWarning(message);
			ViewData["Message"] = message;
			return RedirectToAction("Login", "Auth");
        }

        HttpContext.Session.Set<User>("user", user);

        return RedirectToAction("User", "Profile");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserAuthModel model)
    {
        if (_db.Users.FirstOrDefault(u => u.Login == model.Login) != null)
        {
            _logger.LogWarning($"Логин {model.Login} занят!");
            ViewData["Message"] = $"Логин {model.Login} занят!";
            return RedirectToAction("Register", "Auth");
        }

        User user = new User
        {
            Login = model.Login,
            Email = model.Email,
            Password = model.Password,
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        _logger.LogInformation($"Пользователь [{model.Login}] [{model.Email}] [{model.Password}] зарегистрирован!");

        return RedirectToAction("Login", "Auth");
    }
}
