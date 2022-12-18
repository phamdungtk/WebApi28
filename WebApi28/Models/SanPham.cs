using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class SanPham
{
    public int MaSanPham { get; set; }

    public int MaDanhMuc { get; set; }

    public string TenSanPham { get; set; } = null!;

    public string MoTaSanPham { get; set; } = null!;

    public string AnhDaiDien { get; set; } = null!;

    public int MaNhaSanXuat { get; set; }

    public int MaDonViTinh { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<ChiTietAnhSanPham> ChiTietAnhSanPhams { get; } = new List<ChiTietAnhSanPham>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; } = new List<ChiTietKhuyenMai>();

    public virtual ICollection<ChiTietNhom> ChiTietNhoms { get; } = new List<ChiTietNhom>();

    public virtual ICollection<GiaSanPham> GiaSanPhams { get; } = new List<GiaSanPham>();

    public virtual ICollection<LichSuGiaBan> LichSuGiaBans { get; } = new List<LichSuGiaBan>();

    public virtual DanhMuc MaDanhMucNavigation { get; set; } = null!;

    public virtual DonViTinh MaDonViTinhNavigation { get; set; } = null!;

    public virtual NhaSanXuat MaNhaSanXuatNavigation { get; set; } = null!;
}
