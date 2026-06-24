# ToDoList

Clean Architecture ToDoList project with an ASP.NET Core API backend and an Angular frontend.

## Development Requirements

- .NET SDK 10
- Node.js 24 LTS
- npm 11
- Angular CLI 22

Recommended Node version for the Angular app:

```bash
node --version
# v24.x
```

## Backend

The backend API project is:

```text
src/ToDoList.Api
```

Run it from the repository root:

```bash
dotnet run --project src/ToDoList.Api/ToDoList.Api.csproj
```

Default local URLs:

```text
API:     http://localhost:5068
Swagger: http://localhost:5068/swagger/index.html
```

Useful endpoint:

```text
GET http://localhost:5068/api/todos
```

## Frontend

The Angular app is:

```text
frontend/ToDoList.Web
```

Run it from the Angular project folder:

```bash
cd frontend/ToDoList.Web
npm install
npm start
```

Default local URL:

```text
http://localhost:4200
```

The Angular dev server uses `proxy.conf.json` to forward API requests:

```text
/api/** -> http://localhost:5068
```

That means frontend code should call:

```text
/api/todos
```

instead of hard-coding the backend host.

## Run The App Locally

Start the backend first:

```bash
dotnet run --project src/ToDoList.Api/ToDoList.Api.csproj
```

Then start the frontend in a second terminal:

```bash
cd frontend/ToDoList.Web
npm start
```

Open:

```text
http://localhost:4200
```
