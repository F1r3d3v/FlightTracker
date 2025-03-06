# Object-Oriented Flight Data Management System

## Description

Semester project developed for an Object-Oriented Design course. It is designed as an application for managing flight data, built on solid object-oriented principles to ensure extensibility and scalability. The project applies various design patterns to support flexible development and future enhancements.

## Project Goals

-   Create a functional application for managing flight data.
-   Design the application based on "solid" foundations of object-oriented software design, allowing for easy extension in the future.
-   Apply design patterns that will support the development of the application and allow for its flexibility and scalability.

## Project Stages

-   [x] Stage 1 - Data loading and serialization
-   [x] Stage 2 - New data source
-   [x] Stage 3 - GUI integration
-   [x] Stage 4 - Message review
-   [x] Stage 5 - Data update
-   [x] Stage 6 - Data filtering

## Table of Contents

1.  [Description](#description)
2.  [Project Goals](#project-goals)
3.  [Project Stages](#project-stages)
4.  [Installation](#installation)
5.  [Usage Guide](#usage-guide)
    -   [Running the Application](#running-the-application)
    -   [Loading Data](#loading-data)
    -   [Using the Text-Based User Interface (TUI)](#using-the-text-based-user-interface-tui)
    -   [Querying Data](#querying-data)
    -   [Using the GUI](#using-the-gui)
    -   [Logging](#logging)
6. [Summary](#summary)

## Installation

1.  **Prerequisites:**
    -   .NET 8.0 SDK

2.  **Clone the Repository:**

    ```bash
    git clone https://github.com/F1r3d3v/FlightTracker.git
    cd FlightTracker
    ```

3.  **Restore Dependencies:**

    - Navigate to the project's root directory (`src/ProjOb`):
      ```bash
      cd src/ProjOb
      ```

4. **Build:**
   ```
    dotnet build
   ```
   
5. **Local Packages**
      The project uses a local NuGet package source.  Make sure any `.nupkg` files (like `FlightTrackerGUI.1.0.0.nupkg`) are located in the `ThirdParty/NuGet` directory.  The `NuGet.config` file is set up to use this local source:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
        <packageSources>
            <add key="Local Packages" value="./ThirdParty/NuGet" />
        </packageSources>
    </configuration>
    ```

6. **Third-party DLLs**
    The project references DLLs in the ThirdParty folder.
        - `ThirdParty/FTGUI/FlightTrackerGUI.dll`
        -   `ThirdParty/NSS/NetworkSourceSimulator.dll`
        
    Make sure to include them in your build.


## Usage Guide

### Running the Application

1.  **Navigate to the build directory:**

    ```bash
    cd bin/Debug/net8.0
    ```

2.  **Run the application:**

    ```bash
    dotnet ProjOb.dll
    ```

### Loading Data
The application supports loading data from different sources:

-   **Local Database (FTR format):**  Loads data from a `.ftr` file.  An example file (`example_data.ftr`) is provided in the `res` directory.
-   **Network Stream:** Simulates a network data stream using the `NetworkSourceSimulator.dll`. It uses `example_data.ftr`.
- **Local Database with Network Stream Changes:** Loads initial data from a `.ftr` file and then applies updates from a `.ftre` file via the `NetworkSourceSimulator`.  Uses `example_data.ftr` and `example.ftre`

On startup, the application prompts you to choose a data source.

### Using the Text-Based User Interface (TUI)

After selecting a data source, the application presents a main menu with several options:

-   **Flight Tracker:** Launches the graphical user interface (GUI) to visualize flight data.
-   **Query:** Allows you to enter SQL-like queries to retrieve and manipulate data.
-   **Report:** Generates reports using various media outlets (Newspaper, Radio, Television).
-   **Logs:** Provides options to show or clear application logs.
-   **Snapshot:** Creates a JSON snapshot of the current database state.  The snapshot is saved to a file named `snapshot_<timestamp>.json` in the execution directory.
-   **Exit:** Terminates the application.

### Querying Data

The `Query` option allows you to interact with the database using a custom query language.  Here's a breakdown of the supported commands:

-   **DISPLAY:** Retrieves and displays data.
    -   Syntax: `DISPLAY <varlist> FROM <object_class> [WHERE <condition>]`
    -   `<varlist>`:  A comma-separated list of object attributes to display, or `*` for all attributes.
    -   `<object_class>`: The type of object to query (e.g., `Crews`, `Passengers`, `Flights`, etc.).
    -   `<condition>` (optional): A boolean expression to filter the results.

-   **UPDATE:** Modifies existing data.
    -   Syntax: `UPDATE <object_class> SET (<attribute1> = <value1>, <attribute2> = <value2>, ...) [WHERE <condition>]`
    -   `<object_class>`:  The type of object to update.
    -   `<attribute> = <value>`:  Pairs specifying the attributes to update and their new values.  Multiple pairs are separated by commas and enclosed in parentheses.
    -   `<condition>` (optional): A boolean expression to select which objects to update.

-   **DELETE:** Removes data.
    -   Syntax: `DELETE <object_class> [WHERE <condition>]`
    -   `<object_class>`: The type of object to delete.
    -   `<condition>` (optional): A boolean expression to select which objects to delete.

-   **ADD:** Adds new data.
    -   Syntax: `ADD <object_class> NEW (<attribute1> = <value1>, <attribute2> = <value2>, ...)`
    - `<object_class>`: The type of object to add.
    - `<attribute> = <value>`: Pairs specifying the attributes of the new object and their values. Multiple pairs are separated by commas and enclosed in parentheses.

**Supported Operators in Conditions:**

-   Arithmetic: `+`, `-`, `*`, `/`
-   Relational: `<`, `<=`, `>`, `>=`, `=`, `!=`
-   Logical: `AND`, `OR`, `NOT`

**Examples:**

-   Display all attributes of all flights:
    ```
    DISPLAY * FROM Flights
    ```

-   Display the ID and Name of passengers with more than 1000 miles:
    ```
    DISPLAY ID, Name FROM Passengers WHERE Miles > 1000
    ```

-   Update the role of a crew member with ID 123 to "Pilot":
    ```
    UPDATE Crews SET (Role = "Pilot") WHERE ID = 123
    ```

- Delete all airports in "USA":

   ```
   DELETE Airports WHERE Country = "USA"
   ```

-   Add a new airport:
    ```
    ADD Airports NEW (ID = 1000, Name = "New Airport", Code = "NEW", Longitude = -73.93, Latitude = 40.73, AMSL = 100, Country = "USA")
    ```

-   Chained identifiers are supported:
    ```
    DISPLAY Origin.Country FROM Flights
    ```

### Using the GUI
The GUI option visualizes flight data using the `FlightTrackerGUI.dll` library. The GUI displays the flights in real time using the data from the selected data source, and updates are shown based on the simulator data.

### Logging

The application logs various events, including data updates and errors. Logs are stored in the `logs` directory, with a new log file created daily (e.g., `yyyy-MM-dd.log`). You can view logs through TUI and choose to clear them.

## Summary

This project provides a practical implementation of object-oriented design principles and design patterns in a real-world application context. It demonstrates data loading, serialization, user interface integration (both TUI and GUI), data manipulation, and logging. The project is designed to be extensible, allowing for future enhancements and additions.
