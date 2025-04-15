namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGia")]
    public partial class DanhGia
    {
        [Key]
        public int madanhgia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngay { get; set; }

        public string noidung { get; set; }
        
        public int? mand { get; set; }

        public int? mabn { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public int rating { get; set; }
    }
}
