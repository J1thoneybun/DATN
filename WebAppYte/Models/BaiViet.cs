namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [Key]
        public int mabv { get; set; }

        public string tieude { get; set; }

        public string noidung { get; set; }

        [StringLength(100)]
        public string hinhanh { get; set; }

        public string mota { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngaydang { get; set; }

        public int? maloai { get; set; }

        public int? mand { get; set; }

        public virtual Loai Loai { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
