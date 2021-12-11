using CPNP_WebAPI_COMP106401.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Services
{
    public interface ILoaiReponsitory
    {
        List<LoaiVM> GetAll();
        LoaiVM GetById(int id);
        LoaiVM Create(LoaiModel loai);
        void Update(LoaiVM loai);
        void Delete(int id);
    }
}
