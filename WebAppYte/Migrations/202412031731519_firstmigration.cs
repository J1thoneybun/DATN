namespace WebAppYte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaiViet",
                c => new
                    {
                        mabv = c.Int(nullable: false, identity: true),
                        tieude = c.String(),
                        noidung = c.String(),
                        hinhanh = c.String(maxLength: 100),
                        mota = c.String(),
                        ngaydang = c.DateTime(storeType: "date"),
                        maloai = c.Int(),
                        mand = c.Int(),
                    })
                .PrimaryKey(t => t.mabv)
                .ForeignKey("dbo.Loai", t => t.maloai)
                .ForeignKey("dbo.NguoiDung", t => t.mand)
                .Index(t => t.maloai)
                .Index(t => t.mand);
            
            CreateTable(
                "dbo.Loai",
                c => new
                    {
                        maloai = c.Int(nullable: false, identity: true),
                        tenloai = c.String(),
                    })
                .PrimaryKey(t => t.maloai);
            
            CreateTable(
                "dbo.NguoiDung",
                c => new
                    {
                        mand = c.Int(nullable: false, identity: true),
                        hoten = c.String(nullable: false, maxLength: 50),
                        diachi = c.String(maxLength: 50),
                        ngaysinh = c.DateTime(storeType: "date"),
                        gioitinh = c.String(maxLength: 5),
                        sdt = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        email = c.String(maxLength: 30, fixedLength: true, unicode: false),
                        chucvu = c.String(),
                        hocham = c.String(),
                        hocvi = c.String(),
                        gioithieu = c.String(),
                        makhoa = c.Int(),
                        machinhanh = c.Int(),
                        tendn = c.String(maxLength: 50, unicode: false),
                        mk = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        quyen = c.Int(),
                        anh = c.String(),
                        trangthai = c.Int(),
                    })
                .PrimaryKey(t => t.mand)
                .ForeignKey("dbo.ChiNhanh", t => t.machinhanh)
                .ForeignKey("dbo.Khoa", t => t.makhoa)
                .Index(t => t.makhoa)
                .Index(t => t.machinhanh);
            
            CreateTable(
                "dbo.BenhAn",
                c => new
                    {
                        maba = c.Int(nullable: false, identity: true),
                        mabn = c.Int(nullable: false),
                        mabs = c.Int(nullable: false),
                        tieude = c.String(),
                        ngaykham = c.DateTime(nullable: false),
                        giokham = c.DateTime(nullable: false),
                        mach = c.Double(nullable: false),
                        nhietdo = c.Double(nullable: false),
                        nhiptho = c.Double(nullable: false),
                        chieucao = c.Double(nullable: false),
                        cannang = c.Double(nullable: false),
                        bmi = c.Double(nullable: false),
                        thiluctrai = c.Double(nullable: false),
                        thilucphai = c.Double(nullable: false),
                        nhanapP = c.Double(nullable: false),
                        nhanapT = c.Double(nullable: false),
                        trangthai = c.Int(nullable: false),
                        chuandoan = c.String(),
                        ketqua = c.String(),
                    })
                .PrimaryKey(t => t.maba)
                .ForeignKey("dbo.NguoiDung", t => t.mabs, cascadeDelete: true)
                .ForeignKey("dbo.BenhNhan", t => t.mabn, cascadeDelete: true)
                .Index(t => t.mabn)
                .Index(t => t.mabs);
            
            CreateTable(
                "dbo.BenhNhan",
                c => new
                    {
                        mabn = c.Int(nullable: false, identity: true),
                        tenbn = c.String(nullable: false, maxLength: 50),
                        sdt = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        email = c.String(maxLength: 30, fixedLength: true, unicode: false),
                        diachi = c.String(maxLength: 50),
                        ngaysinh = c.DateTime(storeType: "date"),
                        gioitinh = c.String(maxLength: 5),
                        tendn = c.String(maxLength: 50, unicode: false),
                        mk = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        trangthai = c.Int(),
                    })
                .PrimaryKey(t => t.mabn);
            
            CreateTable(
                "dbo.DanhGia",
                c => new
                    {
                        madanhgia = c.Int(nullable: false, identity: true),
                        ngay = c.DateTime(storeType: "date"),
                        noidung = c.String(),
                        mand = c.Int(),
                        mabn = c.Int(),
                        rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.madanhgia)
                .ForeignKey("dbo.BenhNhan", t => t.mabn)
                .ForeignKey("dbo.NguoiDung", t => t.mand)
                .Index(t => t.mand)
                .Index(t => t.mabn);
            
            CreateTable(
                "dbo.HoiDap",
                c => new
                    {
                        ma = c.Int(nullable: false, identity: true),
                        hoi = c.String(),
                        ngayhoi = c.DateTime(),
                        ngaytl = c.DateTime(),
                        dap = c.String(),
                        mand = c.Int(),
                        mabn = c.Int(),
                        trangthai = c.Int(),
                    })
                .PrimaryKey(t => t.ma)
                .ForeignKey("dbo.BenhNhan", t => t.mabn)
                .ForeignKey("dbo.NguoiDung", t => t.mand)
                .Index(t => t.mand)
                .Index(t => t.mabn);
            
            CreateTable(
                "dbo.CaKham",
                c => new
                    {
                        maca = c.Int(nullable: false, identity: true),
                        ngaykham = c.DateTime(storeType: "date"),
                        hinhthuc = c.String(maxLength: 30),
                        ca = c.String(maxLength: 100, fixedLength: true, unicode: false),
                        mand = c.Int(),
                        trangthai = c.Int(),
                    })
                .PrimaryKey(t => t.maca)
                .ForeignKey("dbo.NguoiDung", t => t.mand)
                .Index(t => t.mand);
            
            CreateTable(
                "dbo.DatLich",
                c => new
                    {
                        madat = c.Int(nullable: false, identity: true),
                        ngaydat = c.DateTime(storeType: "date"),
                        mota = c.String(),
                        sdt = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        hoten = c.String(maxLength: 30),
                        ngaysinh = c.DateTime(storeType: "date"),
                        trangthai = c.Int(),
                        maca = c.Int(),
                        mabn = c.Int(),
                    })
                .PrimaryKey(t => t.madat)
                .ForeignKey("dbo.CaKham", t => t.maca)
                .Index(t => t.maca);
            
            CreateTable(
                "dbo.ChiNhanh",
                c => new
                    {
                        machinhanh = c.Int(nullable: false, identity: true),
                        diachi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.machinhanh);
            
            CreateTable(
                "dbo.Khoa",
                c => new
                    {
                        makhoa = c.Int(nullable: false, identity: true),
                        tenkhoa = c.String(maxLength: 50),
                        mota = c.String(),
                    })
                .PrimaryKey(t => t.makhoa);
            
            CreateTable(
                "dbo.DangNhap",
                c => new
                    {
                        ma = c.Int(nullable: false, identity: true),
                        tendn = c.String(maxLength: 30, fixedLength: true, unicode: false),
                        mk = c.String(maxLength: 30, fixedLength: true, unicode: false),
                        quyen = c.Int(),
                        mand = c.Int(),
                    })
                .PrimaryKey(t => t.ma);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NguoiDung", "makhoa", "dbo.Khoa");
            DropForeignKey("dbo.NguoiDung", "machinhanh", "dbo.ChiNhanh");
            DropForeignKey("dbo.CaKham", "mand", "dbo.NguoiDung");
            DropForeignKey("dbo.DatLich", "maca", "dbo.CaKham");
            DropForeignKey("dbo.BenhAn", "mabn", "dbo.BenhNhan");
            DropForeignKey("dbo.HoiDap", "mand", "dbo.NguoiDung");
            DropForeignKey("dbo.HoiDap", "mabn", "dbo.BenhNhan");
            DropForeignKey("dbo.DanhGia", "mand", "dbo.NguoiDung");
            DropForeignKey("dbo.DanhGia", "mabn", "dbo.BenhNhan");
            DropForeignKey("dbo.BenhAn", "mabs", "dbo.NguoiDung");
            DropForeignKey("dbo.BaiViet", "mand", "dbo.NguoiDung");
            DropForeignKey("dbo.BaiViet", "maloai", "dbo.Loai");
            DropIndex("dbo.DatLich", new[] { "maca" });
            DropIndex("dbo.CaKham", new[] { "mand" });
            DropIndex("dbo.HoiDap", new[] { "mabn" });
            DropIndex("dbo.HoiDap", new[] { "mand" });
            DropIndex("dbo.DanhGia", new[] { "mabn" });
            DropIndex("dbo.DanhGia", new[] { "mand" });
            DropIndex("dbo.BenhAn", new[] { "mabs" });
            DropIndex("dbo.BenhAn", new[] { "mabn" });
            DropIndex("dbo.NguoiDung", new[] { "machinhanh" });
            DropIndex("dbo.NguoiDung", new[] { "makhoa" });
            DropIndex("dbo.BaiViet", new[] { "mand" });
            DropIndex("dbo.BaiViet", new[] { "maloai" });
            DropTable("dbo.DangNhap");
            DropTable("dbo.Khoa");
            DropTable("dbo.ChiNhanh");
            DropTable("dbo.DatLich");
            DropTable("dbo.CaKham");
            DropTable("dbo.HoiDap");
            DropTable("dbo.DanhGia");
            DropTable("dbo.BenhNhan");
            DropTable("dbo.BenhAn");
            DropTable("dbo.NguoiDung");
            DropTable("dbo.Loai");
            DropTable("dbo.BaiViet");
        }
    }
}
