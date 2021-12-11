using CPNP_WebAPI_COMP106401.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Services
{
    public interface IHangHoaReponsitory
    {
        List<HangHoaModel> GetAll(string search, double? from, double? to, string sort, int page = 1);
        HangHoaModel GetById(string id);
        HangHoaModel Create(IHangHoaModel hh);
        void Update(HangHoaModel hh);
        void Delete(string id);
    }
}
