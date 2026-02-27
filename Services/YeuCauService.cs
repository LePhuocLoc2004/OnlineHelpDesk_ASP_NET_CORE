using OnlineHelpDesk_ASP_NET_CORE.Models;

public interface YeuCauService
{
    List<YeuCau> GetByNhanVien(string manv_gui);
    YeuCau? GetById(int id);
    void Create(YeuCau yeuCau);
    void Update(YeuCau yeuCau);
}
