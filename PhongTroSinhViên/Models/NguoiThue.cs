namespace MyMvcApp.Models
{
    public class NguoiThue
    {
        public int Id { get; set; }

        public string TenNguoiDung { get; set; }

        public int? PhongSoHuu { get; set; }

        public DateTime? NgayNhanPhong { get; set; }
    }
}