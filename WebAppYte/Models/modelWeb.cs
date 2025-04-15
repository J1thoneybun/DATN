using System.Data.Entity;

namespace WebAppYte.Models
{
    public partial class modelWeb : DbContext
    {
        public modelWeb()
            : base("name=modelWeb")
        {
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<BenhNhan> BenhNhans { get; set; }
        public virtual DbSet<BenhAn> BenhAns { get; set; }
        public virtual DbSet<CaKham> CaKhams { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public virtual DbSet<DangNhap> DangNhaps { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DatLich> DatLiches { get; set; }
        public virtual DbSet<HoiDap> HoiDaps { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public object CauHois { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.tendn)
                .IsUnicode(false);

            modelBuilder.Entity<BenhNhan>()
                .Property(e => e.mk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CaKham>()
                .Property(e => e.ca)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.tendn)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.mk)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DatLich>()
                .Property(e => e.sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.tendn)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.mk)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
