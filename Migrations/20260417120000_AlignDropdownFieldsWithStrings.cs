using DPEDAdmissionSystem.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DPEDAdmissionSystem.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260417120000_AlignDropdownFieldsWithStrings")]
public partial class AlignDropdownFieldsWithStrings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            IF OBJECT_ID(N'[Applications]', N'U') IS NOT NULL
            BEGIN
                IF COL_LENGTH(N'[Applications]', N'Gender') IS NOT NULL
                BEGIN
                    ALTER TABLE [Applications] ALTER COLUMN [Gender] nvarchar(20) NULL;

                    UPDATE [Applications]
                    SET [Gender] = CASE [Gender]
                        WHEN N'0' THEN N'Male'
                        WHEN N'1' THEN N'Female'
                        WHEN N'2' THEN N'Other'
                        ELSE ISNULL(NULLIF([Gender], N''), N'')
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [Gender] nvarchar(20) NOT NULL;
                END;

                IF COL_LENGTH(N'[Applications]', N'MaritalStatus') IS NOT NULL
                BEGIN
                    ALTER TABLE [Applications] ALTER COLUMN [MaritalStatus] nvarchar(20) NULL;

                    UPDATE [Applications]
                    SET [MaritalStatus] = CASE [MaritalStatus]
                        WHEN N'0' THEN N'Single'
                        WHEN N'1' THEN N'Married'
                        WHEN N'2' THEN N'Other'
                        ELSE ISNULL(NULLIF([MaritalStatus], N''), N'')
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [MaritalStatus] nvarchar(20) NOT NULL;
                END;

                IF COL_LENGTH(N'[Applications]', N'Category') IS NOT NULL
                BEGIN
                    ALTER TABLE [Applications] ALTER COLUMN [Category] nvarchar(50) NULL;

                    UPDATE [Applications]
                    SET [Category] = CASE [Category]
                        WHEN N'0' THEN N'General'
                        WHEN N'1' THEN N'SC (R&O)'
                        WHEN N'2' THEN N'SC (M&B)'
                        WHEN N'3' THEN N'BC'
                        WHEN N'4' THEN N'EWS'
                        WHEN N'5' THEN N'Ex-Serviceman'
                        WHEN N'6' THEN N'Sports'
                        WHEN N'7' THEN N'Physically Handicapped'
                        ELSE ISNULL(NULLIF([Category], N''), N'')
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [Category] nvarchar(50) NOT NULL;
                END;
            END;
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            IF OBJECT_ID(N'[Applications]', N'U') IS NOT NULL
            BEGIN
                IF COL_LENGTH(N'[Applications]', N'Gender') IS NOT NULL
                BEGIN
                    UPDATE [Applications]
                    SET [Gender] = CASE [Gender]
                        WHEN N'Male' THEN N'0'
                        WHEN N'Female' THEN N'1'
                        WHEN N'Other' THEN N'2'
                        ELSE NULL
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [Gender] int NULL;
                END;

                IF COL_LENGTH(N'[Applications]', N'MaritalStatus') IS NOT NULL
                BEGIN
                    UPDATE [Applications]
                    SET [MaritalStatus] = CASE [MaritalStatus]
                        WHEN N'Single' THEN N'0'
                        WHEN N'Married' THEN N'1'
                        WHEN N'Other' THEN N'2'
                        ELSE NULL
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [MaritalStatus] int NULL;
                END;

                IF COL_LENGTH(N'[Applications]', N'Category') IS NOT NULL
                BEGIN
                    UPDATE [Applications]
                    SET [Category] = CASE [Category]
                        WHEN N'General' THEN N'0'
                        WHEN N'SC (R&O)' THEN N'1'
                        WHEN N'SC (M&B)' THEN N'2'
                        WHEN N'BC' THEN N'3'
                        WHEN N'EWS' THEN N'4'
                        WHEN N'Ex-Serviceman' THEN N'5'
                        WHEN N'Sports' THEN N'6'
                        WHEN N'Physically Handicapped' THEN N'7'
                        ELSE NULL
                    END;

                    ALTER TABLE [Applications] ALTER COLUMN [Category] int NULL;
                END;
            END;
            """);
    }
}
