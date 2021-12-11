using CPNP_WebAPI_COMP106401.Models;
using CPNP_WebAPI_COMP106401.Services;
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
    public class ILoaiController : ControllerBase
    {
        private readonly ILoaiReponsitory _loaiReponsitory;

        public ILoaiController(ILoaiReponsitory loaiReponsitory)
        {
            _loaiReponsitory = loaiReponsitory;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_loaiReponsitory.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _loaiReponsitory.GetById(id);
                if(data != null)
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
        public IActionResult UpdateById(int id, LoaiVM loai)
        {
            if(id != loai.MaLoai)
            {
                return BadRequest();
            }
            try
            {
                _loaiReponsitory.Update(loai);
                return NoContent();
                
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _loaiReponsitory.Delete(id);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiModel loai)
        {
            try
            {
                return Ok(_loaiReponsitory.Create(loai));

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
