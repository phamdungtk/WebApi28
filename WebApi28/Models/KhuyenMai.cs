using System;
using System.Collections.Generic;

namespace WebApi28.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    public string? TenKhuyenMai { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<ChiTietKhuyenMai> ChiTietKhuyenMais { get; } = new List<ChiTietKhuyenMai>();
}
