using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Admin()
        {
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Index");
            }
            var phongTro = _context.PhongTro
                .Include(p => p.NguoiSoHuu)
                .ToList();

            return View(phongTro);
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

            ViewBag.SoPhongTrong = soPhongTrong;

            return View();
        }

        public IActionResult Home()
        {
            var tenNguoiDung = HttpContext.Session.GetString("TenNguoiDung");

            var user = _context.NguoiThue
                .FirstOrDefault(x => x.TenNguoiDung == tenNguoiDung);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var phong = _context.PhongTro
                .FirstOrDefault(x => x.Id == user.PhongSoHuu);

            if (phong == null)
            {
                return RedirectToAction("Index");
            }

            DateTime ngayNhanPhong = user.NgayNhanPhong.Value;
            DateTime ngayThuTien = ngayNhanPhong;

            while (ngayThuTien <= DateTime.Today)
            {
                ngayThuTien = ngayThuTien.AddMonths(1);
            }

            ViewBag.NgayThuTien = ngayThuTien;

            return View(phong);
        }
        public IActionResult Fix()
        {
            return View();
        }
    }
}