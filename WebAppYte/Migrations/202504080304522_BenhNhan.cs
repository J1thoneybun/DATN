namespace WebAppYte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BenhNhan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NguoiDung", "ResetPasswordCode", c => c.String(maxLength: 50));
            AddColumn("dbo.NguoiDung", "ResetPasswordCodeExpiry", c => c.DateTime());
            AddColumn("dbo.BenhNhan", "ResetPasswordCode", c => c.String(maxLength: 100));
            AddColumn("dbo.BenhNhan", "ResetPasswordCodeExpiry", c => c.DateTime());
            AlterColumn("dbo.NguoiDung", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NguoiDung", "Email", c => c.String(maxLength: 30, fixedLength: true, unicode: false));
            DropColumn("dbo.BenhNhan", "ResetPasswordCodeExpiry");
            DropColumn("dbo.BenhNhan", "ResetPasswordCode");
            DropColumn("dbo.NguoiDung", "ResetPasswordCodeExpiry");
            DropColumn("dbo.NguoiDung", "ResetPasswordCode");
        }
    }
}
