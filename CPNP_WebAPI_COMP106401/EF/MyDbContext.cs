using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.EF
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions opt): base(opt) { }

        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e => {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDH);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NguoiNhanHang).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<DonHangChiTiet>(e =>
            {
                e.ToTable("ChiTietDonHang");
                e.HasKey(ctdh => new { ctdh.MaDH, ctdh.MaHH });

                e.HasOne(ctdh => ctdh.DonHang)
                    .WithMany(ctdh => ctdh.DonHangChiTiets)
                    .HasForeignKey(ctdh => ctdh.MaDH)
                    .HasConstraintName("FK_DonHangCT_DonHang");
                e.HasOne(ctdh => ctdh.HangHoa)
                    .WithMany(ctdh => ctdh.DonHangChiTiets)
                    .HasForeignKey(ctdh => ctdh.MaHH)
                    .HasConstraintName("FK_DonHangCT_HangHoa");

            });
            modelBuilder.Entity<NguoiDung>(e =>
            {
                e.HasIndex(nd => nd.UserName).IsUnique();
                e.Property(nd => nd.HoTen).IsRequired().HasMaxLength(250);
                e.Property(nd => nd.Email).IsRequired().HasMaxLength(150);
            });
        }
    }
}
