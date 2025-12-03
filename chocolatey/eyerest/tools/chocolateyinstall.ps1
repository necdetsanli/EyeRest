$ErrorActionPreference = 'Stop' 

$toolsDir = Split-Path -Parent $MyInvocation.MyCommand.Definition

$url64 = 'https://github.com/necdetsanli/EyeRest/releases/download/v1.3.0/EyeRest-1.3.0-setup.msi'

$packageArgs = @{
  packageName    = $env:ChocolateyPackageName
  fileType       = 'msi'
  url64bit       = $url64
  softwareName   = 'EyeRest*'
  checksum64     = '1C9BF2706B8459A4F6FDF11B3654F188C16EE322BD9EBEF2D62FA0E52D782EA9'
  checksumType64 = 'sha256'
  silentArgs    = "/qn /norestart /l*v `"$($env:TEMP)\$($packageName).$($env:chocolateyPackageVersion).log`""
  validExitCodes = @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs
