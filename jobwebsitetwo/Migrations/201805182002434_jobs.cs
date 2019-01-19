namespace jobwebsitetwo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.jobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        jobtitle = c.String(nullable: false),
                        jobcontent = c.String(),
                        jobimage = c.String(),
                        categoryid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.categories", t => t.categoryid, cascadeDelete: true)
                .Index(t => t.categoryid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "categoryid", "dbo.categories");
            DropIndex("dbo.jobs", new[] { "categoryid" });
            DropTable("dbo.jobs");
        }
    }
}
