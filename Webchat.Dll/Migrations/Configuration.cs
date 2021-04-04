namespace Webchat.Dll.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Webchat.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<Webchat.Dal.WebchatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Webchat.Dal.WebchatContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.User.AddOrUpdate(new User { IsAdmin = true, UserCreated = DateTime.Now, UserMail = "kutayeen@gmail.com", UserName = "kutayeen", UserPassword = "4AnFx9ivYPOcVOM/Jqbko46AZHY=", UserPhoto = null, UserId=0 });
            context.User.AddOrUpdate(new User { IsAdmin = false, UserCreated = DateTime.Now, UserMail = "kutayeen2@gmail.com", UserName = "kutayeen2", UserPassword = "4AnFx9ivYPOcVOM/Jqbko46AZHY=", UserPhoto = null, UserId = 0 });
            context.User.AddOrUpdate(new User { IsAdmin = false, UserCreated = DateTime.Now, UserMail = "kutayeen3@gmail.com", UserName = "kutayeen3", UserPassword = "4AnFx9ivYPOcVOM/Jqbko46AZHY=", UserPhoto = null, UserId = 0 });
            context.User.AddOrUpdate(new User { IsAdmin = false, UserCreated = DateTime.Now, UserMail = "kutayeen4@gmail.com", UserName = "kutayeen4", UserPassword = "4AnFx9ivYPOcVOM/Jqbko46AZHY=", UserPhoto = null, UserId = 0 });

            context.SaveChanges();
        }
    }
}
