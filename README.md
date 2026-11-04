# Smart Building Control Center

**A hands-on C#/.NET learning project exploring the backend development of a building management system.**

> **Project status: In development.** \
This README describes what I am learning and planning to build, not the features already implemented

## About the project

I started **Smart Building Control Center** to deepen my understanding of C#/.NET development and gain practical experience building backend systems and full-stack applications.

As a full-stack .NET developer, I'm particularly interested in understanding how different parts of an application work together, from backend services and APIs to databases and user interfaces. Building automation provides an interesting opportunity to explore these concepts through a practical, real-world scenario.

I wanted to build something that goes beyond displaying information on a screen. My goal is to understand what happens behind the interface: how C# services represent and manage data, how REST APIs enable communication between application components, how databases store and retrieve information, and how these systems can be designed to remain reliable and maintainable as they grow.

That is the idea behind **Smart Building Control Center**. I'm developing it step by step as a simplified building management system (BMS), starting with an ASP.NET Core Web API and gradually working toward a full-stack application with simulated equipment, PostgreSQL persistence, real-time communication, and an interactive web dashboard.

This is a **learning and portfolio project**, not production-ready building management software. I'm deliberately taking a hands-on approach by implementing features myself, testing my code, reviewing architectural decisions, and learning the reasoning behind the technologies and design patterns I use.

> **Simulation only:** No real building, physical controllers, or operational building network is connected. Device behaviour and sensor data will be simulated in software.

## How this relates to Smart Building Dashboard

I am also working with a separate project, **`smart-building-dashboard`**, which focuses on the operator experience using **Blazor and MudBlazor**. That application demonstrates interactive floor plans, HVAC and lighting controls, trend charts, and alarm handling using local C# simulation data.

**Smart Building Control Center has a different learning focus.** Here, I am exploring the parts that would sit behind a building management interface: APIs, domain models, databases, background processing, testing, and eventually real-time communication.

Keeping these projects separate helps me learn both sides of .NET development. They share a building-automation theme, but **the existing dashboard is not currently presented as a client of this backend**. Connecting them is a possible future step, not an implemented feature.

## What I want to build

The scenario is a fictional commercial office building with **three floors and twelve rooms**. It is intentionally small enough to understand and test, while still giving me realistic problems to solve.

Over time, I want an operator to be able to:

- Browse buildings, floors, rooms, and equipment.
- Monitor simulated temperature, humidity, and occupancy readings.
- Change lighting levels and HVAC setpoints through validated commands.
- Review current equipment states and historical sensor readings.
- See energy-use estimates, alarms, and acknowledgements.
- Watch new readings appear on a Blazor dashboard without manually refreshing.

I won't add all of this at once. The first milestone is much smaller: **a working ASP.NET Core API with a simple endpoint that I can explain, run, and test myself**.

## Technologies I'm using 

The stack below is a **roadmap**, not a list of dependencies already installed or features already completed.

| Area | Technologies and concepts | Why I want to learn them |
| --- | --- | --- |
| Language and runtime | C#, .NET 10 | Build a stronger foundation in C# and modern .NET development |
| Backend | ASP.NET Core Web API, REST, dependency injection | Understand how client/server applications are structured |
| Data | PostgreSQL, Entity Framework Core, SQL, migrations | Store building information and readings beyond a single application session |
| Simulation | Background services, `async`/`await`, cancellation | Model periodically changing sensor data without physical hardware |
| Frontend | Blazor, MudBlazor, HTML, CSS | Build an interface that consumes backend data |
| Live updates | ASP.NET Core SignalR | Explore real-time updates and connection handling |
| Quality | xUnit, integration testing, logging, validation | Make code easier to trust, debug, and maintain |
| Tooling | JetBrains Rider, Git, GitHub, GitHub Actions | Practise a repeatable development workflow |
| Later exploration | TypeScript, Docker Compose, Modbus TCP simulation | Explore useful additions when the core system is stable |

I plan to introduce each technology only when the project has a reason to use it. For example, I want to understand an in-memory room service before replacing it with a database, and get a conventional HTTP API working before adding real-time updates.

## Development roadmap

I'm following a learning-first roadmap. Each stage is intended to end with something working that I can test, explain, and commit to GitHub.

| Phase | Focus | Intended milestone |
| --- | --- | --- |
| 1 | Development environment and HTTP fundamentals | Create and run a first ASP.NET Core API endpoint |
| 2 | C# models and REST APIs | Expose building, floor, and room data through endpoints |
| 3 | PostgreSQL and Entity Framework Core | Persist buildings and rooms using a relational database |
| 4 | Building sensor simulation | Generate and store changing temperature, humidity, and occupancy readings |
| 5 | Blazor and MudBlazor | Display API-backed room and building information in a web UI |
| 6 | Lighting and HVAC controls | Send validated control commands through the API |
| 7 | Real-time monitoring | Deliver updates to the UI using SignalR |
| 8 | Energy and alarms | Add consumption history, alarm states, and acknowledgement |
| 9 | Security and tests | Introduce roles, validation, automated tests, and CI |
| 10 | TypeScript and automation protocols | Explore a useful TypeScript feature and a Modbus TCP simulator |
| 11 | Portfolio and interview preparation | Improve documentation, demos, architecture explanations, and test coverage |

**The roadmap is flexible.** I would rather finish a smaller feature properly than add several technologies I cannot yet explain. BACnet or physical-device integration may be explored later, but neither is required for the initial application.

## How I'm approaching the code

I'm starting with a simple ASP.NET Core Web API project instead of designing a large multi-project architecture before I need one. As the application grows, I expect to introduce separate responsibilities for domain models, request/response DTOs, services, data access, and background simulation.

A few engineering questions I particularly want to explore are:

- **Models versus DTOs:** What should a room look like inside the application, and what information should the API return?
- **Commands versus measurements:** Why should changing a target temperature not immediately change a measured temperature?
- **Persistence:** How should buildings, rooms, and timestamped sensor readings relate to one another in PostgreSQL?
- **Asynchronous work:** How can a simulation run periodically and shut down cleanly?
- **State and consistency:** How should the backend validate controls and keep equipment status, alarms, and history in agreement?
- **Testing:** Which rules should be verified with unit tests, and which behaviours need integration tests?

These are not just features to check off. They are the areas where I want to become more confident making and explaining software design decisions.

### Intended data flow

The eventual application will follow a flow similar to this:

```text
Simulated sensors and equipment
             |
             v
    ASP.NET Core services
             |
        PostgreSQL
             |
             v
      REST API / SignalR
             |
             v
     Blazor + MudBlazor UI
```

This diagram represents the **intended direction**, not an already implemented deployment. The first iterations will be much simpler, using local in-memory data and HTTP endpoints.

## Development workflow

I'm developing on **macOS (Apple Silicon)** with **JetBrains Rider** and the **.NET 10 SDK**. I use Git and GitHub to track progress.

My aim is to work in small increments:

1. Understand the next concept and why the application needs it.
2. Implement a manageable feature myself.
3. Run it, test it, and debug any problems.
4. Review the code for correctness and readability.
5. Commit a working milestone with a meaningful message.

I also want to practise explaining my decisions as I go, since understanding the code matters more to me than simply getting a large application generated quickly.

## Running the project locally

**Setup instructions will be added and verified as the first API and solution structure are implemented.** The development plan currently follows:

- macOS with .NET 10 SDK installed (other platforms may be supported later).
- JetBrains Rider or another .NET-compatible editor.
- PostgreSQL and Docker **only when the relevant development phases begin**.



## Current scope and limitations

This project is still being developed, so the planned features above should not be interpreted as complete. In particular:

- There is no connection to physical HVAC, lighting, or building automation equipment.
- Sensor values and device responses will be simulated rather than measured from real hardware.
- Database persistence, a browser dashboard, SignalR, authentication, and industrial protocol integration belong to later phases of the roadmap.
- Architecture and setup instructions will evolve as the application takes shape.

I'll update this README as milestones are implemented, and include screenshots, API examples, tests, and a working demo when there is something real to show.

## Independent portfolio project

This project is inspired by general building automation and building management concepts. It is an independent educational project and  does not use proprietary controllers, source code, or product interfaces.
