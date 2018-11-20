namespace Task1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Day = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
        }
    }
}
