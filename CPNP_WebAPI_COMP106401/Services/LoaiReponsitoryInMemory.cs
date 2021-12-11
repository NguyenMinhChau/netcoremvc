using CPNP_WebAPI_COMP106401.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Services
{
    public class LoaiReponsitoryInMemory : ILoaiReponsitory
    {
        static List<LoaiVM> loais = new List<LoaiVM> { 
            new LoaiVM{MaLoai = 1, TenLoai = "TiVi"},
            new LoaiVM{MaLoai = 2, TenLoai = "Máy hút bụi"},
            new LoaiVM{MaLoai = 3, TenLoai = "Tủ lạnh"},
            new LoaiVM{MaLoai = 4, TenLoai = "Điều hòa"},
            new LoaiVM{MaLoai = 5, TenLoai = "Máy giặt"},
        };
        public LoaiVM Create(LoaiModel loai)
        {
            var data = new LoaiVM
            {
                MaLoai = loais.Max(l => l.MaLoai) + 1,
                TenLoai = loai.TenLoai,
            };
            loais.Add(data);
            return data;
        }

        public void Delete(int id)
        {
            var _loai = loais.SingleOrDefault(l => l.MaLoai == id);
            if (_loai != null)
            {
                loais.Remove(_loai);
            }
        }

        public List<LoaiVM> GetAll()
        {
            return loais;
        }

        public LoaiVM GetById(int id)
        {
            return loais.SingleOrDefault(l => l.MaLoai == id);
        }

        public void Update(LoaiVM loai)
        {
            var _loai = loais.SingleOrDefault(l => l.MaLoai == loai.MaLoai);
            if(_loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
            }
        }
    }
}
