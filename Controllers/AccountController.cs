using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineHelpDesk_ASP_NET_CORE.Models;

public class AccountController : Controller
{
    private readonly NhanVienService _nhanVienService;

    public AccountController(NhanVienService nhanVienService)
    {
        _nhanVienService = nhanVienService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        // Nếu đã đăng nhập thì điều hướng theo quyền
        if (HttpContext.Session.GetString("Username") != null)
        {
            var role = HttpContext.Session.GetInt32("Quyen") ?? 0;
            return role switch
            {
                3 => RedirectToAction("Index", "Admin"),
                2 => RedirectToAction("Index", "Support"),
                1 => RedirectToAction("GuiYeuCau", "NhanVien"),
                _ => View()
            };
        }

        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _nhanVienService.Login(username, password);

        if (user == null)
        {
            ViewBag.Error = "nhap sai";
            return View();
        }

        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetInt32("Quyen", user.Quyen);

        return user.Quyen switch
        {
            3 => RedirectToAction("Index", "Admin"),
            2 => RedirectToAction("Index", "Support"),
            1 => RedirectToAction("GuiYeuCau", "NhanVien"),
            _ => RedirectToAction("Login")
        };
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
