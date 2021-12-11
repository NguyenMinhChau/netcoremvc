using CPNP_WebAPI_COMP106401.EF;
using CPNP_WebAPI_COMP106401.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DonHangController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(DateTime? fromngaydat, DateTime? tongaydat, string hotenkhachhang, string diachigiao)
        {
            try
            {
                var dsDonHang = _context.DonHangs.AsQueryable();
                if (!string.IsNullOrEmpty(hotenkhachhang))
                {
                    dsDonHang = dsDonHang.Where(hh => hh.NguoiNhanHang.Contains(hotenkhachhang));
                } 
                if (!string.IsNullOrEmpty(diachigiao))
                {
                    dsDonHang = dsDonHang.Where(hh => hh.DiaChiGiao.Contains(diachigiao));
                }
                if (fromngaydat.HasValue)
                {
                    dsDonHang = dsDonHang.Where(hh => hh.NgayDat >= fromngaydat);
                }
                if (tongaydat.HasValue)
                {
                    dsDonHang = dsDonHang.Where(hh => hh.NgayDat <= tongaydat);
                }
                return Ok(dsDonHang);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var dh = _context.DonHangs.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
            if (dh != null)
            {
                return Ok(dh);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDonHang(string id)
        {
            try
            {
                var dh = _context.DonHangs.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
                if (dh == null)
                {
                    return NotFound();
                }
                _context.DonHangs.Remove(dh);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult CreatDonHang(DonHangModel model)
        {
            try
            {
                var dh = new DonHang
                {
                    NgayDat = model.NgayDat,
                    NgayGiao = model.NgayGiao,
                    NguoiNhanHang = model.NguoiNhanHang,
                    DiaChiGiao = model.DiaChiGiao,
                    SoDienThoai = model.SoDienThoai,
                    TinhTrangDonHang = model.TinhTrangDonHang
                    
                };
                _context.Add(dh);
                _context.SaveChanges();
                return Ok(dh);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateById(string id, DonHangModel model)
        {
            var dh = _context.DonHangs.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
            if (dh != null)
            {
                dh.NgayDat = model.NgayDat;
                dh.NgayGiao = model.NgayGiao;
                dh.NguoiNhanHang = model.NguoiNhanHang;
                dh.DiaChiGiao = model.DiaChiGiao;
                dh.SoDienThoai = model.SoDienThoai;
                dh.TinhTrangDonHang = model.TinhTrangDonHang;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
