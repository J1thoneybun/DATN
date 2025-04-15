namespace WebAppYte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaKham")]
    public partial class CaKham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaKham()
        {
            DatLiches = new HashSet<DatLich>();
        }

        [Key]
        public int maca { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ngaykham { get; set; }

        [StringLength(30)]
        public string hinhthuc { get; set; }

        [StringLength(100)]
        public string ca { get; set; }

        public int? mand { get; set; }

        public int? trangthai { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatLich> DatLiches { get; set; }
    }
}