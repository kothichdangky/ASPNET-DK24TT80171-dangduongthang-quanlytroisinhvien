using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Admin(int page = 1)
        {
            int pageSize = 5;

            int totalRows = _context.PhongTro.Count();
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index");
            }
            var phongTro = _context.PhongTro
        .Include(x => x.NguoiSoHuu)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    (double)totalRows / pageSize
                );
            return View(phongTro);
        }

        [HttpPost]
        public IActionResult UpdatePhong(
            int Id,
            double LuongNuoc,
            double LuongDien,
            decimal TienHangThang,
            bool TinhTrangDongTien)
        {
            var phong = _context.PhongTro
                .FirstOrDefault(x => x.Id == Id);

            if (phong == null)
            {
                return RedirectToAction("Admin");
            }

            phong.LuongNuoc = LuongNuoc;
            phong.LuongDien = LuongDien;
            phong.TienHangThang = TienHangThang;
            phong.TinhTrangDongTien = TinhTrangDongTien;
            _context.SaveChanges();

            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult DeleteNguoiThue(int PhongId)
        {
            var phong = _context.PhongTro
                .FirstOrDefault(x => x.Id == PhongId);

            if (phong == null)
            {
                return RedirectToAction("Admin");
            }

            var nguoiThue = _context.NguoiThue
                .FirstOrDefault(x => x.Id == phong.NguoiSoHuuId);

            if (nguoiThue != null)
            {
                var hoaDons = _context.HoaDon
                    .Where(x => x.NguoiThueId == nguoiThue.Id)
                    .ToList();

                _context.HoaDon.RemoveRange(hoaDons);

                _context.NguoiThue.Remove(nguoiThue);
            }
            phong.NguoiSoHuuId = null;

            _context.SaveChanges();

            return RedirectToAction("Admin");
        }

        public IActionResult Index()
        {

            var role = HttpContext.Session.GetString("Role");
            var phongSoHuu = HttpContext.Session.GetInt32("PhongSoHuu");
            var soPhongTrong = _context.PhongTro.Count(x => x.NguoiSoHuuId == null);


            if (role != "Admin" && phongSoHuu > 0)
            {
                return RedirectToAction("Home");
            }

            var phongTrong = _context.PhongTro
                .Where(x => x.NguoiSoHuuId == null)
                .ToList();

            ViewBag.SoPhongTrong = soPhongTrong;
            ViewBag.PhongTrong = phongTrong;

            return View();
        }

        public IActionResult Home(
            int page = 1,
            bool openModal = false)
        {
            var tenNguoiDung =
                HttpContext.Session.GetString("TenNguoiDung");

            var user = _context.NguoiThue
                .FirstOrDefault(x =>
                    x.TenNguoiDung == tenNguoiDung);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = 3;

            var hoaDons = _context.HoaDon
                .Where(x => x.NguoiThueId == user.Id)
                .OrderByDescending(x => x.NgayThanhToan)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalRows = _context.HoaDon
                .Count(x => x.NguoiThueId == user.Id);

            ViewBag.HoaDons = hoaDons;

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    (double)totalRows / pageSize
                );

            var phong = _context.PhongTro
                .FirstOrDefault(x =>
                    x.Id == user.PhongSoHuu);

            if (phong == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.NgayThuTien =
                user.NgayThuTienTiepTheo;

            ViewBag.OpenModal =
                openModal;

            return View(phong);
        }

        public IActionResult Fix(int page = 1)
        {
            int pageSize = 5;

            int totalRows = _context.SuaChua.Count();

            var dsSuaChua = _context.SuaChua
                .Include(x => x.NguoiThue)
                .Include(x => x.Phong)
                .OrderByDescending(x => x.NgayYeuCau)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;

            ViewBag.TotalPages =
                (int)Math.Ceiling(
                    (double)totalRows / pageSize
                );

            return View(dsSuaChua);
        }

        [HttpPost]
        public IActionResult RequestSuaChua(string NoiDung)
        {
            if (string.IsNullOrWhiteSpace(NoiDung))
            {
                TempData["FixError"] =
                    "Vui lòng nhập nội dung yêu cầu sửa chữa";

                return RedirectToAction("Home");
            }

            var tenNguoiDung =
                HttpContext.Session.GetString("TenNguoiDung");

            var user = _context.NguoiThue
                .FirstOrDefault(x =>
                    x.TenNguoiDung == tenNguoiDung);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var suaChua = new SuaChua
            {
                PhongId = user.PhongSoHuu.Value,
                NguoiThueId = user.Id,
                NoiDung = NoiDung,
                NgayYeuCau = DateTime.Now,
            };

            _context.SuaChua.Add(suaChua);

            _context.SaveChanges();

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult DeleteSuaChua(int Id)
        {
            var suaChua = _context.SuaChua
                .FirstOrDefault(x => x.Id == Id);

            if (suaChua != null)
            {
                _context.SuaChua.Remove(suaChua);

                _context.SaveChanges();
            }

            return RedirectToAction("Fix");
        }
    }
}

