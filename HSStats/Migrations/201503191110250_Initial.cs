namespace HSStats.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        Mode = c.Byte(nullable: false),
                        MyHero = c.Byte(nullable: false),
                        OpponentHero = c.Byte(nullable: false),
                        Turn = c.Int(nullable: false),
                        Result = c.Byte(nullable: false),
                        MatchTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Matches");
        }
    }
}
