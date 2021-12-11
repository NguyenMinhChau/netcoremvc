﻿// <auto-generated />
using System;
using CPNP_WebAPI_COMP106401.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CPNP_WebAPI_COMP106401.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20211209091547_DbSetNguoiDung")]
    partial class DbSetNguoiDung
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.DonHang", b =>
                {
                    b.Property<Guid>("MaDH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChiGiao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("NgayGiao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiNhanHang")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TinhTrangDonHang")
                        .HasColumnType("int");

                    b.HasKey("MaDH");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.DonHangChiTiet", b =>
                {
                    b.Property<Guid>("MaDH")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaHH")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("tinyint");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaDH", "MaHH");

                    b.HasIndex("MaHH");

                    b.ToTable("ChiTietDonHang");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.HangHoa", b =>
                {
                    b.Property<Guid>("MaHH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<byte>("GiamGia")
                        .HasColumnType("tinyint");

                    b.Property<int?>("MaLoai")
                        .HasColumnType("int");

                    b.Property<string>("TenHH")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaHH");

                    b.HasIndex("MaLoai");

                    b.ToTable("HangHoa");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.Loai", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaLoai");

                    b.ToTable("Loai");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.NguoiDung", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.DonHangChiTiet", b =>
                {
                    b.HasOne("CPNP_WebAPI_COMP106401.EF.DonHang", "DonHang")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("MaDH")
                        .HasConstraintName("FK_DonHangCT_DonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CPNP_WebAPI_COMP106401.EF.HangHoa", "HangHoa")
                        .WithMany("DonHangChiTiets")
                        .HasForeignKey("MaHH")
                        .HasConstraintName("FK_DonHangCT_HangHoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("HangHoa");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.HangHoa", b =>
                {
                    b.HasOne("CPNP_WebAPI_COMP106401.EF.Loai", "Loai")
                        .WithMany("HangHoas")
                        .HasForeignKey("MaLoai");

                    b.Navigation("Loai");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.DonHang", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.HangHoa", b =>
                {
                    b.Navigation("DonHangChiTiets");
                });

            modelBuilder.Entity("CPNP_WebAPI_COMP106401.EF.Loai", b =>
                {
                    b.Navigation("HangHoas");
                });
#pragma warning restore 612, 618
        }
    }
}
