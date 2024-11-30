namespace Code2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Age = c.Int(nullable: false),
                        DateOfJoining = c.DateTime(nullable: false),
                        Picture = c.String(),
                        EmployeeActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ProjectAssignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectAssignments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectAssignments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.ProjectAssignments", new[] { "EmployeeId" });
            DropIndex("dbo.ProjectAssignments", new[] { "ProjectId" });
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectAssignments");
            DropTable("dbo.Employees");
        }
    }
}
