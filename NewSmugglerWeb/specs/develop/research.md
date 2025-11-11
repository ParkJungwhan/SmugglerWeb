# Research: SmugglerWeb Core (Auth + Data)

## Decisions

### OAuth2/OIDC 플로우
- Decision: Authorization Code + PKCE, 외부 OIDC 제공자(벤더 중립) 연동
- Rationale: Blazor WASM 호환/보안 표준, 모바일/웹 모두 검증된 패턴
- Alternatives considered: Implicit Flow(보안 취약), Password Grant(비권장), 자체 IdP 구축(초기 과도)

### 인증 토큰 저장/전파
- Decision: WASM는 브라우저 메모리/토큰 관리 라이브러리 사용, Server는 인증 쿠키 사용
- Rationale: XSS/CSRF 고려, 플랫폼 권장 구성
- Alternatives: LocalStorage(보안 위험), 전역 JS 변수(취약)

### 데이터베이스/마이그레이션
- Decision: PostgreSQL + EF Core Migrations, 연결은 Npgsql
- Rationale: .NET 8과 성숙한 통합, 마이그레이션 워크플로우 용이
- Alternatives: Dapper(간결하지만 마이그레이션 별도), Raw SQL(유지보수 어려움)

### API 스타일/계약
- Decision: REST + JSON, OpenAPI 3.0 스펙 제공(contracts/openapi.yaml)
- Rationale: 광범위한 도구/검증/계약 테스트 호환
- Alternatives: GraphQL(과도), gRPC(브라우저 제약)

### JS/TS 빌드
- Decision: TypeScript(tsc) + npm scripts(초기), 필요 시 Vite 도입
- Rationale: 초기 복잡도 최소화, 점진적 확장 가능
- Alternatives: Vite/webpack(강력하나 초기 과도), esbuild(간단하나 생태계 요구 상이)

### 성능/규모 목표(초기)
- Decision: API p95 < 200ms(로컬), 동시 사용자 100, Item 1만 건
- Rationale: 초기 목표로 현실적이며 검증 가능
- Alternatives: 더 높은 목표는 인프라 비용 증가

## Open Questions (해결)

1. OAuth2 제공자 구체화 — Decision: Generic OIDC(환경변수로 권고: AUTH_AUTHORITY, CLIENT_ID 등)
2. 비밀 관리 — Decision: User Secrets(로컬), 환경 변수/Key Vault(운영)
3. 오류 응답 표준 — Decision: { code, message, details?, traceId } 형식
4. 로깅 — Decision: 구조적 로깅(Serilog 등 선택 가능, 템플릿 수준에서는 기본 LoggerFactory)

## Consolidated Summary

- Authentication: OIDC Code + PKCE, 브라우저/서버 권장 패턴 채택
- Data: PostgreSQL + EF Migrations
- Contracts: OpenAPI 3.0, 표준 오류 포맷
- Tooling: tsc 우선, 필요 시 Vite
- Targets: 초기 성능/규모 목표 설정으로 측정 가능성 확보

