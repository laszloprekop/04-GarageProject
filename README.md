# Garage 1.0

![demo](demo.gif)

A console application that simulates a parking garage. Park, retrieve, list, search, filter, and persist vehicles through a Terminal.Gui v2 interface.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Install

```bash
git clone <repo-url>
cd GarageProject
```

## Run

```bash
dotnet run --project GarageProject
```

The first run restores packages and compiles; subsequent runs boot in a couple of seconds.

## What you see on screen

```
┌─ Garage   Search ────────────────────────────────┐ ← menu bar
│ GARAGE Project v1.0 — 11/15 spaces used         │ ← title  (used / capacity)
│ Reg. No   Type        Color     Wheels           │
│ ABC123    Car         Red       4                │ ← vehicle table
│ DEF456    Car         Green     4                │   (one row per parked vehicle)
│ ...                                              │
│ Car: 3 | Motorcycle: 2 | Bus: 2 | Airplane: 2    │ ← live counts per type
│ F3 Park  F4 Unpark  F5 Search  F6 Filter  ...    │ ← status bar (shortcuts)
└──────────────────────────────────────────────────┘
```

The vehicle table sits in the middle. The row your cursor is on is the one F4 will unpark. The status line summarises counts per type, and the status bar at the bottom shows the most common shortcuts.

## Features

The app supports five vehicle types — **Car**, **Motorcycle**, **Bus**, **Airplane**, **Boat** — each with type-specific properties (fuel type, cylinder volume, seat count, engine count, length).

- **Park** a vehicle with duplicate-registration checks (case-insensitive)
- **Unpark** the highlighted vehicle
- **Search** by partial registration number
- **Filter** by type, color, and/or minimum wheel count (any field blank = match all)
- **Vehicle type summary** — counts grouped by type
- **Save / Load** garage state as JSON
- **Reset** the garage at runtime with a new capacity

## Keyboard reference

Good to know: mouse can used next to keyboard shortcuts too! 

### Main window

| Key       | Action                          |
|-----------|---------------------------------|
| F3        | Park a vehicle                  |
| F4        | Unpark the highlighted row      |
| F5        | Search by registration          |
| F6        | Filter by properties            |
| F7        | Load garage from disk           |
| F8        | Save garage to disk             |
| F10       | Quit                            |
| Ctrl+G    | Reset garage (set new capacity) |
| Alt+G     | Open the Garage menu            |
| Alt+S     | Open the Search menu            |
| ↑ ↓       | Move the row cursor             |

Every action is also reachable from the menu bar.

### Inside any dialog

| Key             | Action                                            |
|-----------------|---------------------------------------------------|
| Tab / Shift+Tab | Move focus between fields and buttons             |
| ↑ ↓             | Move within a list or open dropdown               |
| F4 or Space     | Open a closed dropdown (e.g. vehicle Type)        |
| Enter           | Submit (activates the default button)             |
| Space           | Click the focused button                          |
| Escape          | Close the dialog without saving                   |

In **Add Vehicle**, registration and wheels are required; color may be blank. In **Filter**, every field is optional — leave a field blank to match any value.

## Persistence

`Save` (F8) writes the current garage state — capacity plus vehicles — to `garage.json` next to the `.csproj` file. `Load` (F7) *replaces* the in-memory garage with the file's contents (it doesn't merge).

The save format encodes the concrete vehicle subtype via a `$type` discriminator, so a Motorcycle round-trips back as a Motorcycle rather than a generic Vehicle.

## Project structure

```
GarageProject/
├── Domain/          Vehicle base class, 5 subclasses, Garage<T>
├── Application/     GarageHandler — business logic + JSON persistence
└── UI/              Terminal.Gui views and dialogs
```

## Spec and design

See `GarageProject/Docs/untracked/` for the original exercise specification, design plan, and step-by-step coding script.
