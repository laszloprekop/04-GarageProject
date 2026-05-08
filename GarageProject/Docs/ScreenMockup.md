# Screen Mockup — Garage 1.0

Rough layout sketch before implementation. Not to scale.

---

## Main screen

```
 GARAGE v1.0  Garage  Search  Help                           ← menu bar
 5 / 10 spaces used                                          ← title bar
┌────────────┬────────────┬──────────┬────────┐
│ Reg. No    │ Type       │ Color    │ Wheels │              ← table header
├────────────┼────────────┼──────────┼────────┤
│ ABC123     │ Car        │ Red      │ 4      │
│ XYZ789     │ Motorcycle │ Black    │ 2      │  ← selected row (highlighted)
│ DEF456     │ Bus        │ Yellow   │ 6      │
│ ...        │            │          │        │
│            │            │          │        │
└────────────┴────────────┴──────────┴────────┘
 Car: 2  │  Motorcycle: 1  │  Bus: 1  │  Boat: 1            ← type summary
 F3 Add       F4 Remove       F7 Search          F10 Quit   ← status bar
```

---

## Add vehicle dialog (F3)

```
        ┌────────── Add Vehicle ──────────┐
        │                                 │
        │  Type:                          │
        │  [ Car              ▼ ]         │
        │                                 │
        │  Registration:                  │
        │  [                    ]         │
        │                                 │
        │  Color:                         │
        │  [                    ]         │
        │                                 │
        │  Wheels:                        │
        │  [ 4  ]                         │
        │                                 │
        │        [ OK ]  [ Cancel ]       │
        └─────────────────────────────────┘
```

---

## Search result (F7)

```
        ┌──── Results for "ABC" — 1 found ────┐
        │ ┌──────────┬──────┬───────┬───────┐ │
        │ │ Reg. No  │ Type │ Color │Wheels │ │
        │ ├──────────┼──────┼───────┼───────┤ │
        │ │ ABC123   │ Car  │ Red   │ 4     │ │
        │ └──────────┴──────┴───────┴───────┘ │
        │              [ Close ]              │
        └─────────────────────────────────────┘
```
