namespace MyMvcApp.Models
{
    public class PhongTro
    {
        public int Id { get; set; }

        public string TenPhong { get; set; }

        public int? NguoiSoHuuId { get; set; }

        // float
        public double LuongNuoc { get; set; }

        // float
        public double LuongDien { get; set; }

        // decimal
        public decimal TienHangThang { get; set; }

        // decimal
        public double TongTien { get; set; }

        // decimal
        public decimal TienDatCoc { get; set; }

        // bit
        public bool TinhTrangDongTien { get; set; }

        public NguoiThue? NguoiSoHuu { get; set; }
    }
}