$ErrorActionPreference = 'Stop'

$packageName = $env:ChocolateyPackageName

$packageArgs = @{
  packageName    = $packageName
  softwareName   = 'EyeRest*'
  fileType       = 'MSI'
  silentArgs     = '/qn /norestart'
  validExitCodes = @(0, 3010, 1605, 1614, 1641)
}

[array]$keys = Get-UninstallRegistryKey -SoftwareName $packageArgs.softwareName

if ($keys.Count -eq 1) {
  $keys | ForEach-Object {
    $packageArgs.silentArgs = "$($_.PSChildName) $($packageArgs.silentArgs)"

    if ($packageArgs.ContainsKey('file')) {
      $packageArgs.Remove('file')
    }

    Uninstall-ChocolateyPackage @packageArgs
  }
}
elseif ($keys.Count -eq 0) {
  Write-Warning "$packageName appears to already be uninstalled."
}
else {
  Write-Warning "$($keys.Count) matching uninstall entries were found for $packageName."
  Write-Warning "To avoid accidentally removing the wrong application, nothing will be uninstalled."
  Write-Warning "Matched entries:"
  $keys | ForEach-Object { Write-Warning "- $($_.DisplayName)" }
}
