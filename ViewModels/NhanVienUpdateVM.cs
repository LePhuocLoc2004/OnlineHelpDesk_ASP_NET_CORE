using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.ViewModels
{
    public class NhanVienUpdateVM
    {
        [Required]
        public string Hoten { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime? Ngaysinh { get; set; }

        public IFormFile? ImageUpload { get; set; }
    }
}
