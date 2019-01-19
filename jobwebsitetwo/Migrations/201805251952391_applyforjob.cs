namespace jobwebsitetwo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applyforjob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "userid", c => c.String(maxLength: 128));
            CreateIndex("dbo.jobs", "userid");
            AddForeignKey("dbo.jobs", "userid", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.jobs", new[] { "userid" });
            DropColumn("dbo.jobs", "userid");
        }
    }
}
