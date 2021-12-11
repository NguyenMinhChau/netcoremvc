using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.EF
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        public Guid id { get; set; }
        public int idUser { get; set; }
        [ForeignKey(nameof(idUser))]
        public NguoiDung NguoiDung { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool isUse { get; set; }
        public bool isRevoke { get; set; }
        public DateTime isUseAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
