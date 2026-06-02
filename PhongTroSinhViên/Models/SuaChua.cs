namespace MyMvcApp.Models
{
    public class SuaChua
    {
        public int Id { get; set; }

        public int PhongId { get; set; }

        public int NguoiThueId { get; set; }

        public string NoiDung { get; set; }

        public DateTime NgayYeuCau { get; set; }

        public PhongTro? Phong { get; set; }

        public NguoiThue? NguoiThue { get; set; }
    }
}