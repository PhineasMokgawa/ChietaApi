using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CHIETAMIS.Migrations
{
    public partial class AddMobileNotificationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ikp_Home_Language_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Language_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_Home_Language_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ikp_Nationality_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_Nationality_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ikp_OFO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OFO_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_OFO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ikp_Province_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province_Code = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_Province_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ikp_SETA_",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SETA_Id = table.Column<int>(type: "int", nullable: false),
                    Abrev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_SETA_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ikp_SIC_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIC_Code = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ikp_SIC_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LEVY_PAYMENTSs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PERIOD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SDL_NO = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RECEIPT_DATE_SARS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LEVY_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PENALTY_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    INTEREST_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TOTAL_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NO_SDL201_OUTSTANDING = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DEBT_OUTSTANDING_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SARS_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SARS_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SARS_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NSF_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NSF_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NSF_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_SETUP_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_SETUP_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_SETUP_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_ADMIN_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_ADMIN_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SETA_ADMIN_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UNAPPORTIONED_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UNAPPORTIONED_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UNAPPORTIONED_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GRANT_A = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GRANT_B = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GRANT_C = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GRANT_D = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FINANCIAL_YEAR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LEVY_TYPE = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    SETA_CODE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_A_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_A_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_A_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_A_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_B_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_B_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_B_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_B_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_B_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_B_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_B_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_C_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_C_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_C_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_C_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_C_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_C_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_C_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_D_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_D_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_D_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_D_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_D_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_D_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_D_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_E_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_E_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_E_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_E_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_E_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_E_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_E_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_OVERRIDDEN_AMOUNT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_B_OVERRIDDEN_AMOUNT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_C_OVERRIDDEN_AMOUNT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_D_OVERRIDDEN_AMOUNT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_E_OVERRIDDEN_AMOUNT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PROOF_OF_PAYMENT_RECEIVED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    TOTAL_GRANT_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_B_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_C_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_D_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_E_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_A_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_B_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_C_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_D_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_E_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_A_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_B_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_C_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_D_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_E_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_E = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_A_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_B_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_C_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_D_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_E_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_B_USER_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_C_USER_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    LEVY_AMOUNT_RECEIVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    INTEREST_AMOUNT_RECEIVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PENALTY_AMOUNT_RECEIVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOTAL_AMOUNT_RECEIVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SETA_COMPLETE_ADMIN_LEVY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SETA_COMPLETE_ADMIN_INTEREST = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SETA_COMPLETE_ADMIN_PENALTY = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SETA_COMPLETE_ADMIN_TOTAL = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_DG = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_MG_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_MG_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_MG_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_MG_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG_OVERRIDDEN_AMT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_MG_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_MG_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_MG_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_MG_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_MG_USER_COMMENT = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    GRANT_DG_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_DG_STATUS_COMMENT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    GRANT_DG_DISBURSED_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GRANT_DG_DISBURSED_BY = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GRANT_DG_APPROVED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_DG_DECLINED = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_DG_SWEPT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_DG_OVERRIDDEN_AMT_DIFF = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GRANT_DG_PROCESSED = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    GRANT_DG_PAYMENT_STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_DG_CHEQUE_EFT_NO = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GRANT_DG_BATCH_NO = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GRANT_DG_USER_COMMENT = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    statusOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusTwo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEVY_PAYMENTSs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_AdminCrit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsBased = table.Column<bool>(type: "bit", nullable: false),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_AdminCrit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Alternate_Id_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alternate_Id_Type_Id = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DGInd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Alternate_Id_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Bank_Account_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIntegrateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Bank_Account_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_BBBEE_Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_BBBEE_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_BBBStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_BBBStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Chambers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Chamber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Chambers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Citizen_Resident_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizen_Resident_Status_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Citizen_Resident_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_CompanyCompliance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_CompanyCompliance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_CompanySizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_CompanySizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_CompanyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_CompanyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Counters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N_Last_Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Counters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Dg_Payment_Tranche_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tranche_Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Dg_Payment_Tranche_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_DiscLearnerType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_DiscLearnerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Discretionary_Lesedi_Qualification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Discretionary_Lesedi_Qualification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Discretionary_ProgrammeDeliverables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    TrancheTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliverableId = table.Column<int>(type: "int", nullable: false),
                    Deliverable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliesTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranchePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Discretionary_ProgrammeDeliverables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Discretionary_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Discretionary_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Discretionary_Universtity_College",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityCollegeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Discretionary_Universtity_College", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_EmploymentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employment_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_EmploymentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Equity_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equity_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Equity_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_EvaluationMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvalMthdCd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvalMthdDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_EvaluationMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Fintegrate_Payment_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Fintegrate_Payment_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_FocusArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FocusAreaCd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FocusAreaDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundinglImit = table.Column<int>(type: "int", nullable: false),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_FocusArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_FocusCritEval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FocusAreaKey = table.Column<short>(type: "smallint", nullable: false),
                    AdminCritKey = table.Column<short>(type: "smallint", nullable: false),
                    EvalMthdCd = table.Column<short>(type: "smallint", nullable: false),
                    ProjTypCD = table.Column<short>(type: "smallint", nullable: false),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false),
                    AllowNew = table.Column<bool>(type: "bit", nullable: false),
                    AllowContinuing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_FocusCritEval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Gender_Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Gender_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Grant_Approval_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantStatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Grant_Approval_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Grant_Approval_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Grant_Approval_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Grant_Deliverable_Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Delivertable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obligation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrancheTypeId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Grant_Deliverable_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_HistoricalPerformance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_HistoricalPerformance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Learning_Programme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Learning_Programmes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Learning_Programme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Lesedi_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Lesedi_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_MainPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MP_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MN_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DC_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_MainPlace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mand_Pivotal_Programmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pivotal_Programme = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mand_Pivotal_Programmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Approval_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Approval_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_ExtensionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_ExtensionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Grant_Achievement_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Achievement_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Grant_Achievement_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Grant_Qualification_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qualification_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NQF_Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Band = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Grant_Qualification_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Grants_Gap_Reason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gap_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Grants_Gap_Reason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Grants_Scarce_Reason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scarce_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Grants_Scarce_Reason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Grants_Target_Beneficiary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Target_Beneficiary = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Grants_Target_Beneficiary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Programmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Programme_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Programme = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Programmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatory_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatory_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Mandatoty_Grants_Impact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Impact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Mandatoty_Grants_Impact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Occupation_Level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupational_Levels = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Occupation_Level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_OFO_Specialization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OFO_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specilization = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_OFO_Specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Payment_Tranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    Tranche_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Payment_Tranches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Person_Title",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Person_Title", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_PostalCodeMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuralUrban = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_PostalCodeMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_PostalCodeProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province_Code = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_PostalCodeProvince", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_PostCodeMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TownCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_PostCodeMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_ProgrammeDeliverables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    TrancheScheduleId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: false),
                    SubcategoryId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_ProgrammeDeliverables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Project_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjTypCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjTypDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Project_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Region_Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Region_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Region_RSA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    RSA_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Region_RSA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_RegionProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_RegionProvince", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_sdf_designation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation_Code = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_sdf_designation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Specialist_Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Specialist_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_SqmrApp_Indicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Indicator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_SqmrApp_Indicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_SubPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SP_CODE = table.Column<int>(type: "int", nullable: false),
                    SP_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MN_CODE = table.Column<int>(type: "int", nullable: false),
                    MN_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DC_MN_C = table.Column<int>(type: "int", nullable: false),
                    DC_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PR_CODE = table.Column<int>(type: "int", nullable: false),
                    PR_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_SubPlace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Tranche_Approval_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Tranche_Approval_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lkp_Vision2025Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Goal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lkp_Vision2025Goals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MandatoryGrantPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SDL_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipFileId = table.Column<int>(type: "int", nullable: false),
                    GrantYear = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    zipfileid = table.Column<int>(type: "int", nullable: false),
                    ChietaAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chieta_Code1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgName_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Account_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Bank_Account_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDLCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandatoryGrantPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLearner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    LearnerId = table.Column<int>(type: "int", nullable: false),
                    WorkplaceId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLearner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ApplicationBatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    TrancheType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ApplicationBatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ApplicationTranche",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    TrancheType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrancheStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current_Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchId = table.Column<int>(type: "int", nullable: true),
                    ProgrammeTypeId = table.Column<int>(type: "int", nullable: true),
                    FocusAreaId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    TrancheAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    New_Learners = table.Column<int>(type: "int", nullable: true),
                    Continuing = table.Column<int>(type: "int", nullable: true),
                    Number_of_Learners = table.Column<int>(type: "int", nullable: true),
                    CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Usrupd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ApplicationTranche", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ApplicationTrancheDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationTrancheId = table.Column<int>(type: "int", nullable: false),
                    LearnerDetailsId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ApplicationTranceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current_Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ApplicationTrancheDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    Account_Holder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BankDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BankingList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipFileId = table.Column<int>(type: "int", nullable: false),
                    SDL_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChietaAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chieta_Code1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgName_Cde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Account_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDLCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatorUserId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BankingList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Bursary_DocumentApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bursary_DocumentApprovals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discrationary_Tranche_Batch_Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheBatchId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discrationary_Tranche_Batch_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Bursary_Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantWindowId = table.Column<int>(type: "int", nullable: false),
                    ApplicationStatusId = table.Column<int>(type: "int", nullable: false),
                    LesediId = table.Column<int>(type: "int", nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    SubmittedBy = table.Column<int>(type: "int", nullable: true),
                    SubmissionDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Bursary_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Bursary_Approvals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    IdCopy = table.Column<bool>(type: "bit", nullable: false),
                    Statement = table.Column<bool>(type: "bit", nullable: false),
                    Results = table.Column<bool>(type: "bit", nullable: false),
                    Registration = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutcomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Bursary_Approvals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Details_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Details_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GAC_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GAC_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GACR_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GACR_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GC_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GC_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GCR_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GCR_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GEC_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GEC_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_GECR_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_GECR_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Grant_Approvals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    BBBEE = table.Column<bool>(type: "bit", nullable: false),
                    TaxClearance = table.Column<bool>(type: "bit", nullable: false),
                    BankLetter = table.Column<bool>(type: "bit", nullable: false),
                    DeclarationOfInterest = table.Column<bool>(type: "bit", nullable: false),
                    ProjectProposal = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OutcomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Grant_Approvals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Grant_DocumentApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Grant_DocumentApprovals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Grant_Window",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgCd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaunchDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotBdgt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: false),
                    DteCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Grant_Window", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Learner_Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrancheType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectLearnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Learner_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_LearnerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    MoA_Contract_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Funded = table.Column<bool>(type: "bit", nullable: false),
                    Contracted_Learning_Achievement_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Learner_Enrolment_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Learning_Programme_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subcategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intervention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_Date_of_Training = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End_Date_of_Training = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Youth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birth_Year = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SA_Citizen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employment_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unemployed_Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home_Address_Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guardian_ID_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guardian_Full_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Urban_Rural = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cell_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupational_Levels_For_Equity_Reporting_Purposes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Occupation_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Specialisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highest_School_Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highest_Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Student_Enrolment_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bursary_Academic_Year_of_Study = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bursary_Completion_Status_Final_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POPI_Act_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POPI_Act_Status_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Workplace_Legal_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Legal_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_LearnerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Lesedi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityCollege = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: false),
                    OtherQualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyStudying = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnderPostGraduate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NSFASBeneficiary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentHist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PassRate = table.Column<int>(type: "int", nullable: false),
                    ConsentYN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Lesedi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Lesedi_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contactnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdUsr = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Lesedi_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    ProjectStatusID = table.Column<int>(type: "int", nullable: false),
                    ProjectStatDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjShortNam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantWindowId = table.Column<int>(type: "int", nullable: false),
                    WindowParamId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    SubmittedBy = table.Column<int>(type: "int", nullable: false),
                    SubmissionDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotInitReq = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotProj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotReq = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotFund = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotExpend = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotRcvd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotDisb = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotGrant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjMgrID = table.Column<int>(type: "int", nullable: false),
                    Auditor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuditYyIncl = table.Column<short>(type: "smallint", nullable: false),
                    POAppr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POApprDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POApprNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinAppr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinApprDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinApprNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileLoctn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoLoctn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaptureDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextLogNum = table.Column<short>(type: "smallint", nullable: false),
                    ArchiveYN = table.Column<bool>(type: "bit", nullable: false),
                    ArchSetDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GISLong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GISLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotOwnCntrb = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotAddFund = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RSAId = table.Column<int>(type: "int", nullable: true),
                    RSAAssignedBy = table.Column<int>(type: "int", nullable: true),
                    RSAAssignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Project_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    OtherIntervention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FocusCritEvalId = table.Column<int>(type: "int", nullable: false),
                    Number_Continuing = table.Column<int>(type: "int", nullable: false),
                    Number_New = table.Column<int>(type: "int", nullable: false),
                    CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HDI = table.Column<int>(type: "int", nullable: false),
                    Female = table.Column<int>(type: "int", nullable: false),
                    Youth = table.Column<int>(type: "int", nullable: false),
                    Number_Disabled = table.Column<int>(type: "int", nullable: false),
                    Rural = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Project_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Project_Details_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    SubmittedBy = table.Column<int>(type: "int", nullable: true),
                    SubmissionDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApplicationStatusId = table.Column<int>(type: "int", nullable: true),
                    Contract_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current_Approver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FocusAreaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    OtherIntervention = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FocusCritEvalId = table.Column<int>(type: "int", nullable: false),
                    Number_Continuing = table.Column<int>(type: "int", nullable: false),
                    Number_New = table.Column<int>(type: "int", nullable: false),
                    CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GEC_Continuing = table.Column<int>(type: "int", nullable: true),
                    GEC_New = table.Column<int>(type: "int", nullable: true),
                    GEC_CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GAC_Continuing = table.Column<int>(type: "int", nullable: true),
                    GAC_New = table.Column<int>(type: "int", nullable: true),
                    GAC_CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GC_Continuing = table.Column<int>(type: "int", nullable: true),
                    GC_New = table.Column<int>(type: "int", nullable: true),
                    GC_CostPerLearner = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HDI = table.Column<int>(type: "int", nullable: false),
                    Female = table.Column<int>(type: "int", nullable: false),
                    Youth = table.Column<int>(type: "int", nullable: false),
                    Number_Disabled = table.Column<int>(type: "int", nullable: false),
                    Rural = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SqmrAppIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vision2025goal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Leviesuptodate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousWSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousParticipation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Project_Details_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Project_US",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    USId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Project_US", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Project_US_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    USId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Project_US_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider_Trading_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_legal_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_SDL_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chieta_Accredited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Public_Private = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Accreditation_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Accredit_Review_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accred_NO_Knowledge_Component = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accred_NO_Practical_Component = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETQA_ID = table.Column<int>(type: "int", nullable: false),
                    Physical_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Tel_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Cell_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider_SARS_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Tel_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_FAX_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Cell_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Web_Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Research_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Research_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Research_DocumentApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Research_DocumentApprovals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_StratRes_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    FocusCritEvalId = table.Column<int>(type: "int", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_StratRes_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_StratRes_Details_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    FocusCritEvalId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GEC_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vision2025goal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SqmrAppIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Leviesuptodate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousWSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousParticipation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_StratRes_Details_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_StratResObjectives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsId = table.Column<int>(type: "int", nullable: false),
                    Objectiv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Learners = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_StratResObjectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Tranche_Batch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Tranche_Batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Window_Params",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DG_Window_Id = table.Column<int>(type: "int", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "int", nullable: false),
                    FocusAreaId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    InterventionId = table.Column<int>(type: "int", nullable: true),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Window_Params", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Discretionary_Workplace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Workplace_Trading_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplacement_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDL_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETQA_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplacement_Approval_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Physical_Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Address_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_Tel_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_Fax_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_Cell_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Workplace_SARS_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Tel_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_FAX_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Cell_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Web_Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Discretionary_Workplace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DiscretionaryStratResObjectives_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailsId = table.Column<int>(type: "int", nullable: false),
                    Objectiv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Learners = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DiscretionaryStratResObjectives_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entityid = table.Column<int>(type: "int", nullable: false),
                    newfilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastmodifieddate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documenttype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ImportBatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipFileId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    LevyYear = table.Column<int>(type: "int", nullable: false),
                    SETA = table.Column<int>(type: "int", nullable: false),
                    BatchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MandatoryCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscretionaryCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RebateCollected = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ImportBatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LeviesRecon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipFileId = table.Column<int>(type: "int", nullable: false),
                    ZipFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LevyYear = table.Column<int>(type: "int", nullable: false),
                    GrantAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrantAmount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestAmount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LeviesRecon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LevyFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportInProgress = table.Column<bool>(type: "bit", nullable: false),
                    CommitInProgress = table.Column<bool>(type: "bit", nullable: false),
                    TransferInYN = table.Column<bool>(type: "bit", nullable: false),
                    TransferOutYN = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LevyFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LevyFileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevyFileId = table.Column<int>(type: "int", nullable: false),
                    LevyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SETA = table.Column<int>(type: "int", nullable: false),
                    SDLNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrantAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrantAmount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdminAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PenaltyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DhetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReturnsOutstanding = table.Column<int>(type: "int", nullable: false),
                    OutstandingDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LevyYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LevyFileDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LevyFileList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DHETZipFileID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImportInProgress = table.Column<bool>(type: "bit", nullable: false),
                    CommitInProgress = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LevyFileList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrantWindowId = table.Column<int>(type: "int", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    GrantStatusID = table.Column<int>(type: "int", nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaptureDte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmissionDte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserSubmitted = table.Column<int>(type: "int", nullable: true),
                    RSAId = table.Column<int>(type: "int", nullable: true),
                    RMId = table.Column<int>(type: "int", nullable: true),
                    SubmittedPrevious = table.Column<bool>(type: "bit", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    UserReviewed = table.Column<int>(type: "int", nullable: false),
                    DateReviewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Firstsubmission = table.Column<bool>(type: "bit", nullable: false),
                    ParentChild = table.Column<bool>(type: "bit", nullable: false),
                    Sublevies = table.Column<bool>(type: "bit", nullable: false),
                    Bankdetails = table.Column<bool>(type: "bit", nullable: false),
                    Employees = table.Column<bool>(type: "bit", nullable: false),
                    TrainingReceived = table.Column<bool>(type: "bit", nullable: false),
                    TrainingPlanned = table.Column<bool>(type: "bit", nullable: false),
                    Finance = table.Column<bool>(type: "bit", nullable: false),
                    EmployerRep = table.Column<bool>(type: "bit", nullable: false),
                    UnionSignatory = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Approval", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_BankDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    SDL_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PARENT_SDL_NO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ORGANISATION_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRADING_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ORGANISATION_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPROVAL_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_ACCOUNT_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_ACCOUNT_HOLDER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_ACCOUNT_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_BRANCH_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_BRANCH_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ORGANISATION_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHYSICAL_ADDRESS_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHYSICAL_ADDRESS_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHYSICAL_ADDRESS_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHYSICAL_ADDRESS_POST_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POSTAL_ADDRESS_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POSTAL_ADDRESS_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POSTAL_ADDRESS_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDF_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMPANY_SIZE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROVINCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CHIETA_REPORTING_REGION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MUNICIPALITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMBER_OF_EMPLOYEES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BBBEE_LEVEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GRANT_YEAR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_BankDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Biodata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    SA_Id_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birth_Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highest_Qualification_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employment_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation_Level_For_Equity_Reporting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisational_Structure_Filter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Post_Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Occupation_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Specialisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OFO_Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Biodata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Extensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReasonForRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Extensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Finance_Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    TOTAL_ACTUAL_PAYROLL_FOR_THE_YEAR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOTAL_ACTUAL_SKILLS_DEVELOPMENT_SPEND_FOR_THE_YEAR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OF_PAYROLL_SPENT_ON_SKILLS_DEVELOPMENT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOTAL_PROJECTED_PAYROLL_FOR_THE_YEAR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TOTAL_PROJECTED_SKILLS_DEVELOPMENT_BUDGET = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PROJECTED_PAYROLL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BENEFICIARIES_TRAIN = table.Column<int>(type: "int", nullable: true),
                    TOTAL_BENEFICIARIES_ACTUALLY_TRAINED_IN_THE = table.Column<int>(type: "int", nullable: true),
                    ACTUAL_TRAINING_VS_PLANNED_TRAINING = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CONFIRMATION_OF_EMPLOYEES_HIGHEST_QUALIFICATIONS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LEARNING_OPPORTUNITIES_UNEMPLOYED_PEOPLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LEARNING_AREAS_AND_OPPORTUNITIES_FOR_EMPLOYED_STAFF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESSING_EQUITY_AND_BBBEE_TARGETS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WORK_PLACEMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AREAS_FOR_RESEARCH_AND_INNOVATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LEARNERS_RETAINED = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PEOPLE_FOUND_EMPLOYMENT_DUE_TRAINING = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    General_Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Finance_Training", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Grant_DocumentApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    ApprovalTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Grant_DocumentApprovals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Grant_Window",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtensionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActiveYN = table.Column<bool>(type: "bit", nullable: false),
                    ExtenstionActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Grant_Window", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_GrantPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SDL_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrantYear = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    zipfileid = table.Column<int>(type: "int", nullable: false),
                    ChietaAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CHIETA_Code1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgName_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BANK_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank_Account_NUmber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Bank_Account_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDLCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_GrantPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_HTVFs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    OCCUPATION_OR_SPECIALISATION_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCCUPATION_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRIMARY_REASON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FURTHER_REASON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FURTHER_REASON_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROVINCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMBER_OF_VACANCIES = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_HTVFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_SkillGaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    OCCUPATION_OR_SPECIALISATION_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKILL_GAB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REASON_FOR_THE_SKILLS_GAP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDITIONAL_COMMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_SkillGaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Mandatory_Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    SA_Id_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification_Learning_Program_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details_Of_Learning_Program = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Study_Field_Or_Specialisation_Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Training_Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Achievement_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year_enrolled_or_completed = table.Column<int>(type: "int", nullable: false),
                    BiodataId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: true),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Mandatory_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_mobile_notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsPushSent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_mobile_notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Organisation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SDL_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SETA_Id = table.Column<short>(type: "smallint", nullable: false),
                    SIC_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Registration_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Trading_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Fax_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Contact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Contact_Email_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Contact_Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation_Contact_Cell_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMPANY_SIZE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUMBER_OF_EMPLOYEES = table.Column<int>(type: "int", nullable: false),
                    TYPE_OF_ENTITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CORE_BUSINESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PARENT_SDL_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BBBEE_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BBBEE_LEVEL = table.Column<int>(type: "int", nullable: true),
                    DATEBUSINESSCOMMENCED = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EXMPTIONCODE = table.Column<short>(type: "smallint", nullable: false),
                    CHAMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEO_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEO_Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEO_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEO_RaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEO_GenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senior_Rep_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senior_Rep_Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senior_Rep_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senior_Rep_RaceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senior_Rep_GenderId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Organisation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Organisation_Physical_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    organisationId = table.Column<int>(type: "int", nullable: false),
                    addressline1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressline2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Organisation_Physical_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Organisation_Postal_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    sameasphysical = table.Column<bool>(type: "bit", nullable: false),
                    organisationId = table.Column<int>(type: "int", nullable: false),
                    addressline1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressline2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Organisation_Postal_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Organisation_Sdf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationId = table.Column<int>(type: "int", nullable: false),
                    SdfId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<short>(type: "smallint", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Organisation_Sdf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Payment_Message_Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMessageId = table.Column<int>(type: "int", nullable: false),
                    ApplicationTrancheDetailsId = table.Column<int>(type: "int", nullable: false),
                    CtrlSum = table.Column<double>(type: "float", nullable: false),
                    PmtMtd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NbOfTxs = table.Column<int>(type: "int", nullable: false),
                    Cd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReqdExctnDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DbtrAcct_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DbtrAcct_TP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MmbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndtoEndId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstdAmt = table.Column<double>(type: "float", nullable: false),
                    ClrSysMmbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CdtrAgt_BrnchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cdtr_Nm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CdtrAcct_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CdtrAcct_Tp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ustrd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UsrUpd = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Payment_Message_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreDtTm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NbOfTxs = table.Column<int>(type: "int", nullable: false),
                    CtrlSum = table.Column<double>(type: "float", nullable: false),
                    Nm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PaymentMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<short>(type: "smallint", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middlenames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idtype = table.Column<short>(type: "smallint", nullable: false),
                    Saidnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Otheriddetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datecreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Person_Physical_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    personId = table.Column<int>(type: "int", nullable: false),
                    addressline1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressline2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Person_Physical_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Person_Postal_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    sameasphysical = table.Column<bool>(type: "bit", nullable: false),
                    personId = table.Column<int>(type: "int", nullable: false),
                    addressline1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressline2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Person_Postal_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PushNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PushNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SAQA_Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QUALIFICATION_ID = table.Column<int>(type: "int", nullable: false),
                    QUALIFICATION_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROVIDER_ID = table.Column<int>(type: "int", nullable: false),
                    PROVIDER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROVIDER_ETQA_ID = table.Column<int>(type: "int", nullable: false),
                    QUALIFICATION_TYPE_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QUALIFICATION_MINIMUM_CREDITS = table.Column<int>(type: "int", nullable: false),
                    NQF_LEVEL_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QUAL_REGISTRATION_START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QUAL_REGISTRATION_END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_DATE_FOR_ENROLMENT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_DATE_FOR_ACHIEVEMENT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ETQA_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SAQA_Qualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SAQA_Unitstandard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UNIT_STANDARD_ID = table.Column<double>(type: "float", nullable: false),
                    UNIT_STD_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UNIT_STD_NUMBER_OF_CREDITS = table.Column<int>(type: "int", nullable: false),
                    Amount1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SAQA_Unitstandard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Sdf_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    designation = table.Column<short>(type: "smallint", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    statusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statusUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Sdf_Details", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Sdf_File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sdfId = table.Column<int>(type: "int", nullable: false),
                    documentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    savedFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fileSize = table.Column<int>(type: "int", nullable: false),
                    fileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Sdf_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Student_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addressline1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressline2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datecreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    DteUpd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsrUpd = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Student_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tranche_Approvals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    TrancheId = table.Column<int>(type: "int", nullable: false),
                    ApprovalLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approval_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tranche_Approvals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Action",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionTypeId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_ActionTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_ActionTarget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_ActionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_ActionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityTypeId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_ActivityTarget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_ActivityTarget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_GroupMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_GroupMember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Process",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Process", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_ProcessAdmins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_ProcessAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStateId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Request", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_RequestAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    DateActioned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserActioned = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_RequestAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_RequestData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_RequestData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_RequestFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MIMEType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filelocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_RequestFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_RequestNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_RequestNote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_RequestStakeholder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_RequestStakeholder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateTypeId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_StateActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_StateActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_StateType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_StateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Target",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Target", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Timer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimerDurationId = table.Column<int>(type: "int", nullable: false),
                    DurationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    TimerResultId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Timer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_TimerDuration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_TimerDuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_TimerResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimerResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_TimerResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_Transition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    CurrentStateId = table.Column<int>(type: "int", nullable: false),
                    NextStateId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_Transition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_TransitionAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_TransitionAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_TransitionActivity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_TransitionActivity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wf_TransitionTimer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionId = table.Column<int>(type: "int", nullable: false),
                    TimeDurationId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wf_TransitionTimer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ikp_Home_Language_Code");

            migrationBuilder.DropTable(
                name: "Ikp_Nationality_Code");

            migrationBuilder.DropTable(
                name: "Ikp_OFO");

            migrationBuilder.DropTable(
                name: "Ikp_Province_Code");

            migrationBuilder.DropTable(
                name: "Ikp_SETA_");

            migrationBuilder.DropTable(
                name: "Ikp_SIC_Code");

            migrationBuilder.DropTable(
                name: "LEVY_PAYMENTSs");

            migrationBuilder.DropTable(
                name: "lkp_AdminCrit");

            migrationBuilder.DropTable(
                name: "lkp_Alternate_Id_Type");

            migrationBuilder.DropTable(
                name: "lkp_Bank");

            migrationBuilder.DropTable(
                name: "lkp_Bank_Account_Type");

            migrationBuilder.DropTable(
                name: "lkp_BBBEE_Level");

            migrationBuilder.DropTable(
                name: "lkp_BBBStatuses");

            migrationBuilder.DropTable(
                name: "lkp_Chambers");

            migrationBuilder.DropTable(
                name: "lkp_Citizen_Resident_Status");

            migrationBuilder.DropTable(
                name: "lkp_CompanyCompliance");

            migrationBuilder.DropTable(
                name: "lkp_CompanySizes");

            migrationBuilder.DropTable(
                name: "lkp_CompanyType");

            migrationBuilder.DropTable(
                name: "lkp_Counters");

            migrationBuilder.DropTable(
                name: "lkp_Dg_Payment_Tranche_Type");

            migrationBuilder.DropTable(
                name: "lkp_DiscLearnerType");

            migrationBuilder.DropTable(
                name: "lkp_Discretionary_Lesedi_Qualification");

            migrationBuilder.DropTable(
                name: "lkp_Discretionary_ProgrammeDeliverables");

            migrationBuilder.DropTable(
                name: "lkp_Discretionary_Status");

            migrationBuilder.DropTable(
                name: "lkp_Discretionary_Universtity_College");

            migrationBuilder.DropTable(
                name: "lkp_EmploymentStatus");

            migrationBuilder.DropTable(
                name: "lkp_Equity_Code");

            migrationBuilder.DropTable(
                name: "lkp_EvaluationMethod");

            migrationBuilder.DropTable(
                name: "lkp_Fintegrate_Payment_Status");

            migrationBuilder.DropTable(
                name: "lkp_FocusArea");

            migrationBuilder.DropTable(
                name: "lkp_FocusCritEval");

            migrationBuilder.DropTable(
                name: "lkp_Gender_Code");

            migrationBuilder.DropTable(
                name: "lkp_Grant_Approval_Status");

            migrationBuilder.DropTable(
                name: "lkp_Grant_Approval_Types");

            migrationBuilder.DropTable(
                name: "lkp_Grant_Deliverable_Schedule");

            migrationBuilder.DropTable(
                name: "lkp_HistoricalPerformance");

            migrationBuilder.DropTable(
                name: "lkp_Learning_Programme");

            migrationBuilder.DropTable(
                name: "lkp_Lesedi_Status");

            migrationBuilder.DropTable(
                name: "lkp_MainPlace");

            migrationBuilder.DropTable(
                name: "lkp_Mand_Pivotal_Programmes");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Approval_Status");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_ExtensionStatus");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Grant_Achievement_Status");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Grant_Qualification_Type");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Grants_Gap_Reason");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Grants_Scarce_Reason");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Grants_Target_Beneficiary");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Programmes");

            migrationBuilder.DropTable(
                name: "lkp_Mandatory_Status");

            migrationBuilder.DropTable(
                name: "lkp_Mandatoty_Grants_Impact");

            migrationBuilder.DropTable(
                name: "lkp_Occupation_Level");

            migrationBuilder.DropTable(
                name: "lkp_OFO_Specialization");

            migrationBuilder.DropTable(
                name: "lkp_Payment_Tranches");

            migrationBuilder.DropTable(
                name: "lkp_Person_Title");

            migrationBuilder.DropTable(
                name: "lkp_PostalCodeMapping");

            migrationBuilder.DropTable(
                name: "lkp_PostalCodeProvince");

            migrationBuilder.DropTable(
                name: "lkp_PostCodeMapping");

            migrationBuilder.DropTable(
                name: "lkp_ProgrammeDeliverables");

            migrationBuilder.DropTable(
                name: "lkp_Project_Type");

            migrationBuilder.DropTable(
                name: "lkp_Region_Managers");

            migrationBuilder.DropTable(
                name: "lkp_Region_RSA");

            migrationBuilder.DropTable(
                name: "lkp_RegionProvince");

            migrationBuilder.DropTable(
                name: "lkp_Regions");

            migrationBuilder.DropTable(
                name: "lkp_sdf_designation");

            migrationBuilder.DropTable(
                name: "lkp_Specialist_Project");

            migrationBuilder.DropTable(
                name: "lkp_SqmrApp_Indicators");

            migrationBuilder.DropTable(
                name: "lkp_SubPlace");

            migrationBuilder.DropTable(
                name: "lkp_Tranche_Approval_Status");

            migrationBuilder.DropTable(
                name: "lkp_Vision2025Goals");

            migrationBuilder.DropTable(
                name: "MandatoryGrantPayment");

            migrationBuilder.DropTable(
                name: "ProjectLearner");

            migrationBuilder.DropTable(
                name: "ProjectNotifications");

            migrationBuilder.DropTable(
                name: "tbl_ApplicationBatch");

            migrationBuilder.DropTable(
                name: "tbl_ApplicationTranche");

            migrationBuilder.DropTable(
                name: "tbl_ApplicationTrancheDetails");

            migrationBuilder.DropTable(
                name: "tbl_BankDetails");

            migrationBuilder.DropTable(
                name: "tbl_BankingList");

            migrationBuilder.DropTable(
                name: "tbl_Bursary_DocumentApprovals");

            migrationBuilder.DropTable(
                name: "tbl_Discrationary_Tranche_Batch_Requests");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Bursary_Applications");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Bursary_Approvals");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Details_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GAC_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GACR_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GC_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GCR_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GEC_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_GECR_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Grant_Approvals");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Grant_DocumentApprovals");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Grant_Window");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Learner_Schedule");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_LearnerDetails");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Lesedi");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Lesedi_Details");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Project");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Project_Details");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Project_Details_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Project_US");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Project_US_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Provider");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Research_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Research_DocumentApprovals");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_StratRes_Details");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_StratRes_Details_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_StratResObjectives");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Tranche_Batch");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Window_Params");

            migrationBuilder.DropTable(
                name: "tbl_Discretionary_Workplace");

            migrationBuilder.DropTable(
                name: "tbl_DiscretionaryStratResObjectives_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Documents");

            migrationBuilder.DropTable(
                name: "tbl_ImportBatch");

            migrationBuilder.DropTable(
                name: "tbl_LeviesRecon");

            migrationBuilder.DropTable(
                name: "tbl_LevyFile");

            migrationBuilder.DropTable(
                name: "tbl_LevyFileDetails");

            migrationBuilder.DropTable(
                name: "tbl_LevyFileList");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Application");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Approval");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_BankDetails");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Biodata");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Extensions");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Finance_Training");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Grant_DocumentApprovals");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Grant_Window");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_GrantPayments");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_HTVFs");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_SkillGaps");

            migrationBuilder.DropTable(
                name: "tbl_Mandatory_Trainings");

            migrationBuilder.DropTable(
                name: "tbl_mobile_notifications");

            migrationBuilder.DropTable(
                name: "tbl_Organisation");

            migrationBuilder.DropTable(
                name: "tbl_Organisation_Physical_Address");

            migrationBuilder.DropTable(
                name: "tbl_Organisation_Postal_Address");

            migrationBuilder.DropTable(
                name: "tbl_Organisation_Sdf");

            migrationBuilder.DropTable(
                name: "tbl_Payment_Message_Transactions");

            migrationBuilder.DropTable(
                name: "tbl_PaymentMessages");

            migrationBuilder.DropTable(
                name: "tbl_Person");

            migrationBuilder.DropTable(
                name: "tbl_Person_Physical_Address");

            migrationBuilder.DropTable(
                name: "tbl_Person_Postal_Address");

            migrationBuilder.DropTable(
                name: "tbl_PushNotifications");

            migrationBuilder.DropTable(
                name: "tbl_SAQA_Qualifications");

            migrationBuilder.DropTable(
                name: "tbl_SAQA_Unitstandard");

            migrationBuilder.DropTable(
                name: "tbl_Sdf_Details");

            migrationBuilder.DropTable(
                name: "tbl_Sdf_File");

            migrationBuilder.DropTable(
                name: "tbl_Student_Address");

            migrationBuilder.DropTable(
                name: "tbl_Tranche_Approvals");

            migrationBuilder.DropTable(
                name: "wf_Action");

            migrationBuilder.DropTable(
                name: "wf_ActionTarget");

            migrationBuilder.DropTable(
                name: "wf_ActionType");

            migrationBuilder.DropTable(
                name: "wf_Activity");

            migrationBuilder.DropTable(
                name: "wf_ActivityTarget");

            migrationBuilder.DropTable(
                name: "wf_ActivityType");

            migrationBuilder.DropTable(
                name: "wf_Group");

            migrationBuilder.DropTable(
                name: "wf_GroupMember");

            migrationBuilder.DropTable(
                name: "wf_Process");

            migrationBuilder.DropTable(
                name: "wf_ProcessAdmins");

            migrationBuilder.DropTable(
                name: "wf_Request");

            migrationBuilder.DropTable(
                name: "wf_RequestAction");

            migrationBuilder.DropTable(
                name: "wf_RequestData");

            migrationBuilder.DropTable(
                name: "wf_RequestFile");

            migrationBuilder.DropTable(
                name: "wf_RequestNote");

            migrationBuilder.DropTable(
                name: "wf_RequestStakeholder");

            migrationBuilder.DropTable(
                name: "wf_State");

            migrationBuilder.DropTable(
                name: "wf_StateActivity");

            migrationBuilder.DropTable(
                name: "wf_StateType");

            migrationBuilder.DropTable(
                name: "wf_Target");

            migrationBuilder.DropTable(
                name: "wf_Timer");

            migrationBuilder.DropTable(
                name: "wf_TimerDuration");

            migrationBuilder.DropTable(
                name: "wf_TimerResult");

            migrationBuilder.DropTable(
                name: "wf_Transition");

            migrationBuilder.DropTable(
                name: "wf_TransitionAction");

            migrationBuilder.DropTable(
                name: "wf_TransitionActivity");

            migrationBuilder.DropTable(
                name: "wf_TransitionTimer");
        }
    }
}
