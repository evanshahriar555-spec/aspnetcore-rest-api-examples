# ASP.NET Core REST API Examples (.NET 10)

Three separate tutorial-style applications demonstrating controller-based REST APIs, MongoDB, and a JavaScript frontend. Each project can run independently. These are learning examples, not production services.

## Requirements

- Windows 10/11, macOS, or Linux
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (`dotnet --version` should report `10.x`)
- Visual Studio 2026 with ASP.NET/web workload **or** Visual Studio Code with C# Dev Kit
- For **MongoBooksApi** only: MongoDB Community Server running locally on port `27017`, or a MongoDB Atlas connection string configured privately
- Internet for first-time NuGet package restore

## 1. Controller-based Web API

```powershell
cd ControllerTodoApi
dotnet restore
dotnet run --urls http://localhost:5101
```

In another terminal:

```powershell
Invoke-RestMethod http://localhost:5101/api/todoitems
Invoke-RestMethod -Method Post -Uri http://localhost:5101/api/todoitems -ContentType 'application/json' -Body '{"name":"walk dog","isComplete":false}'
Invoke-RestMethod http://localhost:5101/api/todoitems
```

Use `ControllerTodoApi.http` in Visual Studio's HTTP request editor to exercise GET/POST/PUT/DELETE. Data is held **in memory** and disappears when the server stops. DTOs prevent the `Secret` model property from being exposed.

## 2. Web API with MongoDB

Install and start [MongoDB Community Server](https://www.mongodb.com/try/download/community) first. Confirm it accepts connections at `mongodb://localhost:27017`. The sample uses database `BookStore` and collection `Books`.

```powershell
cd MongoBooksApi
dotnet restore
dotnet run --urls http://localhost:5102
```

In another terminal:

```powershell
Invoke-RestMethod http://localhost:5102/api/books
Invoke-RestMethod -Method Post -Uri http://localhost:5102/api/books -ContentType 'application/json' -Body '{"bookName":"The Hobbit","price":12.50,"category":"Fantasy","author":"J. R. R. Tolkien"}'
```

Copy the `id` returned by POST. Then request `GET http://localhost:5102/api/books/<id>` or use PUT/DELETE at that URL. MongoDB data persists between app restarts. For Atlas, supply the connection string via user secrets or an environment variable, not by committing credentials:

```powershell
$env:BookStoreDatabase__ConnectionString = 'mongodb+srv://...'
```

**Security:** Do not publish real database passwords or connection strings in Git.

## 3. Web API with JavaScript

```powershell
cd JavaScriptTodoApi
dotnet run --urls http://localhost:5103
```

Open **http://localhost:5103** in your browser. Add tasks, mark tasks complete, and delete them. The browser's `fetch` calls the same-origin ASP.NET Core controller endpoints. This sample uses an in-process thread-safe list, so data resets when the server stops.

## Windows quick start

1. Download this repository and extract it, or `git clone` your repository.
2. Install the **.NET 10 SDK** (not just the runtime). Restart your terminal.
3. Open PowerShell or Visual Studio's terminal in the extracted folder.
4. Run each project's commands above in a separate terminal. Each uses a different port.
5. Press **Ctrl+C** in the running terminal to stop a server.
6. If `dotnet` is not recognized, reopen your terminal and confirm SDK installation using `dotnet --info`.
7. If NuGet restore fails, check your internet connection and package sources.
8. If MongoDB requests fail with connection errors, start MongoDB and verify its configured port.

## API endpoints

| Project | GET all | GET by ID | POST | PUT | DELETE |
| --- | --- | --- | --- | --- | --- |
| ControllerTodoApi | `/api/todoitems` | `/api/todoitems/{id}` | `/api/todoitems` | `/api/todoitems/{id}` | `/api/todoitems/{id}` |
| MongoBooksApi | `/api/books` | `/api/books/{id}` | `/api/books` | `/api/books/{id}` | `/api/books/{id}` |
| JavaScriptTodoApi | `/api/todoitems` | `/api/todoitems/{id}` | `/api/todoitems` | `/api/todoitems/{id}` | `/api/todoitems/{id}` |

## Publish to GitHub

Create a **new public** empty repository on GitHub, then run these commands from the folder containing this README:

```powershell
git init
git add .
git commit -m "Add three ASP.NET Core REST API examples"
git branch -M main
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPOSITORY.git
git push -u origin main
```

Replace `YOUR_USERNAME` and `YOUR_REPOSITORY` with your own values. GitHub may ask you to authenticate through your browser or Git Credential Manager. Share the resulting repository URL. Do not push `bin`, `obj`, or secrets.

## Microsoft references

- [Controller-based Web API](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-10.0)
- [Web API with MongoDB](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-10.0)
- [Call a Web API with JavaScript](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript?view=aspnetcore-10.0)

## Notes

These examples are written for .NET 10. Some Microsoft tutorial screenshots or sample files may use a different version. The code is provided as a complete starter project, but this environment does not have the .NET SDK installed, so compilation and runtime behavior have **not** been verified here. Run `dotnet restore` and `dotnet build` on your own machine before relying on it.
