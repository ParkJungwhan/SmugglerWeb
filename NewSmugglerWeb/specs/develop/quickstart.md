# Quickstart: SmugglerWeb Core

## Prerequisites
- .NET 8 SDK
- Node.js (TS 빌드용, 선택)
- PostgreSQL 인스턴스

## Configuration

### User Secrets (로컬)
```powershell
cd C:\Repo\SmugglerWeb\NewSmugglerWeb
dotnet user-secrets set "Auth:Authority" "https://example-oidc"
dotnet user-secrets set "Auth:ClientId" "NewSmugglerWeb-client"
dotnet user-secrets set "Auth:ClientSecret" "<secret>"
dotnet user-secrets set "ConnectionStrings:Default" "Host=smuggler.info;Port=5432;Database=smuggler;Username=pjh2104;Password={password}"
```

## Restore & Build
```powershell
dotnet restore
dotnet build
```

## Database (EF Core 예시)
```powershell
# 서버 프로젝트 디렉터리에서
dotnet ef migrations add Init --project SmugglerWeb/SmugglerWeb
dotnet ef database update --project SmugglerWeb/SmugglerWeb
```

## Run
```powershell
dotnet run --project SmugglerWeb/SmugglerWeb
```

## API Contracts
- OpenAPI: C:\Repo\SmugglerWeb\NewSmugglerWeb\specs\develop\contracts\openapi.yaml

```csharp
// (Sample) 
using System;
```

