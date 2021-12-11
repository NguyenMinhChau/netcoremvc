
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Models
{
    public class DonHangChiTietModel
    {
        public Guid MaHH { get; set; }
        public Guid MaDH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
    }
}
