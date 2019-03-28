# Database First Entity Framework Dotnet Core 2x simple guide

## 1. Creating the solution projects for this guide
Open a new Terminal window and then type the bellow commands or run the powershell script "1.SetupGuide.ps1" which it will execute the bellow commands:

Create new empty solution "EFDatabaseFirst.JecaestevezApp"
 > dotnet new sln -n EFDatabaseFirst.JecaestevezApp

Create empty console application "ConsoleApp.Jecaestevez"
 > dotnet new console -n ConsoleApp.Jecaestevez -o ConsoleApp

Create empty library application "DAL.JecaestevezApp"
 > dotnet new classlib -n DAL.JecaestevezApp -o DAL

 Add the created console application to the solution
  > dotnet sln EFDatabaseFirst.JecaestevezApp.sln add ConsoleApp/ConsoleApp.Jecaestevez.csproj  

Add the console application to the solution
  > dotnet sln EFDatabaseFirst.JecaestevezApp.sln add DAL/DAL.JecaestevezApp.csproj  

Add a refrence from ConsoleApp to DAL.JecaestevezApp
  >dotnet add ConsoleApp/ConsoleApp.Jecaestevez.csproj reference DAL/DAL.JecaestevezApp.csproj

Build the solution
 > dotnet build

# 2 Add Entity Framework Core packages to 
Add the necesary packages for this example executing the powershell script "2.AddNugetPackages.ps1"

You can also add manual the package opening  terminal and navigate to DatabaseFirst\DAL add to "DAL.JecaestevezApp.csproj"  EntityFrameworkCore.SqlServer and EntityFrameworkCore.Tools

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.SqlServer

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.Tools 

> dotnet add .\DAL\DAL.JecaestevezApp.csproj package Microsoft.EntityFrameworkCore.Design 

# 3 Add a simple class to be used in a new  DBContext
Add DBContext
```
    public class EfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO Extract connection string to a secret
            optionsBuilder.UseSqlServer(@"Server=.\;Database=EFDatabaseFirstDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Item> Items { get; set; }
    }
```

# 4 Create the first migration
Open powershell console and execute the powershell script  "4.CrateMigration.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef  migrations add CreateDatabase --startup-project ..\ConsoleApp

It's possible do the same step using the Package Manager Console in Visual Studio, selecting the DAL.JecaestevezApp.csproj and execute 
> PM > add-migration CreateDatabase

It will be create a folder "Migrations" and the following files:
* CreateDatabase.cs
* CreateDatabase.Designer.cs
* EfDbContextModelSnapshot.cs

Modify the file "CreateDatabase.cs" to add the new table like this:
```
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE [dbo].[Items](
	                [id] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [nvarchar](max) NULL,
	                [Description] [nvarchar](max) NULL,
	                [Expiration] [datetime2](7) NOT NULL,
                 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
                (
	                [id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TABLE [dbo].[Items]");
        }
    }
```
# 5 Update Database
Execute the powershell script  "5.UpdateDatabase.ps1" which will execute the bellow command in "\DAL\DAL.Jecaestevez.csproj"
> dotnet ef database update --startup-project ..\ConsoleApp

Using Package Manager Console select the DAL.JecaestevezApp.csproj and execute 
> PM> update-database –verbose
