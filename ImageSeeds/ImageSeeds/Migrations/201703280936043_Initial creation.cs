using System.Data.Entity.Migrations;

namespace ImageSeeds.Migrations
{
    public partial class Initialcreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                {
                    Id = c.Long(false, true),
                    UserId = c.Long(false),
                    Name = c.String(false),
                    ImagePath = c.String()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Photos",
                c => new
                {
                    Id = c.Long(false, true),
                    AlbumId = c.Long(false),
                    Name = c.String(),
                    ImagePath = c.String(),
                    ThumbnailPath = c.String()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId, true)
                .Index(t => t.AlbumId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Long(false, true),
                    FirstName = c.String(),
                    LastName = c.String(),
                    UserName = c.String(false),
                    Password = c.String(false),
                    Email = c.String()
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Albums", "UserId", "dbo.Users");
            DropForeignKey("dbo.Photos", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Photos", new[] {"AlbumId"});
            DropIndex("dbo.Albums", new[] {"UserId"});
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
            DropTable("dbo.Albums");
        }
    }
}