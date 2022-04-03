if [ -z "$1" ]; then
    echo "No migration name provided"
    echo "Usage: ./add-migration.sh <migration_name>"
    exit 1
fi

dotnet ef migrations add "$1" --project ../src/RIDC.Database.Migrations.MariaDb
dotnet ef migrations add "$1" --project ../src/RIDC.Database.Migrations.MySql
dotnet ef migrations add "$1" --project ../src/RIDC.Database.Migrations.MySqlClassic
dotnet ef migrations add "$1" --project ../src/RIDC.Database.Migrations.PgSql
dotnet ef migrations add "$1" --project ../src/RIDC.Database.Migrations.Sqlite
