namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoiDap")]
    public partial class HoiDap
    {
        [Key]
        public int ma { get; set; }

        public string hoi { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngayhoi { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngaytl { get; set; }

        public string dap { get; set; }

        public int? mand { get; set; }

        public int? mabn { get; set; }

        public int? trangthai { get; set; }

        public virtual BenhNhan BenhNhan { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}