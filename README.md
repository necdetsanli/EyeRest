# EyeRest

<p align="center">
  <img src="assets/icon.png" alt="EyeRest icon" width="128"/>
</p>

<p align="center">
  <a href="https://apps.microsoft.com/detail/9MW31PJW185Q?referrer=appbadge&mode=full">
	<img src="https://get.microsoft.com/images/en-us%20dark.svg" width="200"/></a>
</p>


<p align="center">
  <a href="https://github.com/necdetsanli/EyeRest/releases/latest"><img src="https://img.shields.io/github/v/release/necdetsanli/EyeRest?label=release" alt="Latest release" /></a>
  <a href="https://github.com/necdetsanli/EyeRest/releases"><img src="https://img.shields.io/github/downloads/necdetsanli/EyeRest/total?label=downloads" alt="Downloads" /></a>
  <a href="https://github.com/necdetsanli/EyeRest/blob/master/LICENSE"><img src="https://img.shields.io/github/license/necdetsanli/EyeRest?label=license" alt="License" /></a>
  <img src="https://img.shields.io/badge/telemetry-none-success" alt="No telemetry" style="pointer-events: none;"/>
  <a href="https://github.com/0pandadev/awesome-windows"><img src="https://awesome.re/mentioned-badge.svg" alt="Mentioned in Awesome Windows" /></a>
</p>


A lightweight Windows tray application that gently reminds you to follow the **20–20–20 rule**:  
every **20 minutes**, look at something **20 feet (~6 meters) away** for at least **20 seconds**.

---

## ✨ Overview

EyeRest sits quietly in the Windows system tray and periodically nudges you to take a short visual break.  
It is designed to be:

- **Minimal** – no bloated UI, just a tray icon and a small options dialog.
- **Non-intrusive** – reminders are shown as **Windows 10/11 toast notifications** when possible, with a tray balloon fallback.
- **Session-focused by default** – settings apply to the current run, with an option to **remember your preferences across sessions** and **start automatically with Windows** if you want it.

EyeRest is built with **.NET Framework 4.8** and **Windows Forms**, targeting classic Windows desktop users who spend long hours in front of a screen.

---

## 👁 The 20–20–20 Rule

Long screen time can contribute to digital eye strain.  
A common recommendation is the **20–20–20 rule**:

> Every 20 minutes, look at something 20 feet (about 6 meters) away for at least 20 seconds.

EyeRest helps you actually follow this rule by:

- Tracking time in the background.
- Showing a subtle but clear reminder on your desktop.
- Letting you temporarily disable reminders for the current session when needed.

---

## 🚀 Features

- 🕒 **Automatic reminders**  
  - Default interval: **20 minutes** between notifications.
  - The reminder interval is **configurable per session** (e.g. 5, 20, 45 minutes).
  - Uses a background `System.Threading.Timer` with safe marshaling back to the UI thread for responsiveness.

- 🔔 **Desktop notifications**  
  - Uses **Windows 10/11 toast notifications** (via `Microsoft.Toolkit.Uwp.Notifications`) when available.
  - Falls back to a tray balloon tip if toast notifications are not supported or fail at runtime.
  - Optional system sound (depending on OS settings).

- ⚙️ **Simple options dialog**  
  - Lets you **enable or disable reminders**.
  - Allows configuring the **reminder interval in minutes** within a safe range.
  - Optional setting to **use left-click on the tray icon to toggle reminders** on/off.
  - Optional **“Remember my settings for future sessions”** checkbox to persist your preferences.
  - Optional **“Start automatically with Windows”** checkbox to enable auto-start.
  - Accessible via the tray icon’s context menu (**Options…**) or by double-clicking the icon.

- 🧽 **Clean exit & resource management**  
  - Gracefully disposes the notify icon and timers on exit.
  - Uses `ApplicationContext` to run without a main window.
  - Ensures timer callbacks complete (with a short timeout) during shutdown to avoid race conditions.

- 🧱 **Safe defaults**  
  - Reminders are **enabled by default** on every start.
  - By default, settings are **per-session only**.
  - When “Remember my settings for future sessions” is enabled:
    - Preferences are saved to a small **INI file (`EyeRest.ini`)**.
    - The app first tries to store the INI next to the executable (for a portable-style setup).
    - If the exe folder is not writable, it falls back to `%APPDATA%\EyeRest\EyeRest.ini`.

- ℹ️ **About dialog**  
  - Shows app name, version, author information, a short privacy note, and a link to the GitHub repository.

---

## 💾 Download

### From Microsoft Store

EyeRest is available on the Microsoft Store:

[**➡ Get EyeRest from Microsoft Store**](https://apps.microsoft.com/detail/9MW31PJW185Q?)

> The Store version is packaged as an MSIX desktop app and benefits from the Store’s installation, update and validation mechanisms.

### Direct download (GitHub Releases)

If you prefer a direct installer:

1. Go to the **[Releases](https://github.com/necdetsanli/EyeRest/releases)** page.
2. Download the latest `EyeRest-<version>-setup.msi` file (for example: `EyeRest-1.3.0-setup.msi`).
3. Double-click the MSI and follow the installation wizard.

> After installation, EyeRest will be available from your Start menu and can be pinned or added to startup according to your preferences.

> **System requirements:** Windows 10/11 (or compatible) and **.NET Framework 4.8**.

---

## 📦 Installation from Source

1. **Build from source**
   - Open `EyeRest.sln` in **Visual Studio** (2019 or later recommended).
   - Ensure you have **.NET Framework 4.8** targeting pack installed.
   - Set the configuration to `Release`.
   - Build the solution.

2. **Run**
   - After a successful build, navigate to:
     - `bin\Release\` under the project folder.
   - Run `EyeRest.exe`.
   - You should now see the EyeRest icon in the Windows system tray.

> 💡 For daily use, you can pin `EyeRest.exe` to your Start menu or add it to your startup applications.

---

## 🧭 Usage

Once EyeRest is running:

- **Tray icon**
  - Appears in the Windows notification area (system tray).
  - Hover to see a tooltip about the current state (reminders enabled/disabled).

- **Options**
  - Right-click the tray icon and select **Options…**  
    *or* double-click the icon.
  - A small dialog will appear where you can:
    - **Enable or disable reminders**.
    - **Adjust the reminder interval** in minutes.
    - Optionally **enable left-click toggle**, so a single left-click on the tray icon turns reminders on/off.
    - Optionally **remember your settings for future sessions**.
    - Optionally **start EyeRest automatically with Windows**.
  - Click **OK** to apply the changes.

- **About**
  - Right-click the tray icon and select **About…** to see basic information about EyeRest:
    - Version number, author, a short description and the GitHub link.

- **Exit**
  - Right-click the tray icon and select **Exit** to close EyeRest.
  - All resources (icon, timers, event handlers) are cleaned up before the app exits.

---

## 🧩 How It Works (Under the Hood)

EyeRest is implemented as a **Windows Forms tray application** with an `ApplicationContext`:

- **Entry point**
  - `Program.Main()` initializes WinForms styles and starts:
    ```csharp
    Application.Run(new EyeRestApplicationContext());
    ```

- **Application context**
  - `EyeRestApplicationContext`:
    - Creates the tray `NotifyIcon` using the main app icon and a separate snoozed icon when reminders are disabled.
    - Attaches a context menu (**Options… / About… / Exit**) and handles double-click to open the Options dialog.
    - Manages a `System.Threading.Timer` that:
      - Uses a configurable interval in Release builds (default 20 minutes, shorter in Debug for testing).
      - Runs callbacks on a ThreadPool thread and marshals UI work back to the UI thread via a small helper `Control`.
      - On each tick, attempts to show a **toast notification**, falling back to a tray balloon tip if necessary.
    - On startup, loads persisted settings (if enabled) from an **INI file** and applies them to the current session.
    - When auto-start is enabled, sets/clears a `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` entry so that EyeRest can start with Windows.

- **Options dialog**
  - `Options` form:
    - Binds to an in-memory `ShowMessage` flag to control whether reminders are active.
    - Provides a numeric input to set the **reminder interval in minutes**.
    - Includes an option to **use left-click on the tray icon to toggle reminders**.
    - Includes **“Remember my settings for future sessions”** and **“Start automatically with Windows”** checkboxes.
    - Reads current values on `Shown` and applies changes on `FormClosing` when the user clicks **OK**.
    - When “Remember my settings for future sessions” is enabled, uses an INI-based helper to persist:
      - Reminder on/off
      - Reminder interval
      - Left-click toggle preference
      - Auto-start preference
    - By default, persistence is off; settings remain **per session only** unless the user explicitly opts in.

- **About dialog**
  - `AboutForm`:
    - Displays the application name and version (from assembly metadata).
    - Shows author and basic privacy information (no telemetry, no personal data collection).
    - Provides a clickable link to the GitHub repository.

---

## 🗂 Project Structure

A typical repository layout for EyeRest might look like:

```text
EyeRest/
├─ README.md
├─ CHANGELOG.md
├─ LICENSE
├─ CONTRIBUTING.md
├─ EyeRest.sln
├─ .gitignore
├─ .editorconfig
├─ assets/
│  └─ icon.png
├─ chocolatey/
│  └─ eyerest/
|     ├─ tools/
│     │  ├─ chocolateybeforemodify.ps1
│     │  ├─ chocolateyinstall.ps1
│     │  ├─ chocolateyuninstall.ps1
│     │  ├─ LICENSE.txt
│     │  └─ VERIFICATION.txt
│     ├─ eyerest.nuspec     
│     └─ ReadMe.md    
├─ src/
│  └─ EyeRest/
│     ├─ EyeRest.csproj
│     ├─ Program.cs
│     ├─ EyeRestContext.cs
│     ├─ Configuration.cs
│     ├─ Configuration.Designer.cs
│     ├─ Configuration.resx
│     ├─ AppIcon.ico
|     ├─ AppSnoozeIcon.ico
|     ├─ AutoStartHelper.cs
|     ├─ IniSettingsHelper.cs
│     ├─ app.config
│     ├─ Properties/
│     │  ├─ AssemblyInfo.cs
│     │  ├─ Resources.resx
│     │  ├─ Resources.Designer.cs
│     │  ├─ Settings.settings
│     │  └─ Settings.Designer.cs
│     ├─ bin/      (build output – not committed)
│     └─ obj/      (intermediate files – not committed)
└─ EyeRest.Setup/
   ├─ EyeRest.Setup.vdproj
   ├─ Debug/       (installer build output – ignored)
   └─ Release/     (installer build output – ignored)
````

---

## 🛠 Development Notes

- **Target framework:** `.NET Framework 4.8`
- **UI framework:** Windows Forms
- **Language version:** C# 7.3
- **Key concepts used:**
  - `ApplicationContext` for lifetime management without a main window.
  - `NotifyIcon` for tray integration and stateful tray icons (normal vs. snoozed).
  - `System.Threading.Timer` for background periodic callbacks, with marshaling back to the UI thread.
  - Windows 10/11 toast notifications via `Microsoft.Toolkit.Uwp.Notifications`, with tray balloon fallback.
  - Resource & settings management via `Resources.resx` and `Settings.settings`.
  - INI-based settings using WinAPI helpers, with a **portable-first** strategy (exe folder if writable, otherwise `%APPDATA%\EyeRest`).
  - Auto-start integration via `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` when the user enables “Start with Windows”.

If you want to extend EyeRest, some ideas:

- Add richer snooze controls or notification actions.
- Add basic logging instead of swallowing exceptions silently.
- Add more advanced scheduling (e.g. work hours vs. off hours).
- **Contributions are welcome** – feel free to open issues or submit pull requests.

---

## 📜 Changelog

For a detailed list of changes and release history, see the [CHANGELOG](CHANGELOG.md).

---

## 🤝 Contributing

Contributions are welcome!  
Please check [CONTRIBUTING](CONTRIBUTING.md) for guidelines on reporting issues and submitting pull requests.

---

## 📄 License

This project is licensed under the **MIT License**.

See the [LICENSE](LICENSE) file for full license text.

---

## ⭐ Show your support

If you like EyeRest or find it useful, consider giving the project a ⭐ on GitHub or sharing it with others who spend long hours in front of a screen.

---

## 🙌 Acknowledgements

EyeRest is inspired by the need to protect our eyes during long coding, gaming, or office sessions.  
If this tool helps you remember to look away from the screen from time to time, it’s doing its job.

The application icons are based on work by **Maxicons** from **https://icon-icons.com/**,  
licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/).  

Original icon: https://icon-icons.com/icon/eye-disease-medical-health-retina-optical-lens/133505

Snooze icon: https://icon-icons.com/icon/bed-sleep-insomnia-stress-night-awake-sad-moon/133514

Stay healthy, and don’t forget to blink.
