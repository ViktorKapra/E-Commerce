#dotnet ef database update

cd .\E-Commerce-Server

dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=DESKTOP-0D68JS9\SQLEXPRESS03;Database=aspnet-E_Commerce_Server-826fe2c7-9966-42c2-b85f-e6d46c5b3c13;Trusted_Connection=True;TrustServerCertificate=True"

dotnet ef database update

# Check if the update was successful
if ($LASTEXITCODE -eq 0) {
    Write-Host "Database update completed successfully."
} else {
    Write-Host "Database update failed. Check the error message above." -ForegroundColor Red
}