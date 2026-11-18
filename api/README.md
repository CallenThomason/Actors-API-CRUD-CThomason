## 1. The request journey

```
Client (Postman, browser)
    |
    v
Controller (speaks HTTP, reads the request, picks the status code.)
    |
    V
Services (The Rules / Logic. Converts DTO's to entities and back.)
    | Entity
    V
Repository (The only layer that talks to the database.)
    |Entity
    V
DbContext (Ef core)
    | SQL
    V
Database
```

## 2. Recap

app.db: We then created our models for our Database
AppDbContext: Allows our API to talk to our Data
We ran our migrations. (Mapped our Models / Entities to our DB table)
We lastly made our repository

