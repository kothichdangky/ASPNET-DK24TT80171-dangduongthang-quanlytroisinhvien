namespace MyMvcApp.Models
{
    public class PhongTro
    {
        public int Id { get; set; }

        public string TenPhong { get; set; }

        public int? NguoiSoHuuId { get; set; }

        public double LuongNuoc { get; set; }

        public double LuongDien { get; set; }

        public decimal TienHangThang { get; set; }

        public double TongTien { get; set; }

        public decimal TienDatCoc { get; set; }

        public bool TinhTrangDongTien { get; set; }

        public NguoiThue? NguoiSoHuu { get; set; }
    }
}