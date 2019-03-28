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
            optionsBuilder.UseSqlServer(@"Server=.\;Database=EFCodeFirstDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Item> Items { get; set; }
    }
```