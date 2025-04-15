namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            BaiViets = new HashSet<BaiViet>();
            CaKhams = new HashSet<CaKham>();
            DanhGias = new HashSet<DanhGia>();
            HoiDaps = new HashSet<HoiDap>();
        }

        [Key]
        public int mand { get; set; }

        [Required]
        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(50)]
        public string diachi { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngaysinh { get; set; }

        [StringLength(5)]
        public string gioitinh { get; set; }

        [StringLength(10)]
        public string sdt { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        public string chucvu { get; set; }

        public string hocham { get; set; }

        public string hocvi { get; set; }

        public string gioithieu { get; set; }

        public int? makhoa { get; set; }

        public int? machinhanh { get; set; }

        [StringLength(50)]
        public string tendn { get; set; }

        [StringLength(50)]
        public string mk { get; set; }

        public int? quyen { get; set; }

        public string anh { get; set; }

        public int? trangthai { get; set; }

		[StringLength(50)]
		public string ResetPasswordCode { get; set; }

		public DateTime? ResetPasswordCodeExpiry { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiViet> BaiViets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CaKham> CaKhams { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoiDap> HoiDaps { get; set; }

        public virtual Khoa Khoa { get; set; }
        public virtual ICollection<BenhAn> BenhAns { get; set; }
    }
}
