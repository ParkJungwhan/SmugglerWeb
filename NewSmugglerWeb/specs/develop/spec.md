# Feature Specification: SmugglerWeb Core (Auth + Data)

**Feature Branch**: `[develop]`  
**Created**: 2025-11-10  
**Status**: Draft  
**Input**: User description: "이 프로젝트는 블레이저로 구현되며 DB는 PostgreSQL, 회원가입 등의 로그인은 Oauth2, 그리고 구현은 HTML, CSS, JS와 TypeScript를 중심으로 구현할 예정."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - OAuth2 로그인/가입 (Priority: P1)

사용자는 외부 OAuth2/OIDC 제공자(예: Generic OIDC)로 로그인/가입을 진행한다.

**Why this priority**: 인증은 모든 기능의 선행 조건이기 때문.

**Independent Test**: OIDC Authorization Code + PKCE 플로우로 로그인 후 `/api/me`에서 사용자 프로필을 획득 가능함.

**Acceptance Scenarios**:

1. Given 미로그인 상태, When 로그인 버튼 클릭, Then 제공자 로그인 화면으로 리디렉션
2. Given 인증 후 콜백 수신, When 토큰 교환 성공, Then `/api/me`에서 사용자 정보 반환 200

---

### User Story 2 - 사용자 프로필 조회 (Priority: P2)

로그인한 사용자는 자신의 프로필과 연결된 외부 계정 정보를 조회할 수 있다.

**Why this priority**: 인증 다음으로 기본 활용 기능 제공.

**Independent Test**: 인증 토큰 보유 시 `/api/me` 호출이 200과 표준 스키마로 응답.

**Acceptance Scenarios**:

1. Given 로그인 상태, When `/api/me` 호출, Then 200과 표준 프로필 JSON 반환

---

### User Story 3 - 예시 도메인 CRUD (Priority: P3)

PostgreSQL을 사용하는 단순 엔티티(Item)의 생성/조회/수정/삭제 예시를 제공한다.

**Why this priority**: 데이터 계층/마이그레이션/검증 흐름 검증.

**Independent Test**: 인증 상태에서 `/api/items` REST 엔드포인트로 CRUD 가능.

**Acceptance Scenarios**:

1. Given 로그인 상태, When POST `/api/items` 유효 페이로드, Then 201과 리소스 반환
2. Given 로그인 상태, When GET `/api/items/{id}`, Then 200과 리소스 반환

### Edge Cases

- OAuth2 제공자 에러/취소 시 콜백 처리
- 토큰 만료/갱신 처리
- 잘못된 입력(검증 에러) 및 400/401/403/404/409 응답 표준화

## Requirements *(mandatory)*

### Functional Requirements

- FR-001: 시스템은 OAuth2/OIDC Authorization Code + PKCE 플로우를 지원해야 한다.
- FR-002: 시스템은 `/api/me`에서 인증 사용자 프로필을 반환해야 한다.
- FR-003: 시스템은 PostgreSQL을 사용해 Item 엔티티를 CRUD 할 수 있어야 한다.
- FR-004: 시스템은 표준 오류 응답 형식을 제공해야 한다.
- FR-005: 시스템은 구조적 로깅을 제공해야 한다.
- FR-006: 시스템은 비밀을 User Secrets/환경 변수로 관리해야 한다.

### Key Entities *(include if feature involves data)*

- User: 외부 제공자 식별자와 기본 프로필 정보 보유
- ExternalAccount: 제공자명, 외부 사용자 ID, 연결 상태
- Item: 예시 도메인 엔티티(id, name, description, createdAt)

## Success Criteria *(mandatory)*

### Measurable Outcomes

- SC-001: 신규 사용자 로그인 완료까지 2분 이내
- SC-002: 인증된 `/api/me` 호출 p95 < 200ms(로컬/샘플 데이터 기준)
- SC-003: P1/P2 시나리오에 대해 90% 이상 첫 시도 성공
- SC-004: CRUD 기본 경로 100 동시 요청에서 에러율 < 1%

