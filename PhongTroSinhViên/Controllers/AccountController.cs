using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;

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
    }
}