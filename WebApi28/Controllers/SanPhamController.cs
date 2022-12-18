using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi28.Entities;
using WebApi28.Models;

namespace WebApi28.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {

        private WebApi28Context db = new WebApi28Context();
        [Route("Get-All")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = from n in db.GiaSanPhams
                             join t in db.SanPhams on n.MaSanPham equals t.MaSanPham                         
                             select new {MaSanPham=t.MaSanPham,Gia= n.Gia,TenSanPham = t.TenSanPham,MoTaSanPham =t.MoTaSanPham,AnhDaiDien= t.AnhDaiDien,
                                 NgayTao = t.NgayTao };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("Get-Sp-MoiNhat")]
        [HttpGet]
        public IActionResult GeBanChay()
        {
            try
            {
                var result = db.SanPhams.Select(x => new { x.MaSanPham, x.TenSanPham, x.NgayTao, x.AnhDaiDien }).OrderByDescending(s => s.NgayTao).Take(12).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("Get-By-Id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var kq = from r in db.SanPhams
                         join g in db.GiaSanPhams on r.MaSanPham equals g.MaSanPham
                         select new { r.MaSanPham, r.TenSanPham, r.AnhDaiDien, g.Gia, r.MaDanhMuc, r.NgayTao, r.ChiTietAnhSanPhams };
                var result = kq.Select(x => new { x.MaSanPham, x.TenSanPham, x.AnhDaiDien, x.ChiTietAnhSanPhams, x.Gia }).Where(s => s.MaSanPham == id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-SanPham")]
        [HttpPost]
        public IActionResult CreateSanPham([FromBody] SanPhamModel model)
        {        
           db.GiaSanPhams.Add(model.giasanpham);
            db.SaveChanges();

            int MaGiaSanPham = model.giasanpham.MaGiaSanPham;
            model.sanpham.MaSanPham = MaGiaSanPham;
            db.SanPhams.Add(model.sanpham);
            db.SaveChanges();  
            return Ok(new { data = "OK" });
        }

        [Route("update-SanPham")]
        [HttpPost]
        public IActionResult UpdateSanPham([FromBody] SanPhamModel model)
        {
           

            var obj_giasanpham = db.GiaSanPhams.SingleOrDefault(x => x.MaSanPham == model.giasanpham.MaSanPham);
            obj_giasanpham.Gia = model.giasanpham.Gia;
            //....
            db.SaveChanges();


            var obj_sanpham = db.SanPhams.SingleOrDefault(x => x.MaSanPham == model.sanpham.MaSanPham);
            obj_sanpham.MaDanhMuc = model.sanpham.MaDanhMuc;
            obj_sanpham.TenSanPham = model.sanpham.TenSanPham;
            obj_sanpham.MoTaSanPham = model.sanpham.MoTaSanPham;
            obj_sanpham.AnhDaiDien = model.sanpham.AnhDaiDien;
            obj_sanpham.MaNhaSanXuat = model.sanpham.MaNhaSanXuat;
            obj_sanpham.MaDonViTinh = model.sanpham.MaDonViTinh;
            obj_sanpham.NgayTao = model.sanpham.NgayTao;
            //....
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("delete-SanPham/{MaSanPham}")]
        [HttpDelete]
        public IActionResult DeleteSanPham(int? MaSanPham)
        {
            var obj1 = db.GiaSanPhams.SingleOrDefault(s => s.MaSanPham == MaSanPham);
            db.GiaSanPhams.Remove(obj1);
            db.SaveChanges();
            var obj2 = db.SanPhams.SingleOrDefault(s => s.MaSanPham == MaSanPham);
            db.SanPhams.Remove(obj2);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
