namespace ma.metl.sirh.Model.sirhContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Commission", "TypeAvancement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Commission", "TypeAvancement");
        }
    }
}
