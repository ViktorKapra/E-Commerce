#dotnet ef database update

cd .\E-Commerce-Server
dotnet ef database update

# Check if the update was successful
if ($LASTEXITCODE -eq 0) {
    Write-Host "Database update completed successfully."
} else {
    Write-Host "Database update failed. Check the error message above." -ForegroundColor Red
}