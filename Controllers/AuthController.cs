using Malyshev_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        if (user.Email != model.Email)
        {
            string message = "Почта не совпадает!";
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
        //var claims = new List<Claim> { new Claim(ClaimTypes.Role, _db.Roles.First(r => r.IdRole == user.RoleId).Name) };
        //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("MyProfile", "User");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserAuthModel model)
    {
        if (_db.Users.Any(u => u.Login == model.Login))
        {
            _logger.LogWarning($"Логин {model.Login} занят!");
            ViewData["Message"] = $"Логин {model.Login} занят!";
            return RedirectToAction("Register", "Auth");
        }

        var user = new User
        {
            Login = model.Login!,
            Email = model.Email,
            Password = model.Password!,
        };

        _db.Users.Add(user);
        _db.SaveChanges();

        _logger.LogInformation($"Пользователь [{model.Login}] [{model.Email}] [{model.Password}] зарегистрирован!");

        return RedirectToAction("Login", "Auth");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }
}
