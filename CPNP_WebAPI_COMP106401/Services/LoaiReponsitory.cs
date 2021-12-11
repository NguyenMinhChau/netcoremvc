using CPNP_WebAPI_COMP106401.EF;
using CPNP_WebAPI_COMP106401.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Services
{
    public class LoaiReponsitory : ILoaiReponsitory
    {
        private readonly MyDbContext _context;

        public LoaiReponsitory(MyDbContext context)
        {
            _context = context;
        }
        public LoaiVM Create(LoaiModel loai)
        {
            var _loai = new Loai
            {
                TenLoai = loai.TenLoai

            };
            _context.Add(_loai);
            _context.SaveChanges();
            return new LoaiVM { 
                MaLoai = _loai.MaLoai,
                TenLoai = _loai.TenLoai
            };
        }

        public void Delete(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
            }
        }

        public List<LoaiVM> GetAll()
        {
            var loais = _context.Loais.Select(l => new LoaiVM
            {
                MaLoai=l.MaLoai,
                TenLoai=l.TenLoai
            });
            return loais.ToList();
        }

        public LoaiVM GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == id);
            if(loai != null)
            {
                return new LoaiVM
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }

        public void Update(LoaiVM loai)
        {
            var _loai = _context.Loais.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            _loai.TenLoai = loai.TenLoai;
            _context.SaveChanges();
        }
    }
}
