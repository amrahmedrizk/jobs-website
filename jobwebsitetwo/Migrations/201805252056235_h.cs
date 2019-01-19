namespace jobwebsitetwo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.applyforjobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        massage = c.String(),
                        applaydate = c.DateTime(nullable: false),
                        jobsid = c.Int(nullable: false),
                        userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.jobs", t => t.jobsid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.jobsid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.applyforjobs", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.applyforjobs", "jobsid", "dbo.jobs");
            DropIndex("dbo.applyforjobs", new[] { "userid" });
            DropIndex("dbo.applyforjobs", new[] { "jobsid" });
            DropTable("dbo.applyforjobs");
        }
    }
}
