using CPNP_WebAPI_COMP106401.Models;
using CPNP_WebAPI_COMP106401.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CPNP_WebAPI_COMP106401.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IHangHoaController : ControllerBase
    {
        private readonly IHangHoaReponsitory _hangHoaReponsitory;

        public IHangHoaController(IHangHoaReponsitory hangHoaReponsitory)
        {
            _hangHoaReponsitory = hangHoaReponsitory;
        }

        [HttpGet]
        public IActionResult GetAll(string search, double? from, double? to, string sort, int page = 1)
        {
            try
            {
                var result = _hangHoaReponsitory.GetAll(search, from, to, sort, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var data = _hangHoaReponsitory.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [Authorize] //Đăng nhập mới cho chỉnh sửa
        public IActionResult UpdateById(string id, HangHoaModel hh)
        {
            if (Guid.Parse(id) != hh.MaHH)
            {
                return BadRequest();
            }
            try
            {
                _hangHoaReponsitory.Update(hh);
                return NoContent();

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        [Authorize] //Đăng nhập mới cho xóa
        public IActionResult DeleteById(string id)
        {
            try
            {
                _hangHoaReponsitory.Delete(id);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize] //Đăng nhập mới cho tạo mới
        public IActionResult CreateNew(IHangHoaModel hh)
        {
            try
            {
                return Ok(_hangHoaReponsitory.Create(hh));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
