# Define the path to your Rage MP server executable
$rageMPPath = "C:\Users\Administrator\Desktop\LuxeRp N\ragemp-server.exe"

# Function to start the Rage MP console
function Start-RageMP {
    Write-Host "Starting Rage MP server..."
    Start-Process $rageMPPath
}

# Loop to check if the Rage MP server is running
while ($true) {
    $process = Get-Process -Name "ragemp-server" -ErrorAction SilentlyContinue  # Check if the process is running
    if (-not $process) {
        Start-RageMP  # Start Rage MP if it's not running
    }
    Start-Sleep -Seconds 2  # Wait for 2 seconds before checking again
}
