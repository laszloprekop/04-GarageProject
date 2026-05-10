# Garage 1.0

![demo](demo.gif)

A console application that simulates a simple garage. Park, retrieve, list, and search vehicles through a terminal UI.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Setup

```bash
git clone <repo-url>
cd GarageProject
dotnet restore
```

## Run

```bash
cd GarageProject
dotnet run
```

## Usage

| Key | Action |
|-----|--------|
| F3 | Add a vehicle |
| F4 | Remove selected vehicle |
| F7 | Search by registration number |
| F10 | Quit |
| ↑ ↓ | Navigate the vehicle list |

All features are also accessible via the menu bar at the top of the screen.

## Project structure

```
GarageProject/
├── Domain/          Vehicle base class, subclasses, Garage<T>
├── Application/     GarageHandler — business logic
└── UI/              Terminal.Gui views and dialogs
```

## Exercise spec

See `GarageProject/Docs/untracked/` for the original exercise specification and design plan.
