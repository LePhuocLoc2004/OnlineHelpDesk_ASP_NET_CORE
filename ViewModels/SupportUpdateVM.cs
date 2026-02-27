using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.ViewModels
{
        public class SupportUpdateVM
        {
            [Required]
            public string Hoten { get; set; }

            [DataType(DataType.Date)]
            public DateTime? Ngaysinh { get; set; }

            [DataType(DataType.Password)]
            public string? NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp.")]
            public string? ConfirmPassword { get; set; }

            public IFormFile? ImageUpload { get; set; }
            public string? HinhanhHienTai { get; set; }
        }
    }




