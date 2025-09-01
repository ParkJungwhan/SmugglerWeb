# Repository Guidelines

This repository hosts a .NET 8 Blazor application composed of a server project and a WebAssembly client. Use this guide to navigate the codebase and contribute consistently.
한글로 출력. 빌드미리하지말고 커밋도 미리하지말고. 커밋하라고 할때만 커밋할것.

## Project Structure & Module Organization
- Root: `SmugglerWeb.sln` solution, `.github/`, `.dockerignore`, `README.md`.
- Server: `SmugglerWeb/SmugglerWeb` (entry `Program.cs`, Razor components in `Components/`, assets in `wwwroot/`).
- Client: `SmugglerWeb/SmugglerWeb.Client` (Blazor WASM, pages in `Pages/`, static assets in `wwwroot/`).
- Config: `appsettings.json`, `appsettings.Development.json`; user secrets enabled via `UserSecretsId`.
- Docker: `SmugglerWeb/SmugglerWeb/Dockerfile` (context is repo root).

## Build, Test, and Development Commands
- Restore: `dotnet restore`
- Build: `dotnet build` (solution or individual project)
- Run (server): `dotnet run --project SmugglerWeb/SmugglerWeb`
- Publish: `dotnet publish SmugglerWeb/SmugglerWeb -c Release -o publish`
- Docker build/run: `docker build -f SmugglerWeb/SmugglerWeb/Dockerfile -t smugglerweb .` then `docker run -p 8080:8080 smugglerweb`

## Coding Style & Naming Conventions
- Language: C# 12 on .NET 8; `Nullable` and `ImplicitUsings` are enabled.
- Indentation: 4 spaces; braces on new lines; keep namespace and file layout consistent with existing files.
- Naming: PascalCase for types/methods, camelCase for locals/parameters, `I`-prefixed interfaces, `App.razor`, `*.razor` components in `Components`/`Pages`.
- Formatting: prefer `dotnet format` before commits (if installed). Keep usings minimal and ordered.

## Testing Guidelines
- No test project exists yet. Recommended: create `SmugglerWeb.Tests` (xUnit), place tests under `Tests/` or project root `SmugglerWeb.Tests`, and name files `*Tests.cs`.
- Run tests: `dotnet test`. Aim for coverage of routing, component rendering, and basic server endpoints.

## Commit & Pull Request Guidelines
- Commits: short, imperative subject line; keep related changes together. Conventional Commits (e.g., `feat:`, `fix:`) are welcome but not required.
- PRs: include a clear description, linked issues (e.g., `Closes #123`), steps to test locally, and screenshots/gifs for UI changes.
- CI/CD: if adding pipelines, ensure build and `dotnet test` pass for solution.

## Security & Configuration Tips
- Store secrets with .NET User Secrets for local dev: `dotnet user-secrets set "Section:Key" "value" --project SmugglerWeb/SmugglerWeb`.
- Do not commit secrets or environment-specific settings. Keep production config externalized.

