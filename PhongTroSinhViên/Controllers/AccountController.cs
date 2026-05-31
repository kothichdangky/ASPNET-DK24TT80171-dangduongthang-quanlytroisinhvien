using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;
namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string TenNguoiDung, string MatKhau)
        {
            var user = _context.NguoiThue.FirstOrDefault(x =>
                x.TenNguoiDung == TenNguoiDung &&
                x.MatKhau == MatKhau);

            if (user == null)
            {
                TempData["LoginError"] =
            "Tên đăng nhập hoặc mật khẩu không đúng";
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString(
                "TenNguoiDung",
                user.TenNguoiDung
            );
            HttpContext.Session.SetString("Role", user.Role ?? "");

            HttpContext.Session.SetInt32("PhongSoHuu", user.PhongSoHuu ?? 0);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult ThuePhong(
     string TenNguoiDung,
     string MatKhau,
     int PhongId)
        {
            var daTonTai = _context.NguoiThue
        .Any(x => x.TenNguoiDung == TenNguoiDung);

            if (daTonTai)
            {
                TempData["ThuePhongError"] =
                    "Tên đăng nhập đã tồn tại";

                return RedirectToAction("Index", "Home");
            }

            var nguoiThue = new NguoiThue
            {
                TenNguoiDung = TenNguoiDung,
                MatKhau = MatKhau,
                PhongSoHuu = PhongId,
                NgayNhanPhong = DateTime.Now,
                Role = "User"
            };

            _context.NguoiThue.Add(nguoiThue);

            _context.SaveChanges();

            var phong = _context.PhongTro
                .FirstOrDefault(x => x.Id == PhongId);

            if (phong != null)
            {
                phong.NguoiSoHuuId = nguoiThue.Id;

                _context.SaveChanges();
            }

            HttpContext.Session.SetString(
                "TenNguoiDung",
                nguoiThue.TenNguoiDung
            );

            HttpContext.Session.SetString(
                "Role",
                nguoiThue.Role ?? ""
            );

            HttpContext.Session.SetInt32(
                "PhongSoHuu",
                nguoiThue.PhongSoHuu ?? 0
            );
            TempData["Success"] = true;
            return RedirectToAction("Home", "Home");
        }

    }
}