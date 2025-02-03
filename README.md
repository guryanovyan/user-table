# User table

## Implementation of data management in a single table.
Users can be added locally (only on the client side) or stored on the server (saved to the database).

### Technologies used:
- **Backend:** .NET 8, ASP.NET Core, Entity Framework Core
- **Frontend:** HTML, CSS, JavaScript (Vanilla JS)
- **Database:** SQLite

## Launch

**Restore dependencies:**
```
dotnet restore
```

**Apply migrations (if needed):**
```
dotnet ef database update
```

**Run the server:**
```
dotnet run
```

**Start frontend (open in browser):**
```
index.html
```
