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
    public class DonHangChiTietController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DonHangChiTietController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsDonHangChiTiet = _context.DonHangChiTiets.ToList();
                return Ok(dsDonHangChiTiet);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var dh = _context.DonHangChiTiets.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
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
                var dh = _context.DonHangChiTiets.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
                if (dh == null)
                {
                    return NotFound();
                }
                _context.DonHangChiTiets.Remove(dh);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult CreatDonHang(DonHangChiTietModel model)
        {
            try
            {
                var dh = new DonHangChiTiet
                {
                    MaHH = model.MaHH,
                    MaDH = model.MaDH,
                    SoLuong = model.SoLuong,
                    DonGia = model.DonGia,
                    GiamGia = model.GiamGia,
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
        public IActionResult UpdateById(string id, DonHangChiTietModel model)
        {
            var dh = _context.DonHangChiTiets.SingleOrDefault(x => x.MaDH == Guid.Parse(id));
            if (dh != null)
            {
                dh.MaHH = model.MaHH;
                dh.MaDH = model.MaDH;
                dh.SoLuong = model.SoLuong;
                dh.DonGia = model.DonGia;
                dh.GiamGia = model.GiamGia;
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
