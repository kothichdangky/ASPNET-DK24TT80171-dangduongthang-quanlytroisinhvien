namespace MyMvcApp.Models
{
    public class HoaDon
    {
        public int Id { get; set; }

        public int PhongId { get; set; }

        public int NguoiThueId { get; set; }

        public DateTime NgayThanhToan { get; set; }

        public decimal TongTien { get; set; }
    }
}