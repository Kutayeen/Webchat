using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Webchat.Model;

namespace Webchat.Dal
{
    public class WebchatContext : DbContext
    {
        // Your context has been configured to use a 'WebchatContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Webchat.Dll.WebchatContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WebchatContext' 
        // connection string in the application configuration file.
        public WebchatContext()
            : base("name=WebchatContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<WebchatContext>(new DropCreateDatabaseIfModelChanges<WebchatContext>());

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserMail)
                .IsUnique();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<ChatUser> ChatUser { get; set; }
    }
     
}