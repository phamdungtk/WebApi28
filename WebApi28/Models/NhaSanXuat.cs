using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class NhaSanXuat
{
    public int MaNhaSanXuat { get; set; }

    public string TenNhaSanXuat { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
