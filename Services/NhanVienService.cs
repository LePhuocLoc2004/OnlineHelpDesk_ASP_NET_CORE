using OnlineHelpDesk_ASP_NET_CORE.Models;

public interface NhanVienService
{
    NhanVien? Login(string username, string password);
    void Create(NhanVien nv); 
}
