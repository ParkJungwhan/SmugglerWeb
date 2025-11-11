<!--
Sync Impact Report
- Version change: N/A → 1.0.0
- Modified principles: 신규 정의 (5개 원칙 추가)
- Added sections: "기술 스택 및 제약", "개발 워크플로우, 리뷰, 품질 게이트"
- Removed sections: 없음
- Templates requiring updates:
  ⚠ .specify/templates/plan-template.md (Constitution Check 게이트 문구 반영 필요)
  ⚠ .specify/templates/spec-template.md (수용 기준/우선순위 예시 한국어화 및 독립 테스트 강조 정합성 확인)
  ⚠ .specify/templates/tasks-template.md (테스트 선택사항 문구 vs 헌법의 테스트 원칙 정합성 조정)
  ⚠ .specify/templates/agent-file-template.md (.NET 8/Blazor 명시 및 명령 섹션 보강)
  ⚠ .specify/templates/checklist-template.md (보안/접근성/관찰성 카테고리 예시 추가 고려)
- Follow-up TODOs:
  TODO(templates): 위 템플릿 정합성 반영 PR 생성
-->

# SmugglerWeb Constitution

## Core Principles

### I. 서버/클라이언트 경계 준수 (Blazor Server + WASM 분리)
SmugglerWeb은 .NET 8 Blazor 기반으로 서버(`SmugglerWeb/SmugglerWeb`)와 WASM 클라이언트
(`SmugglerWeb/SmugglerWeb.Client`)를 명확히 분리하여 개발한다.
- MUST: 서버 전용 코드와 클라이언트 전용 코드를 혼용하지 않는다.
- MUST: 공용 계약/DTO는 공유 가능한 프로젝트(또는 폴더)로 추출한다.
- MUST: API 진입점, 라우팅, 정적 자산 경로를 각 프로젝트 규칙에 맞춘다.
- SHOULD: 서버와 클라이언트 간 통신은 버전 가능한 계약을 따른다.
이유: 경계가 명확할수록 배포/테스트가 쉬워지고, 깨지지 않는 변경이 가능해진다.

### II. 계약 기반 API와 타입 안전 공유 모델
서버-클라이언트 간 데이터 교환은 명세화된 계약(REST/JSON 등)과 타입 안전한 DTO/Record를 사용한다.
- MUST: DTO는 직렬화 친화적이며 불변성 지향으로 설계한다.
- MUST: 계약 변경 시 하위 호환을 우선하며, 불가시 명시적 MAJOR 변경으로 관리한다.
- MUST: 모델에 대한 유효성 검증과 오류 응답 형식을 표준화한다.
- SHOULD: 공유 프로젝트를 통해 중복 타입 정의를 제거한다.
이유: 명확한 계약은 리그레션을 줄이고, 다중 클라이언트 확장을 용이하게 한다.

### III. 테스트 우선과 단계적 도입
현재 테스트 프로젝트가 없더라도, 새로운 기능을 추가할 때 테스트 가능성을 보장하고 점진적으로 도입한다.
- MUST: 신규 기능에는 최소 한 개 이상의 검증 가능한 시나리오 테스트(단위/bUnit/계약/통합) 계획을 포함한다.
- SHOULD: 핵심 라우팅, 구성 요소 렌더링, 기본 서버 엔드포인트에 대해 우선 테스트를 추가한다.
- MUST: 빌드 파이프라인에 통합될 테스트 명령은 `dotnet test` 기준으로 정렬한다.
이유: 회귀 방지와 안전한 리팩터링을 위해 테스트는 필수 투자다.

### IV. 관찰성(Observability)과 진단 가능성
운영 중 문제를 빠르게 식별/분석할 수 있도록 로깅과 오류 처리를 표준화한다.
- MUST: 구조적 로깅을 사용하고, 사용자 데이터는 마스킹한다.
- MUST: 실패 시 일관된 오류 응답 형식을 사용한다.
- SHOULD: 주요 상호작용에 진단 로그/추척 정보를 남긴다.
이유: 문제 재현 비용을 줄이고 품질을 가시화한다.

### V. 보안과 구성 관리
비밀 정보는 코드베이스에 포함하지 않으며 환경 별 구성을 체계적으로 분리한다.
- MUST: 로컬 비밀은 .NET User Secrets로 관리하고 저장소에 커밋하지 않는다.
- MUST: 프로덕션 설정은 외부화하고, 민감 데이터는 절대 저장소에 노출하지 않는다.
- SHOULD: 입력 검증, 인증/인가, CORS/헤더 보안을 적정 수준으로 적용한다.
이유: 초기 단계부터 보안을 내재화해야 비용과 리스크를 최소화할 수 있다.

## 기술 스택 및 제약

- 플랫폼: .NET 8, C# 12, Blazor (Server + WebAssembly)
- 솔루션: `SmugglerWeb.sln`
- 서버: `SmugglerWeb/SmugglerWeb` (엔트리 `Program.cs`, Razor 구성요소 `Components/`, 정적 자산 `wwwroot/`)
- 클라이언트: `SmugglerWeb/SmugglerWeb.Client` (페이지 `Pages/`, 정적 자산 `wwwroot/`)
- 구성: `appsettings.json`, `appsettings.Development.json`, User Secrets 활성화
- 도커: `SmugglerWeb/SmugglerWeb/Dockerfile` (빌드 컨텍스트 = 저장소 루트)
- 코드 스타일: 4스페이스, 괄호는 새 줄, Nullable/ImplicitUsings 활성화, 최소한의 using
- 명령: 복구 `dotnet restore`, 빌드 `dotnet build`, 실행 `dotnet run --project SmugglerWeb/SmugglerWeb`,
  게시 `dotnet publish SmugglerWeb/SmugglerWeb -c Release -o publish`,
  도커 `docker build -f SmugglerWeb/SmugglerWeb/Dockerfile -t smugglerweb .`

## 개발 워크플로우, 리뷰, 품질 게이트

헌법 게이트(Constitution Check) — 계획/설계 단계에서 다음을 확인한다.
- G1: 서버/클라이언트 경계 위반 없음 (크로스 참조/의존 금지)
- G2: 공유 DTO/계약의 위치와 버전 전략 명시
- G3: 최소 테스트 범위와 유형(단위/bUnit/계약/통합) 정의
- G4: 로깅/오류 응답 형식 정의 및 개인정보 마스킹 방침
- G5: 비밀/환경 구성 전략(User Secrets, 외부화) 명시

리뷰 프로세스
- PR는 위 게이트(G1–G5) 체크리스트를 통과해야 한다.
- 복잡도 증가(새 프로젝트/레이어 추가)는 정량적 근거와 단순 대안 기각 사유를 포함한다.
- UI 변경은 스크린샷/녹화와 접근성 고려사항을 포함한다.

품질 기준
- 린 변경: 관련 변경만 묶고, 표준 명령으로 재현 가능해야 한다.
- 문서화: README/가이드 또는 스펙 문서에 실행·테스트 방법을 갱신한다.

## Governance

효력
- 본 헌법은 SmugglerWeb 개발 관행을 상위에서 규정하며, 충돌 시 본 헌법이 우선한다.

개정 절차
- 제안서에 변경 배경, 영향 분석(게이트/템플릿), 마이그레이션 계획을 포함한다.
- 최소 1인 이상의 동료 리뷰 승인을 거쳐 병합한다.

버전 정책 (Semantic Versioning)
- MAJOR: 원칙 삭제/재정의 등 하위 호환 불가 변화
- MINOR: 원칙/섹션 추가 또는 유의미한 확장
- PATCH: 문구 정리, 오탈자, 비의미적 명확화

준수 검토
- 모든 PR은 게이트 체크리스트를 통과해야 하며, 리뷰어는 준수 여부를 확인한다.
- 정기적으로(분기 최소 1회) 헌법과 템플릿 정합성을 재검토한다.

**Version**: 1.0.0 | **Ratified**: 2025-11-10 | **Last Amended**: 2025-11-10

