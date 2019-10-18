namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBirthDates : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthdate = '02/02/1981' WHERE Id = 1");
            Sql("UPDATE Customers SET Birthdate = '09/08/1989' WHERE Id = 2");
        }

        public override void Down()
        {
        }
    }
}
