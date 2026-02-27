using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.Models;

namespace OnlineHelpDesk_ASP_NET_CORE.Services
{
    public class YeuCauServiceImpl : YeuCauService
    {
        private readonly AppDbContext _context;
        public YeuCauServiceImpl(AppDbContext context)
        {
            _context = context;
        }

        public List<YeuCau> GetByNhanVien(string username)
        {
            return _context.YeuCaus
                .Include(y => y.DoUuTien)
                .Include(y => y.NhanVienGui)
                .Include(y => y.NhanVienXuLy)
                .Where(y => y.Manv_Gui == username) 
                .ToList();
        }


        public YeuCau? GetById(int id)
        {
            return _context.YeuCaus
                .Include(y => y.DoUuTien)
                .Include(y => y.NhanVienGui)
                .Include(y => y.NhanVienXuLy)
                .FirstOrDefault(y => y.MaYeuCau == id);
        }

        public void Create(YeuCau yeuCau)
        {
            _context.YeuCaus.Add(yeuCau);
            _context.SaveChanges();
        }

        public void Update(YeuCau yeuCau)
        {
            _context.YeuCaus.Update(yeuCau);
            _context.SaveChanges();
        }
    }

}
