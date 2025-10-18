# ⚠️ Important Notice  

> **Currently only the JavaScript client (`JFL7XU_HFT_2022232.JSClient`) is functional.**  
> The WPF and other clients are **temporarily broken** due to the recent switch to **JWT token-based authorization** and will be **updated in a future revision**.  

---

# JFL7XU_HFT_2022232  
*HFT Semester Project, updated to use JWT based authorization*  

## Project Overview  
This repository implements a multi‐layered application built in C#/.NET (with some JavaScript for client interaction) representing a **spacecraft ownership system**. The solution includes:  
- A **Repository** layer for data access  
- A **Logic** layer for business rules  
- API endpoints for controllers and web services  
- Multiple clients (WPF, JavaScript, etc.) interacting with the backend  
- A **Models** project defining domain entities  
- Integration with **authentication (ASP.NET Core Identity + JWT)** and **real‐time updates via SignalR**

---

## Architecture & Projects  

| Project | Purpose |
|----------|----------|
| **JFL7XU_HFT_2022232.Models** | Domain/entity classes (Owner, Starship, Hangar, etc.) |
| **JFL7XU_HFT_2022232.Repository** | Data access layer (repositories, EF Core context) |
| **JFL7XU_HFT_2022232.Logic** | Business logic, validation, and non-CRUD queries |
| **JFL7XU_HFT_2022232.Endpoint** | ASP.NET Core Web API (controllers, Identity, JWT, SignalR) |
| **JFL7XU_HFT_2022232.JSClient** | JavaScript web client communicating with the API (currently functional) |
| **JFL7XU_HFT_2022232.WpfClient** | WPF desktop client (currently broken due to JWT auth) |
| **JFL7XU_HFT_2022232.Test** | Unit and integration tests |

---

## Key Features  
- Full CRUD operations for core entities: **Owners**, **Starships**, **Hangars**  
- Clear separation of concerns via Repository–Logic–Endpoint architecture  
- **JWT-based authentication** with ASP.NET Core Identity  
- Protected API endpoints using `[Authorize]` attributes  
- **SignalR** integration for real-time updates (Create/Update/Delete broadcasts)  
- Interactive API documentation via **Swagger UI**  
- Multiple client implementations (web, desktop)  

---

## How to run 

### Prerequisites  
- .NET SDK (compatible with your project version)
- Visual Studio 2022 or VS Code  

### Setup & Run  
1. Clone the repository:  
   ```bash
   git clone https://github.com/Adam-004/JFL7XU_HFT_2022232.git
   ```
2. Set **`JFL7XU_HFT_2022232.Endpoint`** and **`JFL7XU_HFT_2022232.JSClient`** as the startup project.  
3. Run the project — the API should launch on `https://localhost:40567`.  
4. Access the **Swagger UI** at `https://localhost:40567/swagger` to explore endpoints.  

---

## Authentication & Authorization  
- The API now uses **JWT token-based authentication**.  
- Tokens are generated upon successful login and must be included in subsequent requests via the `Authorization: Bearer <token>` header.  
- The JavaScript client handles token storage and transmission automatically.  
- Other clients (WPF, etc.) are not yet updated to support this authentication model.

---

## Business Logic & Data  
The logic layer defines and enforces rules for:  
- Ownership relations between entities  
- Data validation (e.g., preventing duplicates or invalid states)  
- Non-CRUD queries and statistical operations  

---

## Real-Time Functionality  
- **SignalR** hub (`/hub`) broadcasts messages when entities are created, updated, or deleted.  
- The JS client subscribes to these events to reflect changes in real time.

---

## Testing    
```bash
dotnet test
```
The test suite validates logic-layer correctness and repository integration.

---

## Configuration Summary  

| Aspect | Configuration |
|--------|----------------|
| **CORS** | Configured for the JS client origin (`https://localhost:47580` by default) |
| **Auth** | JWT tokens issued on login; `[Authorize]` protects API routes |
| **SignalR** | `/hub` endpoint registered in Startup |
| **Swagger** | `/swagger` UI for API testing and documentation |

---

## Project Structure  
```
/JFL7XU_HFT_2022232.sln
/Models
/Repository
  /Databases
  /Repos
/Logic
/Endpoint
  /Controllers
  /Services
  /SignalRHub
/JSClient
/WpfClient
/Test
```

---

## Future Work  
- Update **WPF client** to support JWT token authentication 
### Maybe:
- Improve frontend UI/UX for JS client  
- Add more validation, pagination, and search features  
- Delete unused console client
