using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHelpDesk_ASP_NET_CORE.Models
{
    public class YeuCau
    {
        [Key]
        public int MaYeuCau { get; set; }

        public string Tieude { get; set; }
        public string Noidung { get; set; }
        public DateTime Ngaygui { get; set; }

        public int? MaDoUuTien { get; set; }
        [ForeignKey("MaDoUuTien")]
        public DoUuTien DoUuTien { get; set; }

        public string Manv_Gui { get; set; }
        [ForeignKey("Manv_Gui")]
        public NhanVien NhanVienGui { get; set; }

        public string? Manv_XuLy { get; set; } 
        [ForeignKey("Manv_XuLy")]
        public NhanVien NhanVienXuLy { get; set; }
    }
}
