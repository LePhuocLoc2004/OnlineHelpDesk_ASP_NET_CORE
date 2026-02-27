using OnlineHelpDesk_ASP_NET_CORE.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.ViewModels
{
    // ViewModels/YeuCauFilterVM.cs
    public class YeuCauFilterVM
    {
        [DataType(DataType.Date)]
        public DateTime? TuNgay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DenNgay { get; set; }

        public int? MaDoUuTien { get; set; }

        public List<YeuCau> KetQua { get; set; } = new();
        public List<DoUuTien> DanhSachDoUuTien { get; set; } = new();
    }

}
