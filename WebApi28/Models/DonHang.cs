using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public int? MaKhachHang { get; set; }

    public DateTime? NgayDat { get; set; }

    public int? TrangThaiDonHang { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
