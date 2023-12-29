using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_Daber_5edma_version_sans_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_TCandidat_CandidatId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_TJobOffer_JobOfferId",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_TJobOffer_TCompany_CompanyId",
                table: "TJobOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TJobOffer",
                table: "TJobOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TCompany",
                table: "TCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TCandidat",
                table: "TCandidat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.RenameTable(
                name: "TJobOffer",
                newName: "AJobOffer");

            migrationBuilder.RenameTable(
                name: "TCompany",
                newName: "ACompany");

            migrationBuilder.RenameTable(
                name: "TCandidat",
                newName: "ACandidat");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "AJobApplication");

            migrationBuilder.RenameIndex(
                name: "IX_TJobOffer_CompanyId",
                table: "AJobOffer",
                newName: "IX_AJobOffer_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_JobOfferId",
                table: "AJobApplication",
                newName: "IX_AJobApplication_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplication_CandidatId",
                table: "AJobApplication",
                newName: "IX_AJobApplication_CandidatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AJobOffer",
                table: "AJobOffer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ACompany",
                table: "ACompany",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ACandidat",
                table: "ACandidat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AJobApplication",
                table: "AJobApplication",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AJobApplication_ACandidat_CandidatId",
                table: "AJobApplication",
                column: "CandidatId",
                principalTable: "ACandidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AJobApplication_AJobOffer_JobOfferId",
                table: "AJobApplication",
                column: "JobOfferId",
                principalTable: "AJobOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AJobOffer_ACompany_CompanyId",
                table: "AJobOffer",
                column: "CompanyId",
                principalTable: "ACompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AJobApplication_ACandidat_CandidatId",
                table: "AJobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_AJobApplication_AJobOffer_JobOfferId",
                table: "AJobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_AJobOffer_ACompany_CompanyId",
                table: "AJobOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AJobOffer",
                table: "AJobOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AJobApplication",
                table: "AJobApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ACompany",
                table: "ACompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ACandidat",
                table: "ACandidat");

            migrationBuilder.RenameTable(
                name: "AJobOffer",
                newName: "TJobOffer");

            migrationBuilder.RenameTable(
                name: "AJobApplication",
                newName: "JobApplication");

            migrationBuilder.RenameTable(
                name: "ACompany",
                newName: "TCompany");

            migrationBuilder.RenameTable(
                name: "ACandidat",
                newName: "TCandidat");

            migrationBuilder.RenameIndex(
                name: "IX_AJobOffer_CompanyId",
                table: "TJobOffer",
                newName: "IX_TJobOffer_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_AJobApplication_JobOfferId",
                table: "JobApplication",
                newName: "IX_JobApplication_JobOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_AJobApplication_CandidatId",
                table: "JobApplication",
                newName: "IX_JobApplication_CandidatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TJobOffer",
                table: "TJobOffer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TCompany",
                table: "TCompany",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TCandidat",
                table: "TCandidat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_TCandidat_CandidatId",
                table: "JobApplication",
                column: "CandidatId",
                principalTable: "TCandidat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_TJobOffer_JobOfferId",
                table: "JobApplication",
                column: "JobOfferId",
                principalTable: "TJobOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TJobOffer_TCompany_CompanyId",
                table: "TJobOffer",
                column: "CompanyId",
                principalTable: "TCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
