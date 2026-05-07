# C# Exercise 4 - Garage 1.0

> **NOTE** — The result of the exercise must be shown to the teacher and approved before it can be considered completed.

## A first overarching project

To tie together much of what you have learned, we will now build a **garage / console application**. This application shall provide the functionality that a system might need if it is to be used to simulate a simple garage.

It must therefore be possible to park vehicles, retrieve vehicles, see which vehicles are there and what properties they have. All of this in a console application with a main menu and submenus.

The reason you should program a garage is that it is easy to anchor the partitioning of the whole. We can mainly divide a garage into the following parts:

- **The Garage**: A representation of the building itself. The garage is a place where a quantity of vehicles can be stored. The garage can therefore be represented as a **collection of vehicles**.
- **Vehicle**: Cars, motorcycles, unicycles, or whatever type of vehicle one wants to put in the garage.

These are the two "object types" you see in a physical garage. But if we look more closely, there should also be subclasses to vehicle, that is, each vehicle type is its own subclass in the system. In addition to this, functionality is required that handles vehicles being placed in the garage, that vehicles can be taken out of the garage, and that we can get a presentation of what is in the garage and search within it.

In more programming-friendly terms, as a **minimum** we shall therefore have:

- A *collection* of vehicles; the class `Garage`.
- A vehicle class, the class `Vehicle`.
- A number of subclasses to vehicle.
- A user interface that lets us use the functionality of a garage. All interaction with the user takes place here.

## Requirements specification

Vehicles shall be implemented as the class `Vehicle` and subclasses to it.

- `Vehicle` contains all the properties that should exist in all vehicle types. For example: registration number, color, number of wheels and other properties you can think of.
- The registration number is unique.
- At minimum the following subclasses must exist:
  - `Motorcycle`
  - `Airplane`
  - `Car`
  - `Bus`
  - `Boat`
- These shall implement **at least one own property** each, e.g.:
  - `Number of Engines`
  - `Cylinder volume`
  - `Fueltype (Gasoline/Diesel)`
  - `Number of seats`
  - `Length`

The class does not need to inherit from any other class or implement any other interface.

The collection of vehicles shall internally in the class be handled as an **array**. The internal array shall be **private**. When instantiating a new garage, the **capacity** must be specified as an argument to the constructor.

> We shall **NOT** use a `List<Vehicle>` internally in the Garage class!!!!

## Functionality

It must be possible to:

- List all parked vehicles.
- List vehicle types and how many of each are in the garage.
- Add and remove vehicles from the garage.
- Set a capacity (number of parking spaces) when instantiating a new garage.
- Possibility to populate (insert vehicles) the garage with a number of vehicles from the start.
- Find a specific vehicle via the registration number. It must work with both `ABC123` as well as `Abc123` or `AbC123`.
- Search for vehicles based on one or more properties (all possible combinations from the base class `Vehicle`). For example:
  - All black vehicles with four wheels.
  - All motorcycles that are pink and have 3 wheels.
  - All trucks.
  - All red vehicles.
- The user must get feedback that things went well or badly. For example, when we have parked a vehicle we want a confirmation that the vehicle is parked. If it doesn't work, the user wants to know why.

The program shall be a console application with a text-based user interface.

From the interface, it must be possible to:

- Navigate to **all** functionality of the garage via the interface.
- Create a garage with a user-specified size.
- Shut down the application from the interface.

The application shall handle input errors in a robust manner, so that it does **not crash** on incorrect input or use.

## Suggestions for extra functionality (not required)

- Possibility to also search on the vehicle-specific properties.
- Handle multiple garages that can have different types of vehicles in them, for example a hangar, a regular garage, and a motorcycle garage.
  - This will entail being able to navigate between the different garages in the UI to be able to perform the previously mentioned functions on only the garage that is currently selected.
  - It must be clearly shown which garage you are currently working with.
- A garage no longer consists of vehicles but of parking spaces, which in turn can hold vehicles.
- It is possible via C# to write to and read from the file system from your application. Find out how to save your garage (via menu or automatically on shutdown) and load your garage (via menu or automatically on startup of the application).
- Different vehicles take up different amounts of space, e.g. a car takes 1 spot, a boat takes 2 spots, an airplane requires 3 spots, etc., a motorcycle takes only 1/3 of a spot.
- When parking, only the vehicles that the garage has room for should be shown as options.
- Read the size of the garage via configuration.
- Any optional functionality you think should exist.

## Good luck!
