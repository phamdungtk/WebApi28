using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class DonViTinh
{
    public int MaDonViTinh { get; set; }

    public string? TenDonViTinh { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
