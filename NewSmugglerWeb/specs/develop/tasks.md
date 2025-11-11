---

description: "SmugglerWeb Core (Auth + Data) 기능 구현 작업 목록"
---

# Tasks: SmugglerWeb Core (Auth + Data)

**Input**: `/specs/develop/`의 설계 문서
**Prerequisites**: plan.md(필수), spec.md(필수), research.md, data-model.md, contracts/

**Tests**: 테스트는 선택 사항입니다. 스펙에서 명시되거나 요청된 경우에만 테스트 작업을 추가하세요.

**Organization**: 각 사용자 스토리별로 작업을 묶어 독립 구현/검증이 가능하도록 구성합니다.

## 형식: `[ID] [P?] [Story] 파일 경로가 포함된 설명`

- [P]: 병렬 가능(서로 다른 파일, 미해결 의존성 없음)
- [Story]: US1/US2/US3 중 하나(Setup/Foundational/Polish에는 표시하지 않음)
- 모든 작업은 정확한 파일 경로를 포함해야 합니다

## Phase 1: Setup (공유 인프라)

- [ ] T001 계획에 맞게 서버/클라이언트 구조 정합성 확인 `SmugglerWeb/`
- [ ] T002 계약/DTO 공유 폴더 생성 `SmugglerWeb/SmugglerWeb/Shared/`
- [ ] T003 TypeScript 툴체인 스켈레톤(tsconfig, npm scripts) 추가 `SmugglerWeb/SmugglerWeb.Client/wwwroot/ts/`
- [ ] T004 [P] 기본 스타일/에셋 구조 추가 `SmugglerWeb/SmugglerWeb.Client/wwwroot/`

---

## Phase 2: Foundational (모든 스토리의 선행 차단 요소)

- [ ] T005 개발용 연결 문자열 플레이스홀더 구성 `SmugglerWeb/SmugglerWeb/appsettings.Development.json`
- [ ] T006 EF Core DbContext 추가 `SmugglerWeb/SmugglerWeb/Data/AppDbContext.cs`
- [ ] T007 DbContext와 Npgsql 등록 `SmugglerWeb/SmugglerWeb/Program.cs`
- [ ] T008 표준 오류 응답 미들웨어 추가 `SmugglerWeb/SmugglerWeb/Middleware/ErrorHandlingMiddleware.cs`
- [ ] T009 구조적 로깅과 마스킹 정책 연결 `SmugglerWeb/SmugglerWeb/Program.cs`
- [ ] T010 OpenAPI 계약 존재 검증 `specs/develop/contracts/openapi.yaml`
- [ ] T011 인증 설정 바인딩(Authority, ClientId) 추가 `SmugglerWeb/SmugglerWeb/Program.cs`

**Checkpoint**: 기반 준비 완료 — 이후 사용자 스토리 병렬 진행 가능

---

## Phase 3: User Story 1 - OAuth2 로그인/가입 (Priority: P1)

**Goal**: 외부 OIDC 제공자와 Authorization Code + PKCE 인증 연동(로그인/로그아웃/콜백)
**Independent Test**: 로그인 버튼 → 제공자 → 콜백 처리 후 `/api/me` 200 응답

### Implementation

- [ ] T012 [P] [US1] 인증 엔드포인트 스캐폴드 추가 `SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs`
- [ ] T013 [US1] OpenIdConnect/OAuth 구성 `SmugglerWeb/SmugglerWeb/Program.cs`
- [ ] T014 [P] [US1] `/api/auth/login` 리다이렉트 구현 `SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs`
- [ ] T015 [P] [US1] `/api/auth/callback` 핸들러 구현 `SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs`
- [ ] T016 [US1] `/api/auth/logout` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs`
- [ ] T017 [P] [US1] 로그인 UI(버튼/상태) 추가 `SmugglerWeb/SmugglerWeb.Client/Pages/Login.razor`
- [ ] T018 [US1] 인증 상태 프로바이더 연결 `SmugglerWeb/SmugglerWeb.Client/Program.cs`

**Checkpoint**: US1 단독으로 동작하며 데모 가능

---

## Phase 4: User Story 2 - 사용자 프로필 조회 (Priority: P2)

**Goal**: 인증 사용자 프로필을 `/api/me`로 반환하고, 클라이언트에서 표시
**Independent Test**: 인증 후 `/api/me` 200 + 표준 JSON 반환

### Implementation

- [ ] T019 [P] [US2] `UserProfileDto` 추가 `SmugglerWeb/SmugglerWeb/Shared/UserProfileDto.cs`
- [ ] T020 [US2] `/api/me` 엔드포인트 구현 `SmugglerWeb/SmugglerWeb/Endpoints/UserEndpoints.cs`
- [ ] T021 [P] [US2] Identity Claims → DTO 매핑 `SmugglerWeb/SmugglerWeb/Endpoints/UserEndpoints.cs`
- [ ] T022 [US2] 프로필 페이지 및 fetch 로직 추가 `SmugglerWeb/SmugglerWeb.Client/Pages/Profile.razor`

**Checkpoint**: US1 + US2 모두 독립적으로 기능

---

## Phase 5: User Story 3 - 예시 도메인 CRUD (Priority: P3)

**Goal**: PostgreSQL 기반 Item 엔티티 CRUD REST 구현 및 클라이언트 목록/상세
**Independent Test**: 인증 상태에서 `/api/items` CRUD 동작, 유효성 검증 적용

### Implementation

- [ ] T023 [P] [US3] `Item` 엔티티 추가 `SmugglerWeb/SmugglerWeb/Domain/Item.cs`
- [ ] T024 [US3] DbContext에 DbSet<Item> 등록 `SmugglerWeb/SmugglerWeb/Data/AppDbContext.cs`
- [ ] T025 [US3] Item 서비스 구현 `SmugglerWeb/SmugglerWeb/Services/ItemService.cs`
- [ ] T026 [P] [US3] `GET /api/items` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/ItemEndpoints.cs`
- [ ] T027 [P] [US3] `POST /api/items` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/ItemEndpoints.cs`
- [ ] T028 [US3] `GET /api/items/{id}` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/ItemEndpoints.cs`
- [ ] T029 [US3] `PUT /api/items/{id}` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/ItemEndpoints.cs`
- [ ] T030 [US3] `DELETE /api/items/{id}` 구현 `SmugglerWeb/SmugglerWeb/Endpoints/ItemEndpoints.cs`
- [ ] T031 [P] [US3] Items 페이지(목록/생성) 추가 `SmugglerWeb/SmugglerWeb.Client/Pages/Items.razor`

**Checkpoint**: 모든 사용자 스토리가 독립적으로 동작

---

## Phase N: Polish & Cross-Cutting Concerns

- [ ] T032 문서 갱신(quickstart) `specs/develop/quickstart.md`
- [ ] T033 코드 정리 및 검증 규칙 보강 `SmugglerWeb/SmugglerWeb/`
- [ ] T034 성능 튜닝(DB 인덱스/로깅) `SmugglerWeb/SmugglerWeb/`
- [ ] T035 보안 강화(헤더/CORS) `SmugglerWeb/SmugglerWeb/Program.cs`

---

## 의존성 및 실행 순서

### 페이즈 의존성
- Setup(Phase 1): 선행 조건 없음
- Foundational(Phase 2): 모든 사용자 스토리를 차단하는 선행 조건
- User Stories(Phase 3+): Phase 2 완료 후 독립 진행, 우선순위대로 전달
- Polish: 선택된 스토리 완료 후 진행

### 사용자 스토리 의존성
- US1(P1): Phase 2만 선행 필요
+- US2(P2): 인증 컨텍스트 의존으로 US1 이후 권장
- US3(P3): Phase 2만 선행 필요

### 각 사용자 스토리 내 순서
- 모델 → 서비스 → 엔드포인트 → UI 연결
- 각 스토리는 독립적으로 검증 가능해야 함

### 병렬 기회
- [P] 표시 작업: T004, T012, T014, T015, T017, T019, T021, T023, T026, T027, T031

## 병렬 예시: User Story 1

```text
Task: "T014 [P] [US1] Implement /api/auth/login in SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs"
Task: "T015 [P] [US1] Implement /api/auth/callback in SmugglerWeb/SmugglerWeb/Endpoints/AuthEndpoints.cs"
Task: "T017 [P] [US1] Add Login UI in SmugglerWeb/SmugglerWeb.Client/Pages/Login.razor"
```

## 구현 전략

### MVP First (User Story 1만)
1. Phase 1: Setup 완료
2. Phase 2: Foundational 완료
3. Phase 3: US1(인증) 완료
4. 정지 및 검증: 로그인 후 `/api/me` 동작 확인
5. 데모/피드백

### 점진적 전달
1. Setup + Foundational → 기반 준비 완료
2. US1 추가 → 검증 → 데모
3. US2 추가 → 검증 → 데모
4. US3 추가 → 검증 → 데모

## 형식 유효성 검증
- 모든 작업은 `- [ ] T### [P?] [US?] 파일 경로가 포함된 설명` 형식을 따라야 함
- 사용자 스토리 단계에는 [US#] 라벨 필수, Setup/Foundational/Polish에는 생략
- 모든 작업은 정확한 파일 경로를 포함함

