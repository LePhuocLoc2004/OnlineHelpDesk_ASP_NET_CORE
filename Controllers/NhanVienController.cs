using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.Models;
using OnlineHelpDesk_ASP_NET_CORE.ViewModels;

namespace OnlineHelpDesk_ASP_NET_CORE.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GuiYeuCau()
        {
            if (HttpContext.Session.GetInt32("Quyen") != 1)
                return RedirectToAction("Login", "Account");

            ViewBag.DoUuTiens = _context.DoUuTiens.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GuiYeuCau(YeuCauVM model)
        {
            if (HttpContext.Session.GetInt32("Quyen") != 1)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.DoUuTiens = _context.DoUuTiens.ToList();
                return View(model);
            }

            var username = HttpContext.Session.GetString("Username");

            var yeuCau = new YeuCau
            {
                Tieude = model.Tieude,
                Noidung = model.Noidung,
                MaDoUuTien = model.MaDoUuTien,
                Manv_Gui = username,
                Ngaygui = DateTime.Now,
                Manv_XuLy = null // chưa có người xử lý
            };

            _context.YeuCaus.Add(yeuCau);
            _context.SaveChanges();

            TempData["Success"] = "Yêu cầu đã được gửi!";
            return RedirectToAction("GuiYeuCau");
        }

        [HttpGet]
        public IActionResult LichSuYeuCau(DateTime? tuNgay, DateTime? denNgay, int? doUuTien)
        {
            if (HttpContext.Session.GetInt32("Quyen") != 1)
                return RedirectToAction("Login", "Account");

            var username = HttpContext.Session.GetString("Username");

            var query = _context.YeuCaus
                .Include(y => y.DoUuTien)
                .Where(y => y.Manv_Gui == username); // ✅ Chỉ yêu cầu của nhân viên đang đăng nhập

            if (tuNgay.HasValue)
                query = query.Where(y => y.Ngaygui >= tuNgay.Value);
            if (denNgay.HasValue)
                query = query.Where(y => y.Ngaygui <= denNgay.Value);
            if (doUuTien.HasValue)
                query = query.Where(y => y.MaDoUuTien == doUuTien.Value);

            var danhSach = query
                .OrderBy(y => y.Ngaygui) // ✅ Sắp xếp từ cũ đến mới
                .ToList();

            ViewBag.DoUuTienList = _context.DoUuTiens.ToList();
            return View(danhSach);
        }
        [HttpGet]
        public IActionResult CapNhatTaiKhoan()
        {
            if (HttpContext.Session.GetInt32("Quyen") != 1)
                return RedirectToAction("Login", "Account");

            var username = HttpContext.Session.GetString("Username");
            var nv = _context.NhanViens.FirstOrDefault(x => x.Username == username);
            if (nv == null) return NotFound();

            var vm = new NhanVienUpdateVM
            {
                Hoten = nv.Hoten,
                Password = nv.Password,
                Ngaysinh = nv.Ngaysinh
            };

            ViewBag.AnhCu = nv.Hinhanh;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatTaiKhoan(NhanVienUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AnhCu = HttpContext.Session.GetString("Hinhanh");
                return View(model);
            }

            var username = HttpContext.Session.GetString("Username");
            var nv = _context.NhanViens.FirstOrDefault(x => x.Username == username);
            if (nv == null) return NotFound();

            nv.Hoten = model.Hoten;
            nv.Password = model.Password;
            nv.Ngaysinh = model.Ngaysinh;

            // Ảnh đại diện
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
                HttpContext.Session.SetString("Hinhanh", nv.Hinhanh);
            }

            _context.SaveChanges();
            TempData["Success"] = "Cập nhật tài khoản thành công!";
            return RedirectToAction("CapNhatTaiKhoan");
        }

    }
}