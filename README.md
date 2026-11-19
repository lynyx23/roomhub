# RoomHub

Study-room booking API following `ASSIGNMENT.md`: five vertical slices, a rich domain,
`Result<T>`/`Error`, custom async CQRS handlers, and EF Core with SQLite.

## Run

```bash
dotnet run --project src/RoomHub.Api
```

The launch profile serves `http://localhost:5090`. Run `roomhub.http` in order using
VS Code REST Client; it contains R1–R11 checks and a restart check. Its dynamic date
syntax is specific to that client; replace the variables with dates if using another client.

The `RoomHubDB` connection string is in `src/RoomHub.Api/appsettings.json`. Startup
applies migrations asynchronously. The default `roomhub.db` path is relative to the
process working directory. Use an absolute path when launching from different directories:

```bash
ConnectionStrings__RoomHubDB='Data Source=/absolute/path/roomhub.db' dotnet run --project src/RoomHub.Api
```

Keep the database path unchanged across restarts. Database files are ignored by Git.

## Architecture

- `Features/`: endpoint, command/query, handler and contracts for each operation.
- `Domain/`: entities, business rules, result/error types and repository/Unit of Work interfaces.
- `Common/CQRS/`: async command/query contracts and scoped handler registration.
- `Infrastructure/Persistence/`: DbContext, EF repositories, mappings and migrations.

Endpoints await injected handler interfaces and map results to HTTP. Commands load
entities, call domain behavior, stage changes and save through the shared scoped Unit
of Work. Queries use no-tracking projections. Cancellation tokens pass through each
I/O operation. Pure domain validation remains synchronous because it performs no I/O.

Booking creation and cancellation start a SQLite write transaction before reading
state, then validate and save in that transaction. This protects overlap checks,
daily quotas and cancellation against concurrent writers, including separate hosts
sharing the same database. Failed commands dispose the transaction without committing.

Dates are normalized to UTC; input without a timezone is interpreted as UTC. SQLite
reads restore `DateTimeKind.Utc`. Participant identity uses trimmed, case-sensitive
`bookedBy` text, consistent with the existing behavior.

## Verification

```bash
dotnet build src/RoomHub.Api
dotnet run --project tests/RoomHub.RuleChecks
```

The checks cover domain boundaries and the HTTP contract using an in-process test
server with a temporary SQLite database. They verify migrations, UTC round trips,
Unit of Work behavior, rollback, R1–R11, persistence after a complete host restart,
and concurrent booking/cancellation across two hosts sharing one database.
