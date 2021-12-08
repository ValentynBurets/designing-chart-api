using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("0cd7fc69-dae4-4cab-9a39-6e360ad7dcd8"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("42e521f5-b8eb-4493-ae04-e7fd2b84c3da"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("b42d7992-8e08-408e-85d9-8483c4c978d2"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("fe208cc2-d5af-483f-a56b-d687ad544d1d"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("26e9767b-a372-4209-9800-ba07637fbe0e"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("26f0d744-da7d-4241-893a-c80a16d922b1"));

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: new Guid("2afd1a8c-5912-45bd-9211-b5b4764fd4a8"));

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: new Guid("c0a1d593-2e33-4311-81ee-5e9d9cbc2304"));

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("e068e57b-ccfd-4ec5-8c70-20a7d7a2f636"), "CategoryType" });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("130d04d8-4623-4c11-8e35-67df14a0a891"), "SecondCategoryType" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Description", "EtalonChart", "ExpirationDate", "MaxMark", "StatusType", "Title" },
                values: new object[] { new Guid("c781315f-4af0-4c63-a6a3-31b87699f6a2"), new Guid("e068e57b-ccfd-4ec5-8c70-20a7d7a2f636"), "description for exercise", "test chart", new DateTime(2000, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 0, "exercise title" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Description", "EtalonChart", "ExpirationDate", "MaxMark", "StatusType", "Title" },
                values: new object[] { new Guid("c09c4243-e647-4aa2-b117-9f3442f95e19"), new Guid("130d04d8-4623-4c11-8e35-67df14a0a891"), "second description for exercise", "second test chart", new DateTime(2042, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, 3, "second exercise title" });

            migrationBuilder.InsertData(
                table: "Attempts",
                columns: new[] { "Id", "Chart", "ExerciseId", "FinishTime", "Mark", "StartTime", "StudentId" },
                values: new object[,]
                {
                    { new Guid("00bb3458-8225-4906-a0b3-63a11d5685f1"), "created chart from student", new Guid("c781315f-4af0-4c63-a6a3-31b87699f6a2"), new DateTime(2033, 5, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), 32.0, new DateTime(2033, 5, 21, 8, 30, 52, 0, DateTimeKind.Unspecified), new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36") },
                    { new Guid("faa391c9-add3-4baa-9216-3c9365bf2a8b"), "created 2 chart from student", new Guid("c781315f-4af0-4c63-a6a3-31b87699f6a2"), new DateTime(2032, 5, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), 42.0, new DateTime(2032, 5, 21, 8, 30, 52, 0, DateTimeKind.Unspecified), new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36") },
                    { new Guid("cb0fa235-ba05-4e71-977c-aff885dfb2d0"), "created chart from student", new Guid("c781315f-4af0-4c63-a6a3-31b87699f6a2"), new DateTime(2033, 6, 25, 7, 12, 23, 0, DateTimeKind.Unspecified), 32.0, new DateTime(2033, 6, 25, 6, 32, 53, 0, DateTimeKind.Unspecified), new Guid("cd6c466a-6b9d-414b-6394-08d9ba292180") },
                    { new Guid("3404f021-c85d-4e89-995e-8013291f9307"), "created chart from student", new Guid("c09c4243-e647-4aa2-b117-9f3442f95e19"), new DateTime(2033, 5, 11, 10, 30, 52, 0, DateTimeKind.Unspecified), 45.0, new DateTime(2033, 5, 11, 9, 25, 35, 0, DateTimeKind.Unspecified), new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("00bb3458-8225-4906-a0b3-63a11d5685f1"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("3404f021-c85d-4e89-995e-8013291f9307"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("cb0fa235-ba05-4e71-977c-aff885dfb2d0"));

            migrationBuilder.DeleteData(
                table: "Attempts",
                keyColumn: "Id",
                keyValue: new Guid("faa391c9-add3-4baa-9216-3c9365bf2a8b"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("c09c4243-e647-4aa2-b117-9f3442f95e19"));

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: new Guid("c781315f-4af0-4c63-a6a3-31b87699f6a2"));

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: new Guid("130d04d8-4623-4c11-8e35-67df14a0a891"));

            migrationBuilder.DeleteData(
                table: "CategoryTypes",
                keyColumn: "Id",
                keyValue: new Guid("e068e57b-ccfd-4ec5-8c70-20a7d7a2f636"));

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("2afd1a8c-5912-45bd-9211-b5b4764fd4a8"), "CategoryType" });

            migrationBuilder.InsertData(
                table: "CategoryTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c0a1d593-2e33-4311-81ee-5e9d9cbc2304"), "SecondCategoryType" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Description", "EtalonChart", "ExpirationDate", "MaxMark", "StatusType", "Title" },
                values: new object[] { new Guid("26f0d744-da7d-4241-893a-c80a16d922b1"), new Guid("2afd1a8c-5912-45bd-9211-b5b4764fd4a8"), "description for exercise", "test chart", new DateTime(2000, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 0, "exercise title" });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Description", "EtalonChart", "ExpirationDate", "MaxMark", "StatusType", "Title" },
                values: new object[] { new Guid("26e9767b-a372-4209-9800-ba07637fbe0e"), new Guid("c0a1d593-2e33-4311-81ee-5e9d9cbc2304"), "second description for exercise", "second test chart", new DateTime(2042, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, 3, "second exercise title" });

            migrationBuilder.InsertData(
                table: "Attempts",
                columns: new[] { "Id", "Chart", "ExerciseId", "FinishTime", "Mark", "StartTime", "StudentId" },
                values: new object[,]
                {
                    { new Guid("0cd7fc69-dae4-4cab-9a39-6e360ad7dcd8"), "created chart from student", new Guid("26f0d744-da7d-4241-893a-c80a16d922b1"), new DateTime(2033, 5, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), 32.0, new DateTime(2033, 5, 21, 8, 30, 52, 0, DateTimeKind.Unspecified), new Guid("879d65a3-87ab-41ba-83cd-08d9b5dca1a1") },
                    { new Guid("42e521f5-b8eb-4493-ae04-e7fd2b84c3da"), "created chart from student", new Guid("26f0d744-da7d-4241-893a-c80a16d922b1"), new DateTime(2033, 5, 21, 9, 30, 0, 0, DateTimeKind.Unspecified), 62.0, new DateTime(2033, 5, 21, 8, 30, 52, 0, DateTimeKind.Unspecified), new Guid("879d65a3-87ab-41ba-83cd-08d9b5dca1a1") },
                    { new Guid("b42d7992-8e08-408e-85d9-8483c4c978d2"), "created chart from student", new Guid("26f0d744-da7d-4241-893a-c80a16d922b1"), new DateTime(2033, 6, 25, 7, 12, 23, 0, DateTimeKind.Unspecified), 32.0, new DateTime(2033, 6, 25, 6, 32, 53, 0, DateTimeKind.Unspecified), new Guid("8ac2b3ac-1f7c-4ceb-6e4a-08d9b8d6db0e") },
                    { new Guid("fe208cc2-d5af-483f-a56b-d687ad544d1d"), "created chart from student", new Guid("26e9767b-a372-4209-9800-ba07637fbe0e"), new DateTime(2033, 5, 11, 10, 30, 52, 0, DateTimeKind.Unspecified), 45.0, new DateTime(2033, 5, 11, 9, 25, 35, 0, DateTimeKind.Unspecified), new Guid("879d65a3-87ab-41ba-83cd-08d9b5dca1a1") }
                });
        }
    }
}
