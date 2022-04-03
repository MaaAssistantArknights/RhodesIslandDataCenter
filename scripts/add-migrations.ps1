if (!$args[0] || $args[0] -eq "") {
  Write-Error "No migration name provided"
  Write-Error "Usage: pwsh ./add-migration.ps1 <migration_name>"
  Break Script
}

dotnet ef migrations add $args[0] --project ../src/RIDC.Database.Migrations.MariaDb
dotnet ef migrations add $args[0] --project ../src/RIDC.Database.Migrations.MySql
dotnet ef migrations add $args[0] --project ../src/RIDC.Database.Migrations.MySqlClassic
dotnet ef migrations add $args[0] --project ../src/RIDC.Database.Migrations.PgSql
dotnet ef migrations add $args[0] --project ../src/RIDC.Database.Migrations.Sqlite
