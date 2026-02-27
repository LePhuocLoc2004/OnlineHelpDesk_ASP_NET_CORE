using Microsoft.EntityFrameworkCore;
using OnlineHelpDesk_ASP_NET_CORE.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<NhanVien> NhanViens { get; set; }
    public DbSet<YeuCau> YeuCaus { get; set; }
    public DbSet<DoUuTien> DoUuTiens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
        modelBuilder.Entity<YeuCau>().ToTable("YeuCau");
        modelBuilder.Entity<DoUuTien>().ToTable("DoUuTien");

        modelBuilder.Entity<YeuCau>()
            .HasOne(y => y.NhanVienGui)
            .WithMany(nv => nv.YeuCausGui)
            .HasForeignKey(y => y.Manv_Gui)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<YeuCau>()
            .HasOne(y => y.NhanVienXuLy)
            .WithMany(nv => nv.YeuCausXuLy)
            .HasForeignKey(y => y.Manv_XuLy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}