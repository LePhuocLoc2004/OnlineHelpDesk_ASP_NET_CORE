using System.ComponentModel.DataAnnotations;

namespace OnlineHelpDesk_ASP_NET_CORE.Models
{
    public class DoUuTien
    {
        [Key]
        public int MaDoUuTien { get; set; }
        public string TenDoUuTien { get; set; }

        public ICollection<YeuCau> YeuCaus { get; set; }
    }


}
