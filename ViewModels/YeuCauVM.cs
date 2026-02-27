using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.ViewModels
{
    public class YeuCauVM
    {
        [Required]
        public string Tieude { get; set; }

        [Required]
        public string Noidung { get; set; }

        [Required]
        public int MaDoUuTien { get; set; }
    }
}
