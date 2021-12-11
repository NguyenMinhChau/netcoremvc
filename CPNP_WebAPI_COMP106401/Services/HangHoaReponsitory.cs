using CPNP_WebAPI_COMP106401.EF;
using CPNP_WebAPI_COMP106401.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Services
{
    public class HangHoaReponsitory : IHangHoaReponsitory
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE {get; set;} = 5;

        public HangHoaReponsitory(MyDbContext context)
        {
            _context = context;
        }

        public HangHoaModel Create(IHangHoaModel hh)
        {
            var _hh = new EF.HangHoa
            {
                TenHH = hh.TenHH,
                DonGia = hh.DonGia,
                GiamGia = hh.GiamGia,
                MaLoai = hh.MaLoai
            };
            _context.Add(_hh);
            _context.SaveChanges();
            return new HangHoaModel
            {
                TenHH = _hh.TenHH,
                DonGia = _hh.DonGia,
                MaHH = _hh.MaHH,
                TenLoai = _hh.MaLoai.ToString()
            };
        }

        public void Delete(string id)
        {
            var hh = _context.HangHoas.SingleOrDefault(l => l.MaHH == Guid.Parse(id));
            if (hh != null)
            {
                _context.Remove(hh);
                _context.SaveChanges();
            }
        }

        public List<HangHoaModel> GetAll(string search, double? from, double? to, string sort, int page = 1)
        {
            var allProductions = _context.HangHoas.AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                allProductions = allProductions.Where(hh => hh.TenHH.Contains(search));
            }
            if (from.HasValue)
            {
                allProductions = allProductions.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProductions = allProductions.Where(hh => hh.DonGia <= to);
            }

            allProductions = allProductions.OrderBy(hh => hh.TenHH);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "tenhhdesc": allProductions = allProductions.OrderByDescending(hh => hh.TenHH); break;
                    case "tenhhasc": allProductions = allProductions.OrderBy(hh => hh.TenHH); break;
                    case "dongidesc": allProductions = allProductions.OrderByDescending(hh => hh.DonGia); break;
                    case "dongiaasc": allProductions = allProductions.OrderBy(hh => hh.DonGia); break;     
                }
            }

            allProductions = allProductions.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            var result = allProductions.Select(hh => new HangHoaModel
            {
                MaHH = hh.MaHH,
                TenHH = hh.TenHH,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai.TenLoai
            });
            return result.ToList();
        }

        public HangHoaModel GetById(string id)
        {
            var hh = _context.HangHoas.SingleOrDefault(l => l.MaHH == Guid.Parse(id));
            if (hh != null)
            {
                return new HangHoaModel
                {
                   MaHH = hh.MaHH,
                   TenHH = hh.TenHH,
                   DonGia = hh.DonGia,
                   TenLoai = hh.Loai.MaLoai.ToString()
                };
            }
            return null;
        }

        public void Update(HangHoaModel hh)
        {
            var _hh = _context.HangHoas.SingleOrDefault(l => l.MaHH == hh.MaHH);
            _hh.TenHH = hh.TenHH;
            _hh.DonGia = hh.DonGia;
            _context.SaveChanges();
        }
    }
}
