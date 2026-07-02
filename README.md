# ShowTime

*A music-festival ticketing and management platform.*

![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?logo=dotnet&logoColor=white)
![Blazor](https://img.shields.io/badge/Blazor-Web%20App-512BD4?logo=blazor&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)

ShowTime is a web application for discovering and running music festivals. Attendees can browse
festivals, explore lineups, and book tickets; administrators manage the festivals, artists, lineups,
and users behind them. Access is gated by **cookie-based authentication with role-based
authorization**, so visitors and organisers see different capabilities.

<!-- Tip: drop a screenshot of the festivals page here — it makes the repo far more inviting.
     ![ShowTime](docs/screenshot.png) -->

## Features

- **Festivals** — create and browse festivals with location, dates, capacity, and splash art.
- **Artists** — maintain an artist roster (name, genre, image).
- **Lineups** — schedule artists onto stages at specific times for each festival.
- **Bookings & tickets** — book tickets by type, price, and quantity.
- **Users & roles** — register / log in; administrators manage users.
- **Authentication** — secure cookie sessions with a role-based access model and an access-denied flow.

## Domain model

```
Festival ──< Lineup >── Artist        (an artist plays a stage at a time, per festival)
Festival ──< Booking >── User         (a user books tickets for a festival)
```

## Tech stack

- **Blazor Web App (.NET 8)** using both **Interactive Server** and **WebAssembly** render modes
- **Entity Framework Core** with **SQL Server**
- Layered architecture: `ShowTime` (UI) · `ShowTime.BusinessLogic` (services) · `ShowTime.DataAccess`
  (DbContext, models, repositories, migrations)

## Getting started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB / Express is fine)

### Setup

1. Update the `ShowTimeContext` connection string in `ShowTime/ShowTime/appsettings.json`.
2. Apply the database migrations:

   ```bash
   dotnet ef database update --project ShowTime.DataAccess --startup-project ShowTime/ShowTime
   ```

3. Run the app:

   ```bash
   dotnet run --project ShowTime/ShowTime
   ```

## Project structure

```
ShowTime.sln
├── ShowTime/
│   ├── ShowTime/            # Server project — Blazor pages, layout, auth, Program.cs
│   └── ShowTime.Client/     # WebAssembly client project
├── ShowTime.BusinessLogic/  # Services, DTOs, validation
└── ShowTime.DataAccess/     # DbContext, models, configurations, repositories, migrations
```
