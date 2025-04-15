using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppYte.Models
{
    [Table("BenhAn")]
    public class BenhAn
    {
        [Key]
        public int maba { get; set; }

        [ForeignKey("benhnhan")]
        public int mabn { get; set; }
        public BenhNhan benhnhan { get; set; }

        [ForeignKey("bacsi")]
        public int mabs { get; set; }
        public NguoiDung bacsi { get; set; }

        public string tieude { get; set; }

        public DateTime ngaykham { get; set; }
        [DataType(DataType.Time)]
        public DateTime giokham { get; set; }
        public double mach { get; set; }
        public double nhietdo { get; set; }
        public double nhiptho { get; set; }
        public double chieucao { get; set; }
        public double cannang { get; set; }
		[Range(10.0, 40.0, ErrorMessage = "BMI phải từ 10.0 đến 40.0")]

		public double bmi { get; set; }
        public double thiluctrai { get; set; }
        public double thilucphai { get; set; }
        public double nhanapP { get; set; }
        public double nhanapT { get; set; }
        public int trangthai { get; set; }
        public string chuandoan { get; set; }
        public string ketqua { get; set; }

    }
}