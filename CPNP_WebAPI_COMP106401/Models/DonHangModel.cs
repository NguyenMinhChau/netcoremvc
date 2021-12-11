using CPNP_WebAPI_COMP106401.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Models
{
    public class DonHangModel
    {
        public DateTime NgayDat { get; set; }
        public DateTime? NgayGiao { get; set; }
        public TinhTrangDonDatHang TinhTrangDonHang { get; set; }
        public string NguoiNhanHang { get; set; }
        public string DiaChiGiao { get; set; }
        public string SoDienThoai { get; set; }
    }
}
