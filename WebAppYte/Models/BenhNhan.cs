namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BenhNhan")]
    public partial class BenhNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhNhan()
        {
            DanhGias = new HashSet<DanhGia>();
            HoiDaps = new HashSet<HoiDap>();
        }

        [Key]
        public int mabn { get; set; }

        [Required]
        [StringLength(50)]
        public string tenbn { get; set; }

        [StringLength(10)]
        public string sdt { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(50)]
        public string diachi { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngaysinh { get; set; }

        [StringLength(5)]
        public string gioitinh { get; set; }

        [StringLength(50)]
        public string tendn { get; set; }

        [StringLength(50)]
        public string mk { get; set; }

        public int? trangthai { get; set; } = 1;
		[StringLength(100)]
		public string ResetPasswordCode { get; set; }

		public DateTime? ResetPasswordCodeExpiry { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoiDap> HoiDaps { get; set; }

        public virtual ICollection<BenhAn> BenhAns { get; set; }
    }
}
