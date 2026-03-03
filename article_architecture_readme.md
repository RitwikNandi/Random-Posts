# 📰 Satire Articles API Architecture

A scalable, testable, and swappable architecture for loading satire articles from JSON into memory and exposing them via API endpoints.

---

# 🎯 Purpose

This project is designed to:

- Load 20,000+ satire articles from a JSON file
- Store them in memory
- Expose them via API endpoints
- Keep controller logic thin
- Separate business logic from data access
- Allow future migration to a database without breaking controllers
- Remain fully unit testable

---

# 🏗️ Architectural Overview

This solution follows:

- Repository Pattern
- Service Layer Pattern
- Dependency Injection
- Layered Architecture Principles

The controller never talks directly to file storage or database logic.

---

# 🧭 Request Flow

```
Client Request
      ↓
ArticlesController
      ↓
IArticleService (Business Logic)
      ↓
IArticleRepository (Data Access)
      ↓
JsonArticleRepository (Current Implementation)
      ↓
In-Memory List (Loaded from JSON)
```

Future swap:

```
JsonArticleRepository  →  DbArticleRepository
(No controller changes required)
```

---

# 📁 Suggested Project Structure

```
/Controllers
    ArticlesController.cs

/Services
    IArticleService.cs
    ArticleService.cs

/Repositories
    IArticleRepository.cs
    JsonArticleRepository.cs
    DbArticleRepository.cs (Future)

/Models
    RandomPost.cs

/Data
    satire_20000_articles.json
```

---

# 🧩 Layer Responsibilities

## 1️⃣ Controller Layer

Responsible for:

- Handling HTTP requests
- Returning HTTP responses
- Calling service layer

Rules:
- No file reading
- No business logic
- No database logic

Controllers remain thin.

---

## 2️⃣ Service Layer

Responsible for:

- Business rules
- Pagination logic
- Validation
- Sorting/filtering
- Future caching logic

This layer orchestrates behavior.

Example future expansions:
- GetPagedAsync
- FilterByCategory
- AddArticle
- UpdateArticle
- DeleteArticle

---

## 3️⃣ Repository Layer

Responsible for:

- Data retrieval
- Data storage
- Abstracting persistence mechanism

Current implementation:
- JSON file loaded into memory

Future implementation:
- Entity Framework Core
- SQL database

Repository is swappable via dependency injection.

---

# 🔌 Dependency Injection Setup

```csharp
builder.Services.AddSingleton<IArticleRepository, JsonArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
```

When migrating to database:

```csharp
builder.Services.AddScoped<IArticleRepository, DbArticleRepository>();
```

No controller changes required.

---

# 🧪 Testability Strategy

Because everything depends on interfaces:

- Mock IArticleRepository to test ArticleService
- Mock IArticleService to test Controller
- No dependency on file system during unit tests

This allows fully isolated testing.

---

# 🚀 Future Ready Enhancements

This architecture supports:

- Database integration
- Full CRUD operations
- DTO layer
- AutoMapper integration
- Caching layer
- Cursor-based pagination
- Authorization & authentication rules
- Logging & telemetry

The structure does not require redesign when scaling.

---

# ⚠️ Performance Considerations

- Do not expose all 20,000 records in a single request
- Implement pagination at service level
- Keep repository as Singleton when using in-memory storage

For doom-scroll scenarios, implement:

```
GET /api/articles?page=1&pageSize=20
```

---

# 🧠 Architectural Philosophy

This design ensures:

- Separation of concerns
- Maintainability
- Extensibility
- Swappability
- Clean dependency graph
- Production readiness

The system can evolve from JSON → Database → Distributed storage without rewriting the API surface.

---

# 🏁 Conclusion

This architecture transforms a simple JSON-based demo into a scalable backend foundation.

It is struct