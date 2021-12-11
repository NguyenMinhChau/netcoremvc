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
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll(double? from, double? to, string tenSP)
        {
            var hh = hangHoas.AsQueryable();
            if (!string.IsNullOrEmpty(tenSP))
            {
                hh = hh.Where(hh => hh.TenHH.Contains(tenSP));
            }
            if (from.HasValue)
            {
                hh = hh.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                hh = hh.Where(hh => hh.DonGia <= to);
            }

            return Ok(hh);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                return Ok(hangHoa);
            }catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateHangHoa(IHangHoa hangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                MaHH = Guid.NewGuid(),
                TenHH = hangHoaVM.TenHH,
                DonGia = hangHoaVM.DonGia
            };
            hangHoas.Add(hangHoa);
            return Ok(new { Success = true, Data = hangHoa});
        }

        [HttpPut("{id}")]
        public IActionResult EditHanhHoa(string id, HangHoa hanghoa)
        {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                if(id != hangHoa.MaHH.ToString())
                {
                    return BadRequest();
                }
                hangHoa.TenHH = hanghoa.TenHH;
                hangHoa.DonGia = hanghoa.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHangHoa(string id)
        {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                hangHoas.Remove(hangHoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
