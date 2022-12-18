using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class NhomSanPham
{
    public int MaNhomSanPham { get; set; }

    public string? TenNhom { get; set; }

    public bool? TrangThai { get; set; }

    public virtual ICollection<ChiTietNhom> ChiTietNhoms { get; } = new List<ChiTietNhom>();
}
