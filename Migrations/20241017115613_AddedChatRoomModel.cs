using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatRoomModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.Id);
                });
            
            migrationBuilder.AddColumn<int>(
                name: "ChatRoomId",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatRoomId",
                table: "Message",
                column: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_ChatRoom_ChatRoomId",
                table: "Message",
                column: "ChatRoomId",
                principalTable: "ChatRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Create default ChatRoom
            migrationBuilder.Sql("INSERT INTO ChatRoom (Name) VALUES ('General')");

            // Get the Id of the default ChatRoom
            migrationBuilder.Sql(@"
            DECLARE @ChatRoomId INT;
            SELECT @ChatRoomId = Id FROM ChatRoom WHERE Name = 'General';

            UPDATE Message SET ChatRoomId = @ChatRoomId;
        ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_ChatRoom_ChatRoomId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "ChatRoom");

            migrationBuilder.DropIndex(
                name: "IX_Message_ChatRoomId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ChatRoomId",
                table: "Message");
        }
    }
}
