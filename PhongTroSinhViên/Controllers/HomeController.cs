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
            var phongTro = _context.PhongTro
                .Include(p => p.NguoiSoHuu)
                .ToList();

            return View(phongTro);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Fix()
        {
            return View();
        }
    }
}