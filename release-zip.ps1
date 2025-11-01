# 编译 PvzReA11y 项目并打包

Write-Host "++开始编译 PvzReA11y 项目..." -ForegroundColor Green
dotnet build "PvzReA11y\PvzReA11y.csproj" --configuration Release
Write-Host "编译完成!" -ForegroundColor Green

# 生成时间戳
$timestamp = Get-Date -Format "yyyyMMdd-HHmm"
$zipFileName = "PvzReA11yMod-重植版无障碍MOD-v$timestamp.zip"

Write-Host "++开始打包 PvzReA11yMod 文件夹..."
# 打包 PvzReA11yMod 文件夹内容（不包含文件夹本身）
Push-Location "PvzReA11yMod"
Compress-Archive -Path "*" -DestinationPath "..\$zipFileName" -Force
Pop-Location
Write-Host "打包完成! 文件保存为: \"$zipFileName\"" -ForegroundColor Green
