using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi28.Models;

public partial class WebApi28Context : DbContext
{
    public WebApi28Context()
    {
    }

    public WebApi28Context(DbContextOptions<WebApi28Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietAnhSanPham> ChiTietAnhSanPhams { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }

    public virtual DbSet<ChiTietNhom> ChiTietNhoms { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<DonViTinh> DonViTinhs { get; set; }

    public virtual DbSet<GiaSanPham> GiaSanPhams { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LichSuGiaBan> LichSuGiaBans { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }

    public virtual DbSet<NhomSanPham> NhomSanPhams { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<Silde> Sildes { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DUNGXUAN\\SQLEXPRESS;Initial Catalog=WebApi28;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietAnhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaAnhChitiet);

            entity.ToTable("ChiTietAnhSanPham");

            entity.Property(e => e.Anh)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietAnhSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_ChiTietAnhSanPham_SanPham");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaChiTietDonHang);

            entity.ToTable("ChiTietDonHang");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDonHang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_SanPham");
        });

        modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaChiTietKhuyenMai);

            entity.ToTable("ChiTietKhuyenMai");

            entity.Property(e => e.MaChiTietKhuyenMai).ValueGeneratedNever();
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhuyenMaiNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .HasForeignKey(d => d.MaKhuyenMai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietKhuyenMai_KhuyenMai");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietKhuyenMais)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietKhuyenMai_SanPham");
        });

        modelBuilder.Entity<ChiTietNhom>(entity =>
        {
            entity.HasKey(e => e.MaChiTietNhom);

            entity.ToTable("ChiTietNhom");

            entity.HasOne(d => d.MaNhomSanPhamNavigation).WithMany(p => p.ChiTietNhoms)
                .HasForeignKey(d => d.MaNhomSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietNhom_NhomSanPham");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.ChiTietNhoms)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietNhom_SanPham");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc);

            entity.ToTable("DanhMuc");

            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.TenDanhMuc).HasMaxLength(250);
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang);

            entity.ToTable("DonHang");

            entity.Property(e => e.NgayDat).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK_DonHang_KhachHang");
        });

        modelBuilder.Entity<DonViTinh>(entity =>
        {
            entity.HasKey(e => e.MaDonViTinh);

            entity.ToTable("DonViTinh");

            entity.Property(e => e.TenDonViTinh).HasMaxLength(100);
        });

        modelBuilder.Entity<GiaSanPham>(entity =>
        {
            entity.HasKey(e => e.MaGiaSanPham).HasName("PK_GiaSanPham_1");

            entity.ToTable("GiaSanPham");

            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.GiaSanPhams)
                .HasForeignKey(d => d.MaSanPham)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSanPham_SanPham");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang);

            entity.ToTable("KhachHang");

            entity.Property(e => e.DiaChi).HasMaxLength(1500);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(250);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKhuyenMai);

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKhuyenMai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(250);
        });

        modelBuilder.Entity<LichSuGiaBan>(entity =>
        {
            entity.HasKey(e => e.MaGiaBan);

            entity.ToTable("LichSuGiaBan");

            entity.Property(e => e.MaGiaBan).ValueGeneratedNever();
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.LichSuGiaBans)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK_LichSuGiaBan_SanPham");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung);

            entity.ToTable("NguoiDung");

            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(1500);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(20);
            entity.Property(e => e.HoTen).HasMaxLength(250);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNhaCungCap);

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNhaCungCap).ValueGeneratedNever();
            entity.Property(e => e.DiaChi).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(250);
        });

        modelBuilder.Entity<NhaSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaNhaSanXuat);

            entity.ToTable("NhaSanXuat");

            entity.Property(e => e.MoTa).HasColumnType("ntext");
            entity.Property(e => e.TenNhaSanXuat).HasMaxLength(250);
        });

        modelBuilder.Entity<NhomSanPham>(entity =>
        {
            entity.HasKey(e => e.MaNhomSanPham);

            entity.ToTable("NhomSanPham");

            entity.Property(e => e.TenNhom).HasMaxLength(250);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham);

            entity.ToTable("SanPham");

            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MoTaSanPham).HasColumnType("ntext");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.TenSanPham).HasMaxLength(250);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_DanhMuc");

            entity.HasOne(d => d.MaDonViTinhNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDonViTinh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_DonViTinh");

            entity.HasOne(d => d.MaNhaSanXuatNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNhaSanXuat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_NhaSanXuat");
        });

        modelBuilder.Entity<Silde>(entity =>
        {
            entity.HasKey(e => e.MaSilde);

            entity.ToTable("Silde");

            entity.Property(e => e.Anh)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan);

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.MatKhau)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TaiKhoan1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TaiKhoan");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK_TaiKhoan_NguoiDung");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
