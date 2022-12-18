using WebApi28.Entities;
using WebApi28.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi28.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Admin_Api_BanMayTinh.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IUserService _userService;
        private WebApi28Context db = new WebApi28Context();
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            return Ok(user);
        }
        [Route("Get-All")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = from t in db.TaiKhoans
                             join n in db.NguoiDungs on t.MaNguoiDung equals n.MaNguoiDung
                             select new { HoTen = n.HoTen, NgaySinh = n.NgaySinh, GioiTinh = n.GioiTinh, DiaChi = n.DiaChi, Email = n.Email, DienThoai = n.DienThoai, TaiKhoan = t.TaiKhoan1, MatKhau = t.MatKhau, Role = t.Role, AnhDaiDien = n.AnhDaiDien, MaNguoiDung = n.MaNguoiDung };
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
                var hoten = formData.Keys.Contains("hoten") ? (formData["hoten"]).ToString().Trim() : "";
                var result = from t in db.TaiKhoans
                             join n in db.NguoiDungs on t.MaNguoiDung equals n.MaNguoiDung
                             select new { HoTen = n.HoTen, NgaySinh = n.NgaySinh, GioiTinh = n.GioiTinh, DiaChi = n.DiaChi, Email = n.Email, DienThoai = n.DienThoai, TaiKhoan = t.TaiKhoan1, MatKhau = t.MatKhau, Role = t.Role, AnhDaiDien = n.AnhDaiDien, MaNguoiDung = n.MaNguoiDung };
                var kq = result.Where(x=>x.HoTen.Contains(hoten)).OrderByDescending(x => x.MaNguoiDung).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
       

       

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            var result = from t in db.TaiKhoans
                         join n in db.NguoiDungs on t.MaNguoiDung equals n.MaNguoiDung
                         select new { HoTen = n.HoTen, NgaySinh = n.NgaySinh, GioiTinh = n.GioiTinh, DiaChi = n.DiaChi, Email = n.Email, DienThoai = n.DienThoai, TaiKhoan = t.TaiKhoan1, MatKhau= t.MatKhau, Role = t.Role, AnhDaiDien= n.AnhDaiDien, MaNguoiDung = n.MaNguoiDung };
            var user = result.SingleOrDefault(x => x.MaNguoiDung== id);
            return Ok(new { user });
        }

        [Route("create-user")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel model)
        {
            db.NguoiDungs.Add(model.nguoidung);
            db.SaveChanges();

            int MaNguoiDung = model.nguoidung.MaNguoiDung;
            model.taikhoan.MaNguoiDung = MaNguoiDung;
            db.TaiKhoans.Add(model.taikhoan);
            db.SaveChanges();  
            return Ok(new { data = "OK" });
        }


        [Route("update-user")]
        [HttpPost]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            var obj_nguoidung = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == model.nguoidung.MaNguoiDung);
            obj_nguoidung.HoTen = model.nguoidung.HoTen;
            obj_nguoidung.DiaChi = model.nguoidung.DiaChi;
            obj_nguoidung.NgaySinh = model.nguoidung.NgaySinh;
            obj_nguoidung.GioiTinh = model.nguoidung.GioiTinh;
            obj_nguoidung.AnhDaiDien = model.nguoidung.AnhDaiDien;
            obj_nguoidung.DiaChi = model.nguoidung.DiaChi;
            obj_nguoidung.Email = model.nguoidung.Email;
            obj_nguoidung.DienThoai = model.nguoidung.DienThoai;
            obj_nguoidung.TrangThai = model.nguoidung.TrangThai;
            //....
            db.SaveChanges();

            var obj_taikhoan = db.TaiKhoans.SingleOrDefault(x => x.MaNguoiDung == model.taikhoan.MaNguoiDung);
            obj_taikhoan.TaiKhoan1 = model.taikhoan.TaiKhoan1;
            obj_taikhoan.MatKhau = model.taikhoan.MatKhau;
            obj_taikhoan.TrangThai = model.taikhoan.TrangThai;
            obj_taikhoan.Role = model.taikhoan.Role;
            //....
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }

        [Route("delete-user/{MaNguoiDung}")]
        [HttpDelete]
        public IActionResult DeleteUser(int? MaNguoiDung)
        {
            var obj1 = db.TaiKhoans.SingleOrDefault(s => s.MaNguoiDung == MaNguoiDung);
            db.TaiKhoans.Remove(obj1);
            db.SaveChanges();
            var obj2 = db.NguoiDungs.SingleOrDefault(s => s.MaNguoiDung == MaNguoiDung);
            db.NguoiDungs.Remove(obj2);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
