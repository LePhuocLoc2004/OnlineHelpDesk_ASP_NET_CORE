using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.ViewModels
{
    public class NhanVienCreateVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Hoten { get; set; }

        public DateTime? Ngaysinh { get; set; }

        [Required]
        [Range(1, 3)]
        public int Quyen { get; set; }

        public IFormFile? ImageUpload { get; set; }
    }
}
