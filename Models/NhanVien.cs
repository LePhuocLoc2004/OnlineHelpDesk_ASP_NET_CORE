using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineHelpDesk_ASP_NET_CORE.Models;

namespace OnlineHelpDesk_ASP_NET_CORE.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Hoten { get; set; }

        public DateTime? Ngaysinh { get; set; }
        public bool? Kichhoat { get; set; } = true;
        public string? Hinhanh { get; set; }
        [NotMapped]
        public IFormFile? ImageUpload { get; set; } 

        [Required]
        [Range(1, 3)]
        public int Quyen { get; set; }

        [ValidateNever]
        public ICollection<YeuCau> YeuCausGui { get; set; } = new List<YeuCau>();

        [ValidateNever]
        public ICollection<YeuCau> YeuCausXuLy { get; set; } = new List<YeuCau>();
    }
}