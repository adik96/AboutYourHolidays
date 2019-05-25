namespace AboutYourHolidays.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zmiany1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Post", "LastUpdatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "LastUpdatedOn", c => c.DateTime(nullable: false));
        }
    }
}
