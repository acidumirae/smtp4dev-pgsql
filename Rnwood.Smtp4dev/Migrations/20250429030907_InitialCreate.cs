using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rnwood.Smtp4dev.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "imapstate",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lastuid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_imapstate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mailboxes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mailbox", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    log = table.Column<string>(type: "text", nullable: true),
                    numberofmessages = table.Column<int>(type: "integer", nullable: false),
                    clientaddress = table.Column<string>(type: "text", nullable: true),
                    clientname = table.Column<string>(type: "text", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    startdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    sessionerror = table.Column<string>(type: "text", nullable: true),
                    sessionerrortype = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sessions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from = table.Column<string>(type: "text", nullable: true),
                    to = table.Column<string>(type: "text", nullable: true),
                    receiveddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: true),
                    data = table.Column<byte[]>(type: "bytea", nullable: true),
                    mimeparseerror = table.Column<string>(type: "text", nullable: true),
                    attachmentcount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    isunread = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    relayerror = table.Column<string>(type: "text", nullable: true),
                    imapuid = table.Column<int>(type: "integer", nullable: false),
                    secureconnection = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    sessionencoding = table.Column<string>(type: "text", nullable: true),
                    eightbittransport = table.Column<bool>(type: "boolean", nullable: true),
                    mailboxid = table.Column<Guid>(type: "uuid", nullable: true),
                    deliveredto = table.Column<string>(type: "text", nullable: true),
                    sessionid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("messages_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_mailboxes_mailboxid",
                        column: x => x.mailboxid,
                        principalTable: "mailboxes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messages_sessions_sessionid",
                        column: x => x.sessionid,
                        principalTable: "sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "messagerelays",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    messageid = table.Column<Guid>(type: "uuid", nullable: false),
                    to = table.Column<string>(type: "text", nullable: true),
                    senddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messagerelays", x => x.id);
                    table.ForeignKey(
                        name: "fk_messagerelays_messages_messageid",
                        column: x => x.messageid,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_messagerelays_messageid",
                table: "messagerelays",
                column: "messageid");

            migrationBuilder.CreateIndex(
                name: "ix_messages_mailboxid",
                table: "messages",
                column: "mailboxid");

            migrationBuilder.CreateIndex(
                name: "IX_messages_sessionid",
                table: "messages",
                column: "sessionid");

            migrationBuilder.CreateIndex(
                name: "pk_messages_id",
                table: "messages",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imapstate");

            migrationBuilder.DropTable(
                name: "messagerelays");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "mailboxes");

            migrationBuilder.DropTable(
                name: "sessions");
        }
    }
}
