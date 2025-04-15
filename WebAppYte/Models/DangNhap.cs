namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangNhap")]
    public partial class DangNhap
    {
        [Key]
        public int ma { get; set; }

        [StringLength(30)]
        public string tendn { get; set; }

        [StringLength(30)]
        public string mk { get; set; }

        public int? quyen { get; set; }

        public int? mand { get; set; }

		
	}
}
