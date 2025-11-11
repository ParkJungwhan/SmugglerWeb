# Implementation Plan: SmugglerWeb Core (Auth + Data)

**Branch**: `[develop]` | **Date**: 2025-11-10 | **Spec**: C:\Repo\SmugglerWeb\NewSmugglerWeb\specs\develop\spec.md
**Input**: Feature specification from `/specs/develop/spec.md`

**Note**: 본 계획은 템플릿 구조를 따르며, 헌법의 헌법 게이트(G1–G5)를 반영합니다.

## Summary

Blazor(Server + WASM) 기반 코어 기능: OAuth2/OIDC 로그인/가입(P1), 프로필 조회(P2),
PostgreSQL 기반 예시 Item CRUD(P3)를 제공한다. 계약 기반 REST/JSON, 타입 안전 DTO 공유,
구조적 로깅 및 표준 오류 응답을 채택한다.

## Technical Context

**Language/Version**: C# 12 (.NET 8), TypeScript 5.x  
**Primary Dependencies**: ASP.NET Core, Blazor(Server & WASM), EF Core, Npgsql, OIDC(OAuth2) 미들웨어, bUnit  
**Storage**: PostgreSQL (EF Core Migrations)  
**Testing**: xUnit, bUnit, (계약/통합 테스트 구성)  
**Target Platform**: Web (Blazor Server + WebAssembly)  
**Project Type**: web  
**Performance Goals**: API p95 < 200ms(로컬 기준), 초기 동시 사용자 100  
**Constraints**: 비밀은 User Secrets/환경 변수, 로깅은 구조화, 표준 오류 응답 필수  
**Scale/Scope**: 초기 사용자 1k, 기본 3 유저 스토리 범위  

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- G1 경계: 서버/클라이언트 코드 혼용 금지 — 준수(프로젝트 분리 전제)
- G2 계약/DTO: 공유 DTO와 REST/JSON 계약 — 준수(contracts/에 OpenAPI 작성)
- G3 테스트: 최소 시나리오 테스트 계획 포함 — 준수(xUnit/bUnit 계획)
- G4 관찰성: 구조적 로깅/오류 형식 — 준수(표준 응답/마스킹 계획)
- G5 비밀/구성: User Secrets/외부화 — 준수(.NET User Secrets 계획)

## Project Structure

```text
SmugglerWeb.sln
SmugglerWeb/SmugglerWeb                 # Server (Program.cs, Components/, wwwroot/)
SmugglerWeb/SmugglerWeb.Client          # WASM (Pages/, wwwroot/)
specs/develop/                          # 문서/계약/리서치
  plan.md
  research.md
  data-model.md
  quickstart.md
  contracts/
```

**Structure Decision**: 기존 솔루션 레이아웃을 유지하며, 공유 DTO는 별도 공유 프로젝트(또는 폴더)로 추출.

## Complexity Tracking

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| (없음) | - | - |

