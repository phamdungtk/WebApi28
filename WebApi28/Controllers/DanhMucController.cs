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
    public class DanhMucController : ControllerBase
    {
        private WebApi28Context db = new WebApi28Context();
        [Route("Get-DanhMuc-All")]
        [HttpGet]
        public IEnumerable<DanhMucModel> GetAllMenu()
        {
            return GetData();
        }
        [Route("Get-All")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = db.DanhMucs.Select(x => new { x.MaDanhMuc, x.MaDanhMucCha, x.TenDanhMuc, x.Stt ,x.TrangThai}).OrderByDescending(s => s.TrangThai).Take(11).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("SearchGetAll")]
        [HttpPost]
        public IActionResult SearchGetAll([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from c in db.DanhMucs
                             //join d in db.DanhMucs on c.MaDanhMuc equals d.MaDanhMuc                
                             select new {c.MaDanhMuc,c.MaDanhMucCha, c.TenDanhMuc,c.Stt, c.TrangThai };
                var kq = result.OrderBy(x => x.TenDanhMuc).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        [Route("create-user")]
        [HttpPost]
        public IActionResult Create(DanhMuc model)
        {
            db.DanhMucs.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }


        [Route("update-user")]
        [HttpPost]
        public IActionResult Update([FromBody] DanhMuc model)
        {
            var obj_danhmuc = db.DanhMucs.SingleOrDefault(x => x.MaDanhMuc == model.MaDanhMuc);
            obj_danhmuc.MaDanhMucCha = model.MaDanhMucCha;
            obj_danhmuc.TenDanhMuc = model.TenDanhMuc;
            obj_danhmuc.Stt = model.Stt;
            obj_danhmuc.TrangThai = model.TrangThai;
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [NonAction]
        private List<DanhMucModel> GetData()
        {
            var allItemGroups = db.DanhMucs.Where(x => x.TrangThai == true).Select(x => new DanhMucModel { MaDanhMuc = x.MaDanhMuc, MaDanhMucCha = x.MaDanhMucCha, TenDanhMuc = x.TenDanhMuc }).ToList();
            var lstParent = allItemGroups.Where(ds => ds.MaDanhMucCha == null).ToList();
            foreach (var item in lstParent)
            {
                item.children = GetHiearchyList(allItemGroups, item);
            }
            return lstParent;
        }
        [NonAction]
        private List<DanhMucModel> GetHiearchyList(List<DanhMucModel> lstAll, DanhMucModel node)
        {
            var lstChilds = lstAll.Where(ds => ds.MaDanhMucCha == node.MaDanhMuc).ToList();
            if (lstChilds.Count == 0)
                return null;
            for (int i = 0; i < lstChilds.Count; i++)
            {
                var childs = GetHiearchyList(lstAll, lstChilds[i]);
                lstChilds[i].type = (childs == null || childs.Count == 0) ? "leaf" : "";
                lstChilds[i].children = childs;
            }
            return lstChilds.ToList();
        }
        [Route("Get-Search-MaDanhMuc")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                int? ma_danh_muc = null;
                string loc = "";
                if (formData.Keys.Contains("loc") && !string.IsNullOrEmpty(Convert.ToString(formData["loc"]))) { loc = formData["loc"].ToString(); }
                if (formData.Keys.Contains("ma_danh_muc") && !string.IsNullOrEmpty(Convert.ToString(formData["ma_danh_muc"]))) { ma_danh_muc = int.Parse(formData["ma_danh_muc"].ToString()); }
                var result = from r in db.SanPhams
                             join g in db.GiaSanPhams on r.MaSanPham equals g.MaSanPham
                             select new { r.MaSanPham, r.TenSanPham, r.AnhDaiDien, g.Gia, r.MaDanhMuc, r.NgayTao };
                var result1 = result.Where(s => s.MaDanhMuc == ma_danh_muc || ma_danh_muc == null).Select(x => new { x.MaSanPham, x.TenSanPham, x.AnhDaiDien, x.Gia, x.NgayTao }).ToList();
                long total = result1.Count();
                dynamic result2 = null;
                switch (loc)
                {
                    case "TD":
                        result2 = result1.OrderBy(x => x.Gia).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                    case "GD":
                        result2 = result1.OrderByDescending(x => x.Gia).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                    default:
                        result2 = result1.OrderByDescending(x => x.NgayTao).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                        break;
                }
                return Ok(
                           new KQ
                           {
                               page = page,
                               totalItem = total,
                               pageSize = pageSize,
                               data = result2
                           }
                         );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var kq = from r in db.DanhMucs
                         //join g in db.SanPhams on r.MaDanhMuc equals g.MaDanhMuc
                         select new {r.MaDanhMuc ,r.MaDanhMucCha, r.TenDanhMuc, r.Stt, r.TrangThai };
                var result = kq.SingleOrDefault(x => x.MaDanhMuc == id);
                //var result = kq.Select(x => new { x.MaDanhMuc,x.MaDanhMucCha, x.TenDanhMuc, x.Stt, x.TrangThai}).Where(s => s.MaDanhMuc == id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("delete-user/{MaDanhMuc}")]
        [HttpDelete]
        public IActionResult Delete(int? MaDanhMuc)
        {
            var obj1 = db.DanhMucs.SingleOrDefault(s => s.MaDanhMuc == MaDanhMuc);
            db.DanhMucs.Remove(obj1);
            db.SaveChanges();
           
            return Ok(new { data = "OK" });
        }
    }
    public class DanhMucModel
    {
        public int MaDanhMuc { get; set; }
        public int? MaDanhMucCha { get; set; }
        public string TenDanhMuc { get; set; }
        public List<DanhMucModel> children { get; set; }
        public string type { get; set; }
    }
    public class KQ
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public long totalItem { get; set; }
        public dynamic data { get; set; }
    }
    public class SP
    {
        public int MaSanPham { get; set; }
        public int MaDanhMuc { get; set; }
        public string TenSanPham { get; set; }
        public double? Gia { get; set; }
    }
}
