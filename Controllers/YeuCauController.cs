using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.Models;
using System;
using System.Linq;

public class YeuCauController : Controller
{
    private readonly AppDbContext _context;

    public YeuCauController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ A. Xem danh sách yêu cầu theo nhân viên
    public IActionResult XemTheoNhanVien(string username)
    {
        var quyen = HttpContext.Session.GetInt32("Quyen");
        if (quyen != 3)
            return RedirectToAction("Login", "Account");

        var yeuCaus = _context.YeuCaus
            .Where(y => y.Manv_Gui == username)
            .Include(y => y.DoUuTien)
            .Include(y => y.NhanVienXuLy)
            .OrderByDescending(y => y.Ngaygui)
            .ToList();

        ViewBag.TenNhanVien = _context.NhanViens
            .Where(n => n.Username == username)
            .Select(n => n.Hoten)
            .FirstOrDefault() ?? username;

        return View(yeuCaus);
    }

    // ✅ B. Danh sách yêu cầu có lọc theo ngày và độ ưu tiên
    public IActionResult DanhSach(DateTime? tuNgay, DateTime? denNgay, int? doUuTien)
    {
        var quyen = HttpContext.Session.GetInt32("Quyen");
        if (quyen != 3)
            return RedirectToAction("Login", "Account");

        var query = _context.YeuCaus
            .Include(y => y.NhanVienGui)
            .Include(y => y.NhanVienXuLy)
            .Include(y => y.DoUuTien)
            .AsQueryable();

        if (tuNgay.HasValue)
            query = query.Where(y => y.Ngaygui >= tuNgay.Value);

        if (denNgay.HasValue)
            query = query.Where(y => y.Ngaygui <= denNgay.Value);

        if (doUuTien.HasValue)
            query = query.Where(y => y.MaDoUuTien == doUuTien.Value);

        // 👉 Sắp xếp ngày tăng dần (từ bé đến lớn)
        var danhSach = query.OrderBy(y => y.Ngaygui).ToList();

        ViewBag.Supports = _context.NhanViens.Where(x => x.Quyen == 2).ToList();
        ViewBag.DoUuTienList = _context.DoUuTiens.ToList();

        return View(danhSach);
    }


    // ✅ C. Gán yêu cầu cho nhân viên xử lý (support)
    [HttpPost]
    public IActionResult GanXuLy(int maYeuCau, string manvXuLy)
    {
        var yc = _context.YeuCaus.FirstOrDefault(y => y.MaYeuCau == maYeuCau);

        if (yc == null || string.IsNullOrEmpty(manvXuLy))
        {
            TempData["Error"] = "Thông tin không hợp lệ!";
            return RedirectToAction("DanhSach");
        }

        yc.Manv_XuLy = manvXuLy;
        _context.SaveChanges();

        TempData["Success"] = "Đã gán yêu cầu thành công!";
        return RedirectToAction("DanhSach");
    }
}
