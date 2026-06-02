using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

namespace MyMvcApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<PhongTro> PhongTro { get; set; }

        public DbSet<NguoiThue> NguoiThue { get; set; }

        public DbSet<HoaDon> HoaDon { get; set; }

        public DbSet<SuaChua> SuaChua { get; set; }
    }
}