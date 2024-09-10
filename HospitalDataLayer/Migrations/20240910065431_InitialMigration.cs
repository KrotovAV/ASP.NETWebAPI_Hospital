using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalDataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cabinet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthdayDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CabinetId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_Cabinet",
                        column: x => x.CabinetId,
                        principalTable: "Cabinet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctor_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctor_Specialization",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cabinet",
                columns: new[] { "Id", "Num" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 2, 12 },
                    { 3, 13 },
                    { 4, 21 },
                    { 5, 22 },
                    { 6, 23 },
                    { 7, 24 },
                    { 8, 25 }
                });

            migrationBuilder.InsertData(
                table: "District",
                columns: new[] { "Id", "Num" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Specialization",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Педиатр" },
                    { 2, "Кардиолог" },
                    { 3, "Хирург" },
                    { 4, "Окулист" },
                    { 5, "Терапевт" }
                });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "Id", "CabinetId", "DistrictId", "FullName", "SpecializationId" },
                values: new object[,]
                {
                    { 1, 1, 1, "Сергеев Сергей Сергеевич", 1 },
                    { 2, 2, 1, "Иванов Иван Иванович", 2 },
                    { 3, 3, 1, "Дмитриев Дмитрий Дмитриевич", 3 },
                    { 4, 4, 1, "Александров Александр Александрович", 4 },
                    { 5, 5, 2, "Михайлов Михаил Михайлович", 5 },
                    { 6, 6, 2, "Романов Роман Романович", 5 },
                    { 7, 7, 3, "Викторов Виктор Викторович", 1 }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "Id", "Address", "BirthdayDate", "DistrictId", "Gender", "Name", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 1, "Тенистая, д.11, кв.1", new DateTime(1991, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, true, "Петр", "Петрович", "Петров" },
                    { 2, "Тенистая, д.22, кв.2", new DateTime(1992, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, "Ольгович", "Олеговна", "Ольга" },
                    { 3, "Горная, д.33, кв.3", new DateTime(1993, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, "Пашутов", "Павлович", "Павел" },
                    { 4, "Горная, д.44, кв.4", new DateTime(1994, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, true, "Васильев", "Васильевич", "Василий" },
                    { 5, "Васильковая, д.55, кв.5", new DateTime(1995, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, "Антонов", "Антонович", "Антон" },
                    { 6, "Васильковая, д.66, кв.6", new DateTime(1996, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, true, "Геннадьев", "Геннадьевич", "Геннадий" },
                    { 7, "Васильковая, д.77, кв.7", new DateTime(1997, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, "Александрова", "Александровна", "Александра" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_CabinetId",
                table: "Doctor",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DistrictId",
                table: "Doctor",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_SpecializationId",
                table: "Doctor",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DistrictId",
                table: "Patient",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Cabinet");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "District");
        }
    }
}
