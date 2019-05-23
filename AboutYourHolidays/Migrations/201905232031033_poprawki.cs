namespace AboutYourHolidays.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawki : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Country", c => c.String(maxLength: 100));
            AddColumn("dbo.Post", "City", c => c.String());
            AddColumn("dbo.Post", "ImageUrl", c => c.String());
            AddColumn("dbo.Rating", "RateValue4", c => c.Int(nullable: false));
            AddColumn("dbo.Rating", "RateValue5", c => c.Int(nullable: false));
            DropColumn("dbo.Post", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post", "Address", c => c.String(maxLength: 100));
            DropColumn("dbo.Rating", "RateValue5");
            DropColumn("dbo.Rating", "RateValue4");
            DropColumn("dbo.Post", "ImageUrl");
            DropColumn("dbo.Post", "City");
            DropColumn("dbo.Post", "Country");
        }
    }
}
