using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.ViewModels;

namespace OnlineHelpDesk_ASP_NET_CORE.Controllers
{
    public class SupportController : Controller
    {
        private readonly AppDbContext _context;

        public SupportController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(DateTime? tuNgay, DateTime? denNgay, int? doUuTien)
        {
            var quyen = HttpContext.Session.GetInt32("Quyen");
            if (quyen != 2)
                return RedirectToAction("Login", "Account");

            var username = HttpContext.Session.GetString("Username");

            var query = _context.YeuCaus
                .Include(y => y.NhanVienGui)
                .Include(y => y.DoUuTien)
                .Where(y => y.Manv_XuLy == username);

            if (tuNgay.HasValue)
                query = query.Where(y => y.Ngaygui >= tuNgay.Value);

            if (denNgay.HasValue)
                query = query.Where(y => y.Ngaygui <= denNgay.Value);

            if (doUuTien.HasValue)
                query = query.Where(y => y.MaDoUuTien == doUuTien.Value);

            ViewBag.DoUuTienList = _context.DoUuTiens.ToList();
            ViewBag.Username = username;

            return View(query.OrderByDescending(y => y.Ngaygui).ToList());
        }

        [HttpGet]
        public IActionResult CapNhatThongTin()
        {
            var username = HttpContext.Session.GetString("Username");
            var nv = _context.NhanViens.FirstOrDefault(n => n.Username == username);
            if (nv == null || HttpContext.Session.GetInt32("Quyen") != 2)
                return RedirectToAction("Login", "Account");

            var vm = new SupportUpdateVM
            {
                Hoten = nv.Hoten,
                Ngaysinh = nv.Ngaysinh,
                HinhanhHienTai = nv.Hinhanh
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatThongTin(SupportUpdateVM model)
        {
            var username = HttpContext.Session.GetString("Username");
            var nv = _context.NhanViens.FirstOrDefault(n => n.Username == username);
            if (nv == null || HttpContext.Session.GetInt32("Quyen") != 2)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(model);

            nv.Hoten = model.Hoten;
            nv.Ngaysinh = model.Ngaysinh;

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                nv.Password = model.NewPassword;
            }

            if (model.ImageUpload != null)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageUpload.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                model.ImageUpload.CopyTo(stream);

                nv.Hinhanh = "/img/" + fileName;
            }

            _context.SaveChanges();
            TempData["Success"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("CapNhatThongTin");
        }


    }
}
