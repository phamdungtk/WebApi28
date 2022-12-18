using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi28.Services;
using WebApi28.Models;
using WebApi28.Entities;

namespace WebApi28.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DonHangController : Controller
    {
        private WebApi28Context db = new WebApi28Context();
        [Route("Get-DonHang-All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.DonHangs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from c in db.ChiTietDonHangs
                             join d in db.DonHangs on c.MaDonHang equals d.MaDonHang
                             join k in db.KhachHangs on d.MaKhachHang equals k.MaKhachHang
                             join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                             select new { s.MaSanPham, s.TenSanPham, d.MaDonHang, k.TenKhachHang, d.NgayDat, d.TrangThaiDonHang };
                var kq = result.OrderBy(x => x.NgayDat).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                         new ResponseListMessage
                         {
                             page = page,
                             totalItem = kq.Count,
                             pageSize = pageSize,
                             data = kq
                         });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [Route("Create-DonHang")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] GioHang model)
        {
            db.KhachHangs.Add(model.khach);
            db.SaveChanges();

            int MaKhachHang = model.khach.MaKhachHang;
            DonHang dh = new DonHang();
            dh.MaKhachHang = MaKhachHang;
            dh.NgayDat = System.DateTime.Now;
            dh.TrangThaiDonHang = 1;
            db.DonHangs.Add(dh);
            db.SaveChanges();
            int MaDonHang = dh.MaDonHang;

            if (model.donhang.Count > 0)
            {
                foreach (var item in model.donhang)
                {
                    item.MaDonHang = MaDonHang;
                    db.ChiTietDonHangs.Add(item);
                }
                db.SaveChanges();
            }

            return Ok(new { data = "OK" });
        }

    }

    public class GioHang
    {
        public KhachHang khach { get; set; }
        public List<ChiTietDonHang> donhang { get; set; }
    }
}
