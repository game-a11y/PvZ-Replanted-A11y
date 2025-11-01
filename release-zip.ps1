# 编译 PvzReA11y 并打包

Write-Host "开始编译 PvzReA11y 项目..." -ForegroundColor Green

dotnet build "PvzReA11y\PvzReA11y.csproj" --configuration Release

Write-Host "编译完成!" -ForegroundColor Green


