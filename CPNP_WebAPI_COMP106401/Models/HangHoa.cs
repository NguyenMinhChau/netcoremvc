using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Models
{
    public class IHangHoa
    {
        public string TenHH { get; set; }
        public double DonGia {get; set;}
    }
    public class HangHoa : IHangHoa
    {
        public Guid MaHH { get; set; }
    }
    public class HangHoaModel
    {
        public Guid MaHH { get; set; }
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public string TenLoai { get; set; }
    }
}
