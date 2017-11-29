namespace OAuthServer.Data.Migrations
{
    using OAuthServer.Data.DataContexts;
    using OAuthServer.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OAuthServer.Data.DataContexts.OAuthServerDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OAuthServerDataContext context)
        {
            context.Users.AddOrUpdate(new User("adilson","abc123"));
            context.Users.AddOrUpdate(new User("dourado", "abc123"));
        }
    }
}
