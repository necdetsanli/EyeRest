# Changelog

All notable changes to this project will be documented in this file.

The format is inspired by [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),  
and the project aims to follow [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Planned
- Snooze functionality to temporarily pause reminders
- More flexible notification options (sound on/off, different cues)
- Show a brief notification when the app starts to confirm that EyeRest is running
- Add an option to check for updates (e.g. against the latest GitHub release)

---

## [1.3.0] - 2025-12-03

### Added
- Optional **“Remember my settings for future sessions”** option in the Options dialog.
- Optional **“Start automatically with Windows”** option, using the HKCU `Run` key for the current user.
- INI-based settings storage (`EyeRest.ini`) with a **portable-first** strategy:
  - Tries to store settings next to the executable if the folder is writable.
  - Falls back to `%APPDATA%\EyeRest\EyeRest.ini` when the exe directory is not writable.

### Changed
- Updated documentation (README) to describe persistent settings, auto-start behavior, and the INI-based configuration approach.

---

## [1.2.0] - 2025-12-02

### Added
- New **Options** dialog fields:
  - A numeric input to configure the reminder interval in minutes (1–120, default 20).
  - A checkbox to enable or disable using left-click on the tray icon to toggle reminders.
- An **About** dialog showing app name, version, author info, privacy note and a link to the GitHub repository.
- Short confirmation notifications when reminders are toggled on or off:
  - Prefer Windows toast notifications when available.
  - Fall back to tray balloon tips if toasts are not supported or fail.

### Changed
- Renamed the tray menu item from **“Configuration”** to **“Options…”** and updated the dialog title accordingly.
- Updated the reminder checkbox text to **“Enable reminders”** for clearer wording.
- The reminder timer now uses a session-only `ReminderIntervalMinutes` value instead of a hardcoded 20-minute interval in Release builds, while keeping the 5-second interval for DEBUG builds.
- Left-click behavior on the tray icon is now configurable:
  - When the left-click toggle option is enabled, a single click toggles the reminder state, updates the UI/timer and shows a confirmation notification.
  - When it is disabled, a single left-click opens the Options dialog for better discoverability.
- Double-click continues to open the Options dialog and cancels the single-click action to avoid duplicate handling.

### Fixed
- Ensured the tray icon consistently uses the dedicated **snooze icon** when reminders are disabled, instead of occasionally falling back to the default Windows application icon, including when using toast notifications with balloon fallback.

---

## [1.1.0] - 2025-12-01

### Added
- Windows 10/11 **toast notifications** for reminder messages using `Microsoft.Toolkit.Uwp.Notifications`.
- Fallback logic to use classic tray balloon tips when toast notifications are not available or fail at runtime.
- Published EyeRest to the Microsoft Store as an MSIX-packaged Win32 desktop app.

### Changed
- Replaced `System.Windows.Forms.Timer` with `System.Threading.Timer` to schedule reminders on a ThreadPool thread.
- Introduced a small UI invoker `Control` and `BeginInvoke` to safely marshal timer callbacks back to the UI thread.
- Refined `NotifyIcon` and tooltip behavior, including handling double-click to open the configuration dialog.
- Improved shutdown behavior by waiting briefly for any in-flight timer callbacks to complete before disposing the timer, reducing the risk of race conditions.
- Updated assembly metadata:
  - `AssemblyCompany` set to `"Necdet Şanlı"`.
  - `AssemblyConfiguration` set to `"Release"`.
  - Version fields updated to `1.1.0.0` / informational version `1.1.0`.

---

## [1.0.1] - 2025-11-29

### Changed
- Updated the application icon for consistent branding across the tray, configuration window and Start Menu shortcuts.
- Embedded the app icon at the top of the README for better visual identity.
- Added proper attribution and licensing information for the icon asset.

### Fixed
- Ensured the MSI installer uses the new icon for shortcuts and the Apps & Features entry.

---

## [1.0.0] - 2025-11-28

### Added
- First stable release of **EyeRest**, a lightweight Windows tray application for the **20–20–20 rule**
- Periodic reminder every 20 minutes via tray balloon notifications
- System tray icon with tooltip indicating whether reminders are enabled or disabled
- Configuration dialog with:
  - “Warn Me After Every 20 Minutes” checkbox
  - Per-session setting that applies only to the current run
- Optional system sound played together with the reminder balloon (depending on OS settings)
- Clean lifetime management using `ApplicationContext` and proper disposal of `NotifyIcon` and timers
- Project documentation:
  - `README.md` with overview, features, usage, development notes and contribution info
  - `LICENSE` file with the MIT License
  - Initial `CHANGELOG.md` following a Keep a Changelog–style format

---

[Unreleased]: https://github.com/necdetsanli/EyeRest/compare/v1.3.0...HEAD
[1.3.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.3.0
[1.2.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.2.0
[1.1.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.1.0
[1.0.1]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.0.1
[1.0.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.0.0
