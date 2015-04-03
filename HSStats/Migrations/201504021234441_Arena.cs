namespace HSStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Arena : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arenas",
                c => new
                    {
                        ArenaID = c.Int(nullable: false, identity: true),
                        Hero = c.Byte(nullable: false),
                        Wins = c.Int(nullable: false),
                        Defeats = c.Int(nullable: false),
                        Gold = c.Int(nullable: false),
                        Dust = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ArenaID);
            
            AddColumn("dbo.Matches", "ArenaID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "ArenaID");
            DropTable("dbo.Arenas");
        }
    }
}
