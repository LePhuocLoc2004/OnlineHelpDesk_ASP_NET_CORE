using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineHelpDesk_ASP_NET_CORE.Models;
using System;
using System.Linq;
using OnlineHelpDesk_ASP_NET_CORE.Services;
using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.ViewModels;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    // Trang chính của Admin
    public IActionResult Index()
    {
        // Kiểm tra quyền admin (1)
        var quyen = HttpContext.Session.GetInt32("Quyen");
        if (quyen != 3)
            return RedirectToAction("Login", "Account");

        return View(); // Views/Admin/Index.cshtml
    }

    // Danh sách tất cả nhân viên (bao gồm cả support)
    public IActionResult DanhSachNhanVien()
    {
        var quyen = HttpContext.Session.GetInt32("Quyen");
        if (quyen != 3)
            return RedirectToAction("Login", "Account");

        var list = _context.NhanViens.ToList(); // lấy toàn bộ nhân viên
        return View(list); // Views/Admin/DanhSachNhanVien.cshtml
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        if (HttpContext.Session.GetInt32("Quyen") != 3)
            return RedirectToAction("Login", "Account");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateUser(NhanVienCreateVM model)
    {
        if (HttpContext.Session.GetInt32("Quyen") != 3)
            return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid)
            return View(model);

        // Kiểm tra username đã tồn tại
        if (_context.NhanViens.Any(n => n.Username == model.Username))
        {
            ModelState.AddModelError("Username", "Tài khoản đã tồn tại.");
            return View(model);
        }

        // Mapping từ ViewModel sang Entity
        var nv = new NhanVien
        {
            Username = model.Username,
            Password = model.Password,
            Hoten = model.Hoten,
            Ngaysinh = model.Ngaysinh,
            Quyen = model.Quyen,
            Kichhoat = true
        };

        // Xử lý ảnh
        if (model.ImageUpload != null)
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageUpload.FileName);
            string path = Path.Combine(folder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.ImageUpload.CopyTo(stream);
            }

            nv.Hinhanh = "/img/" + fileName;
        }

        _context.NhanViens.Add(nv);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
        return RedirectToAction("Index", "Admin");
    }

}
