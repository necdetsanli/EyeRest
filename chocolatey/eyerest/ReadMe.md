# eyerest – Chocolatey Package

This folder contains the Chocolatey package definition for **EyeRest**, a small Windows tray application that helps you follow the 20–20–20 rule for eye health by showing periodic desktop notifications.

---

## Project vs. Package

- **Project homepage / source code**  
  https://github.com/necdetsanli/EyeRest

- **This folder**  
  Contains the Chocolatey packaging files only:
  - `eyerest.nuspec` – package metadata  
  - `tools/chocolateyinstall.ps1` – install script (wraps the official MSI)  
  - `tools/chocolateyuninstall.ps1` – uninstall script  
  - `tools/VERIFICATION.txt` – instructions for verifying the MSI checksum  

The Chocolatey package does **not** modify `PATH` or global environment variables.  
It simply downloads and installs the official EyeRest MSI from the GitHub Releases page.

---

## Installation (after publishing)

Once the package is approved on the Chocolatey Community Repository, it can be installed with:

```powershell
choco install eyerest
```

To upgrade to the latest version:

```powershell
choco upgrade eyerest
```

To uninstall:

```powershell
choco uninstall eyerest
```

---

## Building and testing the package locally

From this folder (where `eyerest.nuspec` lives):

1. **Pack the package**

```powershell
   choco pack  
```

This will generate a file similar to:
   
```text
eyerest.1.3.0.nupkg
```

2. **Install locally from the current directory**

```powershell
choco install eyerest -s .
```

3. **Upgrade locally (after repacking a new version)**

```powershell
choco upgrade eyerest -s .
```

4. **Uninstall for testing**

```powershell
choco uninstall eyerest
```

---

## Package behavior

The Chocolatey package:

- Downloads the official EyeRest MSI from the GitHub Releases page.  
- Verifies the download using a SHA256 checksum defined in `tools/chocolateyinstall.ps1`.  
- Runs the MSI in silent or quiet mode to install EyeRest for the current machine/user (depending on MSI configuration).  

It does **not**:

- Add EyeRest to PATH  
- Install additional services  
- Add global environment variables  

Shortcuts (Start Menu / optional Desktop) and uninstallation are handled by the MSI itself.

---

## Verification

For details on how to verify the integrity of the MSI used by this package, see:

- `tools/VERIFICATION.txt`

That file explains:

- The official download URL of the MSI (GitHub Releases)  
- How to compute the SHA256 checksum locally (for example with `Get-FileHash`)  
- The expected checksum that Chocolatey uses to validate the installer  

---

## Links

- EyeRest project: https://github.com/necdetsanli/EyeRest  
- Issues / bug tracker: https://github.com/necdetsanli/EyeRest/issues  
- Chocolatey docs for packaging: https://docs.chocolatey.org/en-us/create/create-packages
