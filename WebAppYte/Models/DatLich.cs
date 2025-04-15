namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DatLich")]
    public partial class DatLich
    {
        [Key]
        public int madat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaydat { get; set; }

        public string mota { get; set; }

        [StringLength(10)]
        public string sdt { get; set; }

        [StringLength(30)]
        public string hoten { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaysinh { get; set; }

        public int? trangthai { get; set; }

        public int? maca { get; set; }

        public int? mabn { get; set; }

        public virtual CaKham CaKham { get; set; }
    }
}
