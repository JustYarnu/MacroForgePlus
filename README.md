<p align="center">
  <img src="assets/Logo_small.svg" width="200" />
</p>
<h1 align="center">Macro Forge Plus</h1>
<p align="center">
  DSL-powered macro automation engine with recording capabilities
</p>
<p align="center">
  <img src="https://img.shields.io/badge/Code-C%23-555555?style=for-the-badge&labelColor=555555&color=2ecc71" />
  <img src="https://img.shields.io/badge/Framework-.NET-555555?style=for-the-badge&labelColor=555555&color=8e44ad" />
  <img src="https://img.shields.io/badge/Easy_to_use-Yes-555555?style=for-the-badge&labelColor=555555&color=3498db" />
</p>
  
# About

Macro Forge Plus is a desktop automation tool that allows you to create, record, and execute macros using a simple yet flexible Domain-Specific Language (DSL).

## Key Features

- **Script Editor**: Write and edit macros using a simple, readable DSL
- **Input Recording**: Record your mouse and keyboard actions and convert them to macro scripts
- **Custom Keybinds**: Bind any macro script to a custom keyboard shortcut for instant execution
- **Profile Management**: Save and load keybind profiles as `.mprofile` files
- **Global Hotkeys**: Execute macros from anywhere using global hotkeys
- **Syntax Highlighting**: Built-in editor with syntax highlighting for easy script writing
- **Conditional Logic**: Support for if/held conditions and loops
- **Variables**: Define and use variables in your scripts
- **Functions**: Create reusable function blocks

# Tech Stack

- **Language**: C# (.NET 10.0)
- **Framework**: Windows Forms (WinForms)
- **Editor**: AvalonEdit (syntax highlighting)
- **Input**: WindowsInput library for low-level input simulation
- **Platform**: Windows only (uses Windows API for global hotkeys)

# Getting Started

## Prerequisites

- .NET 10.0 SDK or later
- Windows 10/11

## Building from Source

```bash
# Clone the repository
git clone https://github.com/JustYarnu/MacroForgePlus.git

# Navigate to the project directory
cd MacroForgePlus/app/WinFormsApp

# Build the project
dotnet build

# Run the application
dotnet run
```

## Usage

### Creating a Macro

1. Click **File -> New Script** or press `Ctrl+N` to open the script editor
2. Write your macro using the DSL syntax (see [Command Reference](doc/commandReference.md))
3. Save your script with the `.macro` extension
4. Press `F5` to execute the macro

### Recording Input

1. Open a script in the editor
2. Click **Record -> Start Recording** or press `F6`
3. Perform the actions you want to record
4. Press `F7` to stop recording
5. The recorded actions will be added to your script

### Setting Up Custom Keybinds

1. Click **Keybinds** in the top menu
2. Click **Add** to create a new keybind
3. Select a key combination (e.g., `Ctrl+Alt+K`)
4. Browse and select a `.macro` script file
5. Click **OK** to save the keybind
6. Click **Save Profile** to save your keybinds to a `.mprofile` file
7. Press your custom keybind anywhere to execute the bound macro

### Loading a Keybind Profile

1. Click **Keybinds** in the top menu
2. Click **Load Profile**
3. Select a `.mprofile` file
4. Your keybinds will be loaded and activated

# Default Hotkeys

| Hotkey | Action |
|--------|--------|
| `F5` | Execute current macro |
| `F6` | Start recording |
| `F7` | Stop recording |
| `Escape` | Abort running macro |
| `Ctrl+N` | New script |
| `Ctrl+O` | Open macro file |

# Documentation

For detailed information about the macro DSL syntax, commands, and advanced features, see the [Command Reference](doc/commandReference.md).

# Project Structure

```
Root/
├── app/
│   └── WinFormsApp/
│       ├── GUI/                    # User interface components
│       │   ├── main.cs             # Main application window
│       │   ├── ScriptEditor.cs     # Macro script editor
│       │   ├── RecordingManager.cs # Input recording functionality
│       │   ├── KeybindManagerDialog.cs  # Keybind management UI
│       │   └── KeybindEditDialog.cs     # Keybind editor UI
│       ├── Keybinds/               # Keybind system
│       │   ├── KeybindEntry.cs     # Individual keybind model
│       │   ├── KeybindProfile.cs   # Profile management
│       │   └── KeybindManager.cs   # Hotkey registration & execution
│       ├── Scripting/              # Macro DSL parser & commands
│       ├── Execution/              # Macro execution engine
│       └── Input/                  # Input simulation
├── doc/
│   └── commandReference.md         # DSL documentation
└── assets/                         # Application assets
```
