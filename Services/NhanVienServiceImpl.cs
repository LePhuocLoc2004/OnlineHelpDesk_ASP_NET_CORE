using OnlineHelpDesk_ASP_NET_CORE.Models;
using Microsoft.EntityFrameworkCore;

public class NhanVienServiceImpl : NhanVienService
{
    private readonly AppDbContext _context;

    public NhanVienServiceImpl(AppDbContext context)
    {
        _context = context;
    }

    public NhanVien? Login(string username, string password)
    {
        return _context.NhanViens
            .FirstOrDefault(u => u.Username == username && u.Password == password && u.Kichhoat == true);
    }


    public void Create(NhanVien nv)
    {
        if (_context.NhanViens.Any(x => x.Username == nv.Username))
            throw new Exception("Tài khoản đã tồn tại");

        _context.NhanViens.Add(nv);
        _context.SaveChanges();
    }

}
