# Changelog

All notable changes to this project will be documented in this file.

The format is inspired by [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),  
and the project aims to follow [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

### Added
- (Planned) Configurable reminder interval instead of a fixed 20 minutes
- (Planned) Snooze functionality to temporarily pause reminders
- (Planned) More flexible notification options (sound on/off, different cues)

---

## [1.1.0] - 2025-12-01

### Added
- Windows 10/11 **toast notifications** for reminder messages using `Microsoft.Toolkit.Uwp.Notifications`.
- Fallback logic to use classic tray balloon tips when toast notifications are not available or fail at runtime.

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

[Unreleased]: https://github.com/necdetsanli/EyeRest/compare/v1.1.0...HEAD
[1.1.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.1.0
[1.0.1]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.0.1
[1.0.0]: https://github.com/necdetsanli/EyeRest/releases/tag/v1.0.0
