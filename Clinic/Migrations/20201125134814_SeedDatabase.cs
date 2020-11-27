using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Patients (Name,Gender,DateOfBirth,Address,PhoneNumber,CreatedDate) values('Hồ Thị Cẩm Hoài','Nữ','2005-12-15','873 Lê Hồng Phong, Q5, TpHCM', '0903655711', '2020-02-03')");
            migrationBuilder.Sql("insert into Patients (Name,Gender,DateOfBirth,Address,PhoneNumber,CreatedDate) values('Lê Tấn Hoàng','Nam','1973-11-05','Linh Trung, Thủ Đức, TpHCM','0735425911','2020-12-22')");
            migrationBuilder.Sql("insert into Patients (Name,Gender,DateOfBirth,Address,PhoneNumber,CreatedDate) values('Bùi Trần Nhật Lệ','Nữ','1981-11-09','4/34B Nguyen Trai, Q1, TpHCM','0735711197','2018-01-02')");
            migrationBuilder.Sql("insert into Patients (Name,Gender,DateOfBirth,Address,PhoneNumber,CreatedDate) values('Cao Thành Nghĩa','Nam','1960-11-06','126 Le Hong Phong, Q5, TpHCM','0711197198','2018-01-11')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Patients");
        }
    }
}
