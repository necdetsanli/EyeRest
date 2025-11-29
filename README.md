# EyeRest

<p align="center">
  <img src="assets/icon.png" alt="EyeRest icon" width="128" />
</p>

A lightweight Windows tray application that gently reminds you to follow the **20–20–20 rule**:  
every **20 minutes**, look at something **20 feet (~6 meters) away** for at least **20 seconds**.

---

## ✨ Overview

EyeRest sits quietly in the Windows system tray and periodically nudges you to take a short visual break.  
It is designed to be:

- **Minimal** – no bloated UI, just a tray icon and a small configuration dialog.
- **Non-intrusive** – reminders are shown as balloon notifications near the tray.
- **Session-focused** – settings apply to the current run; when you restart the app, it goes back to the safe default of reminding you.

EyeRest is built with **.NET Framework 4.8** and **Windows Forms**, targeting classic Windows desktop users who spend long hours in front of a screen.

---

## 👁 The 20–20–20 Rule

Long screen time can contribute to digital eye strain.  
A common recommendation is the **20–20–20 rule**:

> Every 20 minutes, look at something 20 feet (about 6 meters) away for at least 20 seconds.

EyeRest helps you actually follow this rule by:

- Tracking time in the background.
- Showing a subtle but clear reminder in the system tray area.
- Letting you temporarily disable reminders for the current session when needed.

---

## 🚀 Features

- 🕒 **Automatic reminders**  
  - Default interval: **20 minutes** between notifications.
  - Uses a UI-thread-friendly timer (WinForms `Timer`) to stay responsive.

- 🔔 **Tray balloon notifications**  
  - Displays a balloon tip from the tray icon with a short reminder.
  - Optional system sound (depending on your implementation).

- ⚙️ **Simple configuration dialog**  
  - A single checkbox to **enable or disable reminders** for the current session.
  - Accessible via the tray icon’s context menu.

- 🧽 **Clean exit & resource management**  
  - Gracefully disposes the notify icon and timer on exit.
  - Uses `ApplicationContext` to run without a main window.

- 🧱 **Safe defaults**  
  - Reminders are **enabled by default** on every start.
  - Per-session configuration only (no persistent settings written to disk unless you decide to add that).

---

## 💾 Download

For end users, the simplest way to install EyeRest is via the MSI installer:

1. Go to the **[Releases](https://github.com/necdetsanli/EyeRest/releases)** page of this repository.
2. Download the latest `EyeRest-<version>-setup.msi` file (for example: `EyeRest-1.0.0-setup.msi`).
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

- **Configuration**
  - Right-click the tray icon and select **Configuration…**  
    *or* double-click the icon.
  - A small dialog will appear with a checkbox:
    - **“Warn Me After Every 20 Minutes”**
  - Check/uncheck the box and click **OK** to apply for the current session.

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
    - Creates the tray `NotifyIcon` using `AppIcon.ico`.
    - Attaches a context menu (Configuration / Exit).
    - Manages a `System.Windows.Forms.Timer` that:
      - Uses a 20-minute interval in Release builds.
      - Calls `ShowMessage` on each tick to display the balloon tip if reminders are enabled.

- **Configuration dialog**
  - `Configuration` form:
    - Has a single `CheckBox` bound to the `ShowMessage` setting.
    - Reads the setting on `Shown`.
    - Writes the setting back on `FormClosing` if the user clicked **OK**.
    - Intentional design: settings are **not persisted** between runs, only stored in memory.

---

## 🗂 Project Structure

A typical repository layout for EyeRest might look like:

```text
EyeRest/
├─ README.md
├─ CHANGELOG.md
├─ LICENSE
├─ EyeRest.sln
├─ .gitignore
├─ .editorconfig
├─ src/
│  └─ EyeRest/
│     ├─ EyeRest.csproj
│     ├─ Program.cs
│     ├─ EyeRestContext.cs
│     ├─ Configuration.cs
│     ├─ Configuration.Designer.cs
│     ├─ Configuration.resx
│     ├─ AppIcon.ico
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
```
---

## 🛠 Development Notes

- **Target framework:** `.NET Framework 4.8`
- **UI framework:** Windows Forms
- **Language version:** C# 7.3
- **Key WinForms concepts used:**
  - `ApplicationContext` for lifetime management without a main window.
  - `NotifyIcon` for tray integration.
  - `Timer` for periodic callbacks on the UI thread.
  - Resource & settings management via `Resources.resx` and `Settings.settings`.

If you want to extend EyeRest, some ideas:

- Make the reminder interval configurable (e.g. slider or numeric input).
- Add basic logging instead of swallowing exceptions silently.
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

## 🙌 Acknowledgements

EyeRest is inspired by the need to protect our eyes during long coding, gaming, or office sessions.  
If this tool helps you remember to look away from the screen from time to time, it’s doing its job.

The application icon is based on work by **Maxicons** from **https://icon-icons.com/**,  
licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/).  
Original icon: https://icon-icons.com/icon/eye-disease-medical-health-retina-optical-lens/133505

Stay healthy, and don’t forget to blink.