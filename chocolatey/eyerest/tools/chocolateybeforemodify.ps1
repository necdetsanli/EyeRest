$processName = 'EyeRest'

$procs = Get-Process -Name $processName -ErrorAction SilentlyContinue
if ($procs) {
    Write-Host "EyeRest is running, attempting to close it gracefully..."

    foreach ($p in $procs) {
        try {
            $p.CloseMainWindow() | Out-Null
            Start-Sleep -Seconds 3

            if (!$p.HasExited) {
                Write-Host "Process did not exit, killing process id $($p.Id)"
                $p.Kill()
            }
        } catch {
            Write-Warning "Failed to close EyeRest process: $($_.Exception.Message)"
        }
    }
}
