using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncInn.Migrations
{
    /// <inheritdoc />
    public partial class NPRoomsAmeneitesAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenity_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Room_RoomID",
                table: "RoomAmenities",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Amenity_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Room_RoomID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities");
        }
    }
}
