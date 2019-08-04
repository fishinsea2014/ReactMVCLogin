namespace ReactMVCLogin.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Login : DbContext
    {
        // Your context has been configured to use a 'Login' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ReactMVCLogin.Models.Login' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Login' 
        // connection string in the application configuration file.
        public Login()
            : base("name=Login.xsd")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<User> UserInfo { get; set; }
        public DbSet<Team> TeamInfo { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}