namespace HSStats.Migrations
{
    using HSStats.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HSStats.Models.HSStatsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HSStats.Models.HSStatsDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Matches.AddOrUpdate( i => i.MatchID,
                new Match
                {
                    Mode = Modes.Ranked,
                    MyHero = Heroes.Hunter,
                    OpponentHero = Heroes.Mage,
                    Result = Results.Win,
                    Turn = Turns.GoFirst,
                    MatchTime = DateTime.Parse("2015-03-10T07:34:42")
                },
                new Match
                {
                    Mode = Modes.Ranked,
                    MyHero = Heroes.Hunter,
                    OpponentHero = Heroes.Hunter,
                    Result = Results.Defeat,
                    Turn = Turns.GoSecond,
                    MatchTime = DateTime.Parse("2015-03-10T07:41:42")
                },
                new Match
                {
                    Mode = Modes.Ranked,
                    MyHero = Heroes.Hunter,
                    OpponentHero = Heroes.Hunter,
                    Result = Results.Defeat,
                    Turn = Turns.GoFirst,
                    MatchTime = DateTime.Parse("2015-03-10T07:50:42")
                },
                new Match
                {
                    Mode = Modes.Ranked,
                    MyHero = Heroes.Hunter,
                    OpponentHero = Heroes.Priest,
                    Result = Results.Defeat,
                    Turn = Turns.GoSecond,
                    MatchTime = DateTime.Parse("2015-03-10T07:58:42")
                },
                new Match
                {
                    Mode = Modes.Ranked,
                    MyHero = Heroes.Hunter,
                    OpponentHero = Heroes.Mage,
                    Result = Results.Win,
                    Turn = Turns.GoFirst,
                    MatchTime = DateTime.Parse("2015-03-10T08:05:42")
                }
            );
        }
    }
}
